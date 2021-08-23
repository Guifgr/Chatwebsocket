using System;

namespace SimpleChatSolution.Model
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string MessageText { get; set; }
        public string Nick { get; set; }
        public DateTime SentDate { get; set; }

    }
}
