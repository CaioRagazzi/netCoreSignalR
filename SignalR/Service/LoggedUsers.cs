using Microsoft.EntityFrameworkCore;
using SignalR.DataAccess;
using SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.Service
{
    public class LoggedUsers : ILoggedUsers
    {
        private readonly Context _context;
        public LoggedUsers(Context context)
        {
            _context = context;
        }

        public async Task AddUser(string user)
        {
            var loggedUser = new LoggedUser()
            {
                UserName = user,
            };
            _context.LoggedUsers.Add(loggedUser);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUser(string user)
        {
            var dbUser = await _context.LoggedUsers.Where(r => r.UserName == user).FirstOrDefaultAsync();
            
            if(dbUser != null)
            {
                _context.Remove(dbUser);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<string>> GetUsers()
        {
            var users = await _context.LoggedUsers.ToListAsync();

            return users.Select(r => r.UserName);
        }
    }
}
