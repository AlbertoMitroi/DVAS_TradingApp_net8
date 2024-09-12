using InternshipTradingApp.AccountManagement.DTOs;
using InternshipTradingApp.AccountManagement.Entities;
using InternshipTradingApp.AccountManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InternshipTradingApp.Server.Controllers.AccountManagement
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FundsController(IFundsService fundsService, UserManager<AppUser> userManager) : ControllerBase
    {
        [HttpPost("add")]
        public async Task<ActionResult> AddFunds([FromBody] AddFundsDto addFundsDto)
        {
            if (addFundsDto.Amount <= 0)
                return BadRequest(new { message = "Amount must be greater than zero." });

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            var user = await userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return NotFound(new { message = "User not found." });

            try
            {
                await fundsService.AddFundsAsync(userId, addFundsDto.Amount);
                return Ok(new { message = "Funds added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while adding funds.", details = ex.Message });
            }
        }

        [HttpPost("withdraw")]
        public async Task<ActionResult> WithdrawFunds([FromBody] WithdrawFundsDto withdrawFundsDto)
        {
            if (withdrawFundsDto.Amount <= 0)
                return BadRequest(new { message = "Amount must be greater than zero." });

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            var user = await userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return NotFound(new { message = "User not found." });

            try
            {
                await fundsService.WithdrawFundsAsync(userId, withdrawFundsDto.BankAccountId ,withdrawFundsDto.Amount);
                return Ok(new { message = "Withdrawal successful." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("balance")]
        public async Task<ActionResult<decimal>> GetBalance()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            var user = await userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                return NotFound(new { message = "User not found." });

            return Ok(user.Balance);
        }
    }
}
