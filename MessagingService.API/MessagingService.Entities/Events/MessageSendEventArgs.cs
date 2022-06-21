using System;

namespace MessagingService.Entities.Events
{
    public class MessageSendEventArgs : EventArgs
    {
        public string from { get; set; }
        public string to { get; set; }
        public string content { get; set; }

        public MessageSendEventArgs(string from, string to, string content)
        {
            this.from = from;
            this.to = to;
            this.content = content;
        }
    }
}
