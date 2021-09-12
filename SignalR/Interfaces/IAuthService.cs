using SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(User user);
    }
}
