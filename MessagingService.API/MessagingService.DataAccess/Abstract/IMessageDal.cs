using MessagingService.Entities.Message;

namespace MessagingService.DataAccess.Abstract
{
    public interface IMessageDal : IRepository<Message, string>
    {

    }
}
