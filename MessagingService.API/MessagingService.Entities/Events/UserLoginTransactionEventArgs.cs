using System;

namespace MessagingService.Entities.Events
{
    public class UserLoginTransactionEventArgs : EventArgs
    {
        public bool isSuccess { get; set; }
        public string userName { get; set; }

        public UserLoginTransactionEventArgs(bool isSucces,string user)
        {
            isSuccess = isSucces;
            userName = user; 
        }
    }
}
