using Microsoft.EntityFrameworkCore;
using SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.DataAccess
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
          : base(options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<LoggedUser> LoggedUsers { get; set; }
    }
}
