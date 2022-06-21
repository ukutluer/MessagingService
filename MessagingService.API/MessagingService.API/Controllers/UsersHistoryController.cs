using MessagingService.Core.Entities.Base;
using MessagingService.Entities.User;
using MessagingService.Extensions;
using MessagingService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MessagingService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersHistoryController : BaseController
    {
        private IUserHistoryService _userHistoryService;
        public UsersHistoryController(IUserHistoryService userHistoryService)
        {
            _userHistoryService = userHistoryService;

        }

        [HttpGet]
        public IActionResult GetUserHistory()
        {

            var userHistories = _userHistoryService.userHistories(GetUserId());
            if (userHistories == null)
                throw new MessagingServiceApiException("User history cannot found !!!");

            return Json(userHistories.ToUserHistoryListViewModel());
        }

       
    }
}
