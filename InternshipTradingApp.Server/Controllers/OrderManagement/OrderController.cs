using AutoMapper;
using InternshipTradingApp.AccountManagement.Entities;
using InternshipTradingApp.OrderManagementSystem.DTOs;
using InternshipTradingApp.OrderManagementSystem.Entities;
using InternshipTradingApp.OrderManagementSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe.Climate;

namespace InternshipTradingApp.Server.Controllers.OrderManagement
{
    [Authorize]
    public class OrderController(IOrderService orderService, UserManager<AppUser> userManager) : BaseApiController
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
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            createOrderDTO.SetId(user.Id);

            await orderService.CreateOrderAsync(createOrderDTO);

            return Ok(createOrderDTO);
        }

        [HttpPost("cancel/{id}")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            await orderService.UpdateOrderStatusAsync(id, OrderStatus.Canceled);

            return Ok($"OderID: {id} has now status 'Cancel'");
        }
    }
}
