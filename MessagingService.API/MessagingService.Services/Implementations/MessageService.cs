using MessagingService.Core.Entities.Settings;
using MessagingService.DataAccess.Abstract;
using MessagingService.Entities.Events;
using MessagingService.Entities.Message;
using MessagingService.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingService.Services.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly AppSettings _appSettings;
        private readonly IMessageDal _messageDal;
        private readonly IAuditService _auditService;

        public event EventHandler<MessageSendEventArgs> OnMessageSended;

        public MessageService(IOptions<AppSettings> appSettings, IMessageDal messageDal, IAuditService auditService)
        {
            _appSettings = appSettings.Value;
            _messageDal = messageDal;
            _auditService = auditService;
            _auditService.Subscribe(this);
        }

        public IQueryable<Message> ReadAllMessageAsync(string user)
        {
            return _messageDal.Get(q => q.to == user || q.from == user).OrderByDescending(q => q.CreatedAt);
        }

        public IQueryable<Message> ReadUserMessageAsync(string user, string to)
        {
            return _messageDal.Get(q => ((q.to == user && q.from == to) || 
                                         (q.to == to && q.from == user))).OrderByDescending(q => q.CreatedAt);
        }

        public async Task SendMessage(Message message)
        {

            await _messageDal.AddAsync(message);
            if(OnMessageSended != null)
                OnMessageSended(this, new MessageSendEventArgs(message.from, message.to, message.content));
        }
    }
}