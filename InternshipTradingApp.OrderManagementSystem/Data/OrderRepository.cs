using InternshipTradingApp.OrderManagementSystem.Entities;
using InternshipTradingApp.OrderManagementSystem.Interfaces;
using InternshipTradingApp.OrderManagementSystem.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternshipTradingApp.OrderManagementSystem.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;
        private readonly IOrderNotificationService _orderNotification;

        public OrderRepository(OrderDbContext context, IOrderNotificationService orderNotification)
        {
            _context = context;
            _orderNotification = orderNotification;
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            await _orderNotification.SendOrderDetailsAsync(order.CustomerId.ToString());
            
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            await _orderNotification.SendOrderDetailsAsync(order.CustomerId.ToString());
        }

        public async Task DeleteAsync(int id)
        {
            var order = await GetByIdAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                await _orderNotification.SendOrderDetailsAsync(order.CustomerId.ToString());
            }
        }
    }
}
