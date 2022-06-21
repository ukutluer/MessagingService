using MessagingService.Core.Entities.Settings;
using MessagingService.DataAccess.Abstract;
using MessagingService.Entities.UserHistory;
using MessagingService.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingService.Services.Implementations
{
    public class UserHistoryService : IUserHistoryService
    {
        private readonly AppSettings _appSettings;
        private readonly IUserHistoryDal _userHistoryDal;


        public UserHistoryService(IOptions<AppSettings> appSettings, IUserHistoryDal userHistoryDal)
        {
            _appSettings = appSettings.Value;
            _userHistoryDal = userHistoryDal;
        }


        public async Task Insert(string userName, bool isSuccess)
        {
            await _userHistoryDal.AddAsync(new UserHistory()
            {
                IsSuccess = isSuccess,
                UserName = userName
            });
        }

        public IQueryable<UserHistory> userHistories(string userName)
        {
            return _userHistoryDal.Get(q => q.UserName == userName);
        }
    }
}