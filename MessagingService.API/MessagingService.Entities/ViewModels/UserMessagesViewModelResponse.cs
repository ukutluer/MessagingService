using MessagingService.Core.Entities.Base;
using System.Collections.Generic;

namespace MessagingService.Entities.ViewModels
{
    public class UserMessagesViewModelResponse : BaseMessagingServiceResponse
    {
        public List<Message.Message> MessageList { get; set; }
    }
}
