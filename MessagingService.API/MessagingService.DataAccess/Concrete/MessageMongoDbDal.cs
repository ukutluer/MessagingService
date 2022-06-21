using MessagingService.Core.Entities.Settings;
using MessagingService.DataAccess.Abstract;
using MessagingService.DataAccess.Base;
using MessagingService.Entities.Message;
using Microsoft.Extensions.Options;

namespace MessagingService.DataAccess.Concrete
{
    public class MessageMongoDbDal : MongoDbRepositoryBase<Message>, IMessageDal
    {
        public MessageMongoDbDal(IOptions<MongoDbSettings> options) : base(options)
        {
        }
    }
}
