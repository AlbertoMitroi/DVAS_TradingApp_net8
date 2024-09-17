using AutoMapper;
using InternshipTradingApp.AccountManagement.Entities;
using InternshipTradingApp.AccountManagement.Interfaces;
using InternshipTradingApp.OrderManagementSystem.DTOs;
using InternshipTradingApp.OrderManagementSystem.Entities;
using InternshipTradingApp.OrderManagementSystem.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;

namespace InternshipTradingApp.OrderManagementSystem.Services
{
    public class OrderService(IOrderRepository orderRepository, IMapper mapper, UserManager<AppUser> userManager, IFundsService fundsService, IOrderMatchingEngineService orderMatchingEngine) : IOrderService
    {
        public async Task<OrderDetailsDTO> GetOrderDetailsAsync(int id)
        {
            var order = await orderRepository.GetByIdAsync(id);
            return mapper.Map<OrderDetailsDTO>(order);
        }

        public async Task CreateOrderAsync(CreateOrderDTO dto)
        {
            var user = await userManager.FindByIdAsync(dto.CustomerId.ToString());
            if (user == null) throw new Exception("User not found for create order.");

            var valueOrder = dto.Quantity * dto.Price;

            if (user.Balance <= valueOrder) throw new Exception("User balance is less than value Order.");

            var order = mapper.Map<Order>(dto);
            await orderRepository.AddAsync(order);
            await orderMatchingEngine.MatchOrderAsync(order);
        }

        public async Task<IEnumerable<OrderDetailsDTO>> GetAllOrdersAsync()
        {
            var orders = await orderRepository.GetAllAsync();
            return mapper.Map<IEnumerable<OrderDetailsDTO>>(orders);
        }

        public async Task UpdateOrderStatusAsync(int orderId, OrderStatus newStatus)
        {
            var order = await orderRepository.GetByIdAsync(orderId);
            if (order != null)
            {
                order.Status = newStatus;
                await orderRepository.UpdateAsync(order);
            }
        }
    }
}
