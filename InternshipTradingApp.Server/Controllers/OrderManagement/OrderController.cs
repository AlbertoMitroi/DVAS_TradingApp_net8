using AutoMapper;
using InternshipTradingApp.OrderManagementSystem.DTOs;
using InternshipTradingApp.OrderManagementSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Stripe.Climate;

namespace InternshipTradingApp.Server.Controllers.OrderManagement
{
    public class OrderController(IOrderService orderService) : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailsDTO>> GetOrderById(int id)
        {
            var order = await orderService.GetOrderDetailsAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO createOrderDTO)
        {
            await orderService.CreateOrderAsync(createOrderDTO);

            return Ok();
        }
    }
}
