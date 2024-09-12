using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace InternshipTradingApp.OrderManagementSystem.SignalR
{
    public class OrderNotificationHub : Hub
    {
        public async Task SendOrderUpdate(string message)
        {
            await Clients.All.SendAsync("ReceiveOrderUpdate", message);
        }
    }
}
