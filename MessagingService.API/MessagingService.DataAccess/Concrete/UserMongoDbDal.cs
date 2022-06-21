using MessagingService.Core.Entities.Settings;
using MessagingService.DataAccess.Abstract;
using MessagingService.DataAccess.Base;
using MessagingService.Entities.User;
using Microsoft.Extensions.Options;

namespace MessagingService.DataAccess.Concrete
{
    public class UserMongoDbDal : MongoDbRepositoryBase<User>, IUserDal
    {
        public UserMongoDbDal(IOptions<MongoDbSettings> options) : base(options)
        {
        }
    }
}
