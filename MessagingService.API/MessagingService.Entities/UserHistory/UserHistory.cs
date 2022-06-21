using MessagingService.Core.Entities.Abstract;

namespace MessagingService.Entities.UserHistory
{
    public class UserHistory : MongoDbEntity
    {
        public string UserName { get; set; }
        public bool IsSuccess { get; set; }
    }
}