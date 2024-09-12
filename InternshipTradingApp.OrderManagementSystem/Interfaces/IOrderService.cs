using InternshipTradingApp.OrderManagementSystem.DTOs;
using InternshipTradingApp.OrderManagementSystem.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternshipTradingApp.OrderManagementSystem.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDetailsDTO> GetOrderDetailsAsync(int id);
        Task CreateOrderAsync(CreateOrderDTO dto);
        Task<IEnumerable<OrderDetailsDTO>> GetAllOrdersAsync();
        Task UpdateOrderStatusAsync(int orderId, OrderStatus newStatus);
    }
}
