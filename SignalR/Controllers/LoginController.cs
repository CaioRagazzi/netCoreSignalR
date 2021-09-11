using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        [HttpPost]
        public ActionResult<dynamic> Login(User user)
        {
            // Gera o Token
            var token = Settings.GenerateToken(user);

            // Oculta a senha
            user.Password = "";

            // Retorna os dados
            return new
            {
                user = user,
                token = token
            };
        }
    }
}
