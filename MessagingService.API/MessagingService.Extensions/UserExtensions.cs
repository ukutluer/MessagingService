using MessagingService.Entities.User;
using MessagingService.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MessagingService.Extensions
{
    public static class UserExtensions
    {
        public static UserViewModelResponse ToUserViewModel(this User user)
        {
            var response = new UserViewModelResponse();
            response.Body = MapUserToUserViewModel(user);
            return response;
        }

        public static UserViewModelResponse ToUserListViewModel(this IEnumerable<User> userList)
        {
            var response = new UserViewModelResponse();
            response.Body = from item in userList select MapUserToUserViewModel(item);
            return response;
        }

        private static object MapUserToUserViewModel(User user)
        {
            return new
            {
                UserName = user.UserName,
                Token = user.Token
            };
        }
    }
}
