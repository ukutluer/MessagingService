using MessagingService.Core.Entities.Base;
using MessagingService.Entities.Message;
using MessagingService.Extensions;
using MessagingService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MessagingService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MessageController : BaseController
    {
        private IMessageService _messageService;
        private IUserService _userService;

        public MessageController(IMessageService messageService, IUserService userService)
        {
            _messageService = messageService;
            _userService = userService;
        }

        [HttpPost("Send")]
        public IActionResult SendMessage(Message message)
        {
            var userId = GetUserId();
            message.from = userId;
            if(!CheckValidUserForSendingMessage(message.to))
                throw new MessagingServiceApiException("Couldnt find user to send message !!!");

            _messageService.SendMessage(message);
            return Json(new BaseMessagingServiceResponse());
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var messages = _messageService.ReadAllMessageAsync(GetUserId());
            return Json(messages.ToUserMessageViewModel());
        }

        [HttpGet("{to}")]
        public IActionResult GetMessages(string to)
        {
            var messages = _messageService.ReadUserMessageAsync(GetUserId(), to);
            return Json(messages.ToUserMessageViewModel());
        }

        private bool CheckValidUserForSendingMessage(string userName)
        {
            return _userService.IsUserExist(new Entities.User.User() { UserName = userName });
        }
    }
}
