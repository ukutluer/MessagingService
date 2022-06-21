using MessagingService.Core.Entities.Settings;
using MessagingService.DataAccess.Abstract;
using MessagingService.DataAccess.Base;
using MessagingService.Entities.UserHistory;
using Microsoft.Extensions.Options;

namespace MessagingService.DataAccess.Concrete
{
    public class UserHistoryMongoDbDal : MongoDbRepositoryBase<UserHistory>, IUserHistoryDal
    {
        public UserHistoryMongoDbDal(IOptions<MongoDbSettings> options) : base(options)
        {
        }
    }
}
