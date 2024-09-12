using InternshipTradingApp.AccountManagement.Data;
using InternshipTradingApp.AccountManagement.DTOs;
using InternshipTradingApp.AccountManagement.Entities;
using InternshipTradingApp.AccountManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternshipTradingApp.Server.Controllers.AccountManagement
{
    [Authorize]
    public class TransactionController(IFundsService fundsService, UserManager<AppUser> userManager) : BaseApiController
    {
        [HttpPost("deposit")]
        public async Task<ActionResult> DepositFunds([FromBody] AddFundsDto depositFundsDto)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            await fundsService.AddFundsAsync(user.Id, depositFundsDto.Amount);
            return NoContent();
        }

        [HttpPost("withdraw")]
        public async Task<ActionResult> WithdrawFunds([FromBody] WithdrawFundsDto withdrawFundsDto)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            await fundsService.WithdrawFundsAsync(user.Id, withdrawFundsDto.BankAccountId, withdrawFundsDto.Amount);
            return NoContent();
        }
    }
}
