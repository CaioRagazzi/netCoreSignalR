using Microsoft.AspNetCore.Mvc;
using SignalR.Interfaces;
using SignalR.Models;

namespace SignalR.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public ActionResult<dynamic> Login(User user)
        {
            var token = _authService.GenerateToken(user);
            user.Password = "";
            return new
            {
                user = user,
                token = token
            };
        }
    }
}
