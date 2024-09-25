
using AutoMapper;
using InternshipTradingApp.OrderManagementSystem.Data;
using InternshipTradingApp.OrderManagementSystem.DTOs;
using InternshipTradingApp.OrderManagementSystem.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InternshipTradingApp.OrderManagementSystem.Services
{
    public class OrderNotificationService(
        OrderDbContext orderDbContext,
        IHubContext<OrderNotificationHub> hubContext,
        ILogger<OrderNotificationService> logger,
        IMapper mapper) : IOrderNotificationService
    {
        public async Task SendOrderDetailsAsync(string userId)
        {
            logger.LogInformation($"Fetching orders for user ID: {userId}");

            var orders = await orderDbContext.Orders.ToListAsync();
            var ordersDto = mapper.Map<List<OrderDetailsDTO>>(orders);

            await hubContext.Clients.All.SendAsync("ReceiveOrders", ordersDto);
        }
    }
}
