using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using SimpleChatSolution.Database;
using SimpleChatSolution.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatWinForm
{
    public partial class Form2 : Form
    {
        private readonly Context _context;
        private string nick {  get; set; }
        HubConnection connection;
        public Form2(string nick)
        {
            InitializeComponent();
            this.nick = nick;
            connection = new HubConnectionBuilder()
            .WithUrl("https://chat-server-guifgr.herokuapp.com/chatHub")
            .Build();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (NewMessage.Text.Length > 0 && connection.State == HubConnectionState.Connected)
            {
                var chatMessage = new ChatMessage()
                {
                    Nick = nick,
                    MessageText = NewMessage.Text,
                    SentDate = DateTime.Now
                };
                await connection.InvokeAsync("SendMessageAsync", JsonConvert.SerializeObject(chatMessage));
                NewMessage.Text = "";
            }
        }

        public void AppendMessage(ChatMessage message)
        {
            var newMessage = $"{message.Nick}: {message.MessageText}\r\n{message.SentDate}";
            if (ChatRichText.Text.Length != 0)
            {
                newMessage = "\r\n\r\n" + newMessage;
            }
            ChatRichText.AppendText(newMessage);
            ChatRichText.ScrollToCaret();
        }

        private void ChatRichText_TextChanged(object sender, EventArgs e)
        {

        }

        private async void Form2_Load(object sender, EventArgs e)
        {
            connection.On<string>("SendMessageAsync", (message) =>
            {
                this.Invoke((Action)(() =>
                {
                    var obj = JsonConvert.DeserializeObject<ChatMessage>(message);
                    AppendMessage(obj);
                    using (var context = new Context())
                    {
                        context.ChatMessages.Add(obj);
                        context.SaveChanges();
                    }
                }));
            });

            if (ChatRichText.Text.Length < 1)
            {
                using (var context = new Context())
                {
                    var dataBaseMessages = context.ChatMessages.ToList();
                    foreach (ChatMessage message in dataBaseMessages)
                    {
                        AppendMessage(message);
                    }
                }

                connection.On<string>("SyncMessageAsync", (message) =>
                {
                    this.Invoke((Action)(() =>
                    {
                        var objs = JsonConvert.DeserializeObject<List<ChatMessage>>(message);
                        foreach (var message in objs)
                        {
                            AppendMessage(message);
                        }
                    }));
                });
            }

            try
            {
                if (connection.State == HubConnectionState.Disconnected)
                {
                    await connection.StartAsync();
                    label1.Text += $"Connected";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                label1.Text = $"{ex.Message}";
            }

            if (connection.State == HubConnectionState.Connected)
            {
                var chatMessage = new ChatMessage()
                {
                    Id = 0
                };
                using (var context = new Context())
                {
                    var entity = context.ChatMessages.OrderByDescending(m => m.Id).FirstOrDefault();
                    if (chatMessage != default) chatMessage = entity;
                }

                await connection.InvokeAsync("SyncMessageAsync", JsonConvert.SerializeObject(chatMessage));
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                form.Close();
            }
        }
    }
}
