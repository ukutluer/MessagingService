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
    public class UsersController : BaseController
    {
        private IUserService _userService;
        private readonly ILogger<UsersController> _logger;
        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate(User userParam)
        {
            var user = _userService.Authenticate(userParam.UserName, userParam.Password);
            if (user == null)
                throw new MessagingServiceApiException("User & Password wrong !!!");

            return Json(user.ToUserViewModel());
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Json(users.ToUserListViewModel());
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register(User user)
        {
            if (_userService.IsUserExist(user))
                throw new MessagingServiceApiException("User already exist.");
            _userService.InsertAsync(user);
            return Json(new BaseMessagingServiceResponse());
        }
    }
}
