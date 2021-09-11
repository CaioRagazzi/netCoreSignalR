using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SignalR.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SignalR.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BroadcastHub : Hub
    {
        private readonly ILoggedUsers _loggedUsers;

        public BroadcastHub(ILoggedUsers loggedUsers)
        {
            _loggedUsers = loggedUsers;
        }
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("--> Connection stabilished " + Context.ConnectionId);
            _loggedUsers.AddUser(Context.UserIdentifier).Wait();
            SendConnectedUsers().Wait();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("--> disconected " + Context.ConnectionId);
            _loggedUsers.RemoveUser(Context.UserIdentifier).Wait();
            SendConnectedUsers().Wait();
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessageAsync(string message)
        {
            var routeOb = JsonConvert.DeserializeObject<dynamic>(message);
            string toClient = routeOb.To;
            string textMessage = routeOb.Message;
            Console.WriteLine("Message received on: ", Context.ConnectionId);

            if (toClient == string.Empty)
            {
                await Clients.All.SendAsync("ReceiveMessage", textMessage);
            }
            else
            {
                await Clients.User(toClient).SendAsync("ReceiveMessage", textMessage);
            }
        }

        public async Task SendConnectedUsers()
        {
            var loggedUsers = await _loggedUsers.GetUsers();
            if (loggedUsers.Count() == 0)
            {
                await Clients.All.SendAsync("LoggedUsers", "empty");
            } else
            {
                await Clients.All.SendAsync("LoggedUsers", loggedUsers);
            }
        }
    }
}
