using MessagingService.Entities.UserHistory;
using MessagingService.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MessagingService.Extensions
{
    public static class UserHistoryExtensions
    {

        public static UserViewModelResponse ToUserHistoryListViewModel(this IEnumerable<UserHistory> userHistory)
        {
            var response = new UserViewModelResponse();
            response.Body = from item in userHistory select MapUserHistoryToUserViewModel(item);
            return response;
        }

        private static object MapUserHistoryToUserViewModel(UserHistory history)
        {
            return new
            {
                UserName = history.UserName,
                TransactionDate = history.CreatedAt,
                IsSuccess = history.IsSuccess
            };
        }
    }
}
