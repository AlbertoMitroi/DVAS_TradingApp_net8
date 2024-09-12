using AutoMapper;
using InternshipTradingApp.AccountManagement.Data;
using InternshipTradingApp.AccountManagement.DTOs;
using InternshipTradingApp.AccountManagement.Entities;
using InternshipTradingApp.AccountManagement.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InternshipTradingApp.Server.Controllers.AccountManagement
{
    public class AccountController(UserManager<AppUser> userManager, ITokenService tokenService, IMapper mapper, AccountDbContext dbContext) : BaseApiController
    {
        [HttpPost("register")] // account/register
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Email)) return BadRequest("Email is taken");

            var user = mapper.Map<AppUser>(registerDto);

            user.UserName = NormalizeUserName(registerDto.FullName);

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            if (user.Email == null) return Unauthorized("Invalid email");

            return new UserDto
            {
                Username = user.UserName,
                Email = user.Email,
                Token = await tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized("Invalid email");

            var result = await userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!result) return Unauthorized("Invalid password");

            if (user.Email == null) return Unauthorized("Invalid email");

            if (user.UserName == null) return Unauthorized("Invalid username");

            return new UserDto
            {
                Username = user.UserName,
                Email = user.Email,
                Token = await tokenService.CreateToken(user)
            };
        }

        [HttpGet("current-user-details")]
        public async Task<ActionResult<UserDetailsDto>> GetUserDetails()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("User ID not found in the claims.");
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (user.Email == null || user.UserName == null)
                return NotFound("Email or Username not found");

            var transactions = await dbContext.Transactions
                .Include(t => t.BankAccount)
                .Where(t => t.UserId == int.Parse(userId))
                .OrderByDescending(t => t.Date)
                .Select(t => new TransactionDto
                {
                    Id = t.Id,
                    Amount = t.Amount,
                    Date = t.Date,
                    Type = t.Type,
                    Bank = t.Type == "Deposit" ?
                          "Stripe Card"
                        : (t.BankAccount != null ? t.BankAccount.BankName : "Unknown")
                })
                .ToListAsync();

            return Ok(new UserDetailsDto
            {
                Username = user.UserName,
                Email = user.Email,
                Balance = user.Balance,
                Transactions = transactions,
            });
        }


        private async Task<bool> UserExists(string email)
        {
            return await userManager.FindByEmailAsync(email) != null;
        }

        private string NormalizeUserName(string fullName)
        {
            return fullName.Trim().Replace(" ", "_").ToLower();
        }   
    }
}
