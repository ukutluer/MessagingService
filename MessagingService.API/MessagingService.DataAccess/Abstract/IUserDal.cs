using MessagingService.Entities.User;

namespace MessagingService.DataAccess.Abstract
{
    public interface IUserDal : IRepository<User, string>
    {

    }
}
