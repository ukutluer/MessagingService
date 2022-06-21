using MessagingService.Entities.Events;
using MessagingService.Entities.Message;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingService.Services.Interfaces
{
    public interface IMessageService
    {
        Task SendMessage(Message message);
        IQueryable<Message> ReadAllMessageAsync(string from);
        IQueryable<Message> ReadUserMessageAsync(string from, string to);

        event EventHandler<MessageSendEventArgs> OnMessageSended;
    }
}
