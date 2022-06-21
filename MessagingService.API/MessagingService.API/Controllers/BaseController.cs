using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace MessagingService.API.Controllers
{
    public class BaseController : Controller
    {
        protected string GetUserId()
        {
            var principal = HttpContext.User;
            return principal?.Claims?.SingleOrDefault(p => p.Type == ClaimTypes.Name)?.Value;
        }
    }
}
