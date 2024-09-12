using ClosedXML.Excel;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Wordprocessing;
using InternshipTradingApp.AccountManagement.DTOs;
using InternshipTradingApp.AccountManagement.Entities;
using InternshipTradingApp.AccountManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternshipTradingApp.Server.Controllers.AccountManagement
{
    [Authorize]
    public class BankAccountController(IBankAccountService bankAccountService, UserManager<AppUser> userManager) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<BankAccountResponseDto>>> GetBankAccounts()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var accounts = await bankAccountService.GetBankAccountsForUser(user.Id);
            return Ok(accounts);
        }

        [HttpPost]
        public async Task<ActionResult> AddBankAccount([FromBody] BankAccountDto bankAccountDto)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();
            if (bankAccountDto.BankName.Length < 10) return BadRequest("Bank name must be greater than 10 characters");
            if (bankAccountDto.IBAN.Length < 12) return BadRequest("IBAN must be greater than 10 caracters");

            try
            {
                await bankAccountService.AddBankAccount(user.Id, bankAccountDto);
                return Ok("Bank Account was added.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", details = ex.Message });
            }
        }

    }
}
