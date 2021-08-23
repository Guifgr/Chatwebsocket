using System;
using System.Threading.Tasks;
using ChatMessageServer.Model;
using ChatMessageServer.Repository;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace ChatMessageServer.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatMessageRepository _repository;
        public ChatHub(IChatMessageRepository repository)
        {
            _repository = repository;
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine("--> Connection established "+ Context.ConnectionId);
            Clients.Client(Context.ConnectionId).SendAsync("ReceiveConnID ", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public async Task SendMessageAsync(string chatMessage)
        {
            var message = JsonConvert.DeserializeObject<ChatMessage>(chatMessage);
            var entityMessage = await _repository.InsertAsync(message);
            chatMessage = JsonConvert.SerializeObject(entityMessage);
            Console.WriteLine(chatMessage);
            await Clients.All.SendAsync("SendMessageAsync", chatMessage);
        }
        
        public async Task SyncMessageAsync(string chatMessage)
        {
            var message = JsonConvert.DeserializeObject<ChatMessage>(chatMessage);
            if (message != null)
            {
                var entityMessages = await _repository.GetNewMessagesAsync(message.Id);
                foreach (var entityMessage in entityMessages) 
                {
                    Console.WriteLine($"Sync message {entityMessage.Id} to an client");
                }
                chatMessage = JsonConvert.SerializeObject(entityMessages);
                await Clients.Caller.SendAsync("SyncMessageAsync", chatMessage);
            }
        }
    }
}