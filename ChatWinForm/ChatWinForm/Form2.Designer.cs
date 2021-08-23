
using System;

namespace ChatWinForm
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ChatRichText = new System.Windows.Forms.RichTextBox();
            this.NewMessage = new System.Windows.Forms.RichTextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ChatRichText
            // 
            this.ChatRichText.Location = new System.Drawing.Point(12, 28);
            this.ChatRichText.Name = "ChatRichText";
            this.ChatRichText.ReadOnly = true;
            this.ChatRichText.Size = new System.Drawing.Size(284, 357);
            this.ChatRichText.TabIndex = 0;
            this.ChatRichText.Text = "";
            this.ChatRichText.TextChanged += new System.EventHandler(this.ChatRichText_TextChanged);
            // 
            // NewMessage
            // 
            this.NewMessage.Location = new System.Drawing.Point(12, 391);
            this.NewMessage.Name = "NewMessage";
            this.NewMessage.Size = new System.Drawing.Size(194, 47);
            this.NewMessage.TabIndex = 1;
            this.NewMessage.Text = "";
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(215, 391);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(75, 47);
            this.SendButton.TabIndex = 2;
            this.SendButton.Text = "➤";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 15);
            this.label1.TabIndex = 3;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.NewMessage);
            this.Controls.Add(this.ChatRichText);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox ChatRichText;
        private System.Windows.Forms.RichTextBox NewMessage;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.Label label1;

    }
}