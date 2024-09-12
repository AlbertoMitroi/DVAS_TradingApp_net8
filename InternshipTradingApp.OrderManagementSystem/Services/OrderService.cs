using AutoMapper;
using InternshipTradingApp.OrderManagementSystem.DTOs;
using InternshipTradingApp.OrderManagementSystem.Entities;
using InternshipTradingApp.OrderManagementSystem.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternshipTradingApp.OrderManagementSystem.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderDetailsDTO> GetOrderDetailsAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return _mapper.Map<OrderDetailsDTO>(order);
        }

        public async Task CreateOrderAsync(CreateOrderDTO dto)
        {
            var order = _mapper.Map<Order>(dto);
            await _orderRepository.AddAsync(order);
        }

        public async Task<IEnumerable<OrderDetailsDTO>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDetailsDTO>>(orders);
        }

        public async Task UpdateOrderStatusAsync(int orderId, OrderStatus newStatus)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order != null)
            {
                order.Status = newStatus;
                await _orderRepository.UpdateAsync(order);
            }
        }
    }
}
