using MessagingService.Entities.Events;
using MessagingService.Entities.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessagingService.Services.Interfaces
{
    public interface IUserService
    {
        event EventHandler<UserLoginTransactionEventArgs> OnUserLoginTransactionProcessed;
        User Authenticate(string name, string password);
        IEnumerable<User> GetAll();
        Task InsertAsync(User user);
        bool IsUserExist(User user);
    }
}
