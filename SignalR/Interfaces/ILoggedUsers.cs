using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.Interfaces
{
    public interface ILoggedUsers
    {
        Task AddUser(string user);
        Task RemoveUser(string user);
        Task<IEnumerable<string>> GetUsers();
    }
}
