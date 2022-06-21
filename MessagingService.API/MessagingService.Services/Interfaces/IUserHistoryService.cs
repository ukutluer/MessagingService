using MessagingService.Entities.UserHistory;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingService.Services.Interfaces
{
    public interface IUserHistoryService
    {
        Task Insert(string userName, bool isSuccess);

        IQueryable<UserHistory> userHistories(string userName);

    }
}
