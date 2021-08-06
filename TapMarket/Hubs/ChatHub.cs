namespace TapMarket.Hubs
{
    using Microsoft.AspNetCore.SignalR;
    using System.Threading.Tasks;
    using TapMarket.Data.Models;

    public class ChatHub : Hub
    {
        public async Task SendMessage(Message message)
            => await Clients.All.SendAsync("receiveMessages", message);
    }
}
