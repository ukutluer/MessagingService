using MessagingService.Core.Entities.Abstract;

namespace MessagingService.Entities.User
{
    public class User : MongoDbEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
