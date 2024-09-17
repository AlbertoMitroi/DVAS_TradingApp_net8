using InternshipTradingApp.OrderManagementSystem.Entities;
using InternshipTradingApp.OrderManagementSystem.Interfaces;
using InternshipTradingApp.OrderManagementSystem.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternshipTradingApp.OrderManagementSystem.Data
{
    public class OrderRepository(OrderDbContext context, IOrderNotificationService orderNotification) : IOrderRepository
    {
        public async Task<Order> GetByIdAsync(int id)
        {
            return await context.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await context.Orders.ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllPendingAsync()
        {
            return await context.Orders
                .Where(order => order.Status == OrderStatus.Pending)
                .ToListAsync();
        }

        public async Task AddAsync(Order order)
        {
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
            await orderNotification.SendOrderDetailsAsync(order.CustomerId.ToString());
            
        }

        public async Task UpdateAsync(Order order)
        {
            context.Orders.Update(order);
            await context.SaveChangesAsync();
            await orderNotification.SendOrderDetailsAsync(order.CustomerId.ToString());
        }

        public async Task DeleteAsync(int id)
        {
            var order = await GetByIdAsync(id);
            if (order != null)
            {
                context.Orders.Remove(order);
                await context.SaveChangesAsync();
                await orderNotification.SendOrderDetailsAsync(order.CustomerId.ToString());
            }
        }
    }
}
