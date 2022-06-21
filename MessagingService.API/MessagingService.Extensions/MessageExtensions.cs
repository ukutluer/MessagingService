using MessagingService.Entities.Message;
using MessagingService.Entities.ViewModels;
using System.Linq;

namespace MessagingService.Extensions
{
    public static class MessageExtensions
    {
        public static UserMessagesViewModelResponse ToUserMessageViewModel(this IQueryable<Message> messageList)
        {
            var response = new UserMessagesViewModelResponse();
            response.Body = from message in messageList
                            select new
                            {
                                messageFrom = message.@from,
                                messageTo = message.to,
                                message = message.content,
                                date = message.CreatedAt
                            };
            return response;
        }

    }
}
