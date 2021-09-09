using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace SignalR.Hubs
{
    public class BroadcastHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("--> Connection stabilished " + Context.ConnectionId);
            Clients.Client(Context.ConnectionId).SendAsync("ReceiveConnId", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public async Task SendMessageAsync(string message)
        {
            var routeOb = JsonConvert.DeserializeObject<dynamic>(message);
            string toClient = routeOb.To;
            Console.WriteLine("Message received on: ", Context.ConnectionId);

            if (toClient == string.Empty)
            {
                await Clients.All.SendAsync("ReceiveMessage", message);
            }
            else
            {
                await Clients.Client(toClient).SendAsync("ReceiveMessage", toClient);
            }
        }
    }
}
