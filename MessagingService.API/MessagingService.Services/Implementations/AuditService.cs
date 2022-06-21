using MessagingService.Entities.Events;
using MessagingService.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace MessagingService.Services.Implementations
{
    public class AuditService : IAuditService
    {
        private readonly ILogger<AuditService> _logger;
        private readonly IUserHistoryService _userHistoryService;

        public AuditService(ILogger<AuditService> logger, IUserHistoryService userHistoryService)
        {
            _logger = logger;
            _userHistoryService = userHistoryService;
            
        }
        public void Subscribe(IUserService userService)
        {
            userService.OnUserLoginTransactionProcessed += WriteAuditLog;
        }

        public void Subscribe(IMessageService messageService)
        {
            messageService.OnMessageSended += WriteAuditLog;
        }


        private void WriteAuditLog(object sender, UserLoginTransactionEventArgs e)
        {
            try
            {
                _userHistoryService.Insert(e.userName, e.isSuccess);
                _logger.LogInformation($"AUDIT LOG: User: {e.userName} login attempt is successed : {e.isSuccess} ");
            }
            catch (Exception)
            {
                //do nothing
            }

        }

        private void WriteAuditLog(object sender, MessageSendEventArgs e)
        {
            try
            {
                _logger.LogInformation($"AUDIT LOG: User: {e.from} send message to {e.to}, write message: {e.content}");
            }
            catch (Exception)
            {
                //do nothing
            }

        }


    }
}
