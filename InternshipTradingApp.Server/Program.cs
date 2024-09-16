using InternshipTradingApp.AccountManagement.Data;
using InternshipTradingApp.AccountManagement.Entities;
using InternshipTradingApp.CompanyInventory.SignalR;
using InternshipTradingApp.OrderManagementSystem.SignalR;
using InternshipTradingApp.Server.Extensions;
using InternshipTradingApp.Server.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace InternshipTradingApp.Server
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddIdentityServices(builder.Configuration);
            // Add services to the container.

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("https://127.0.0.1:4200")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });


            builder.Services.AddSignalR();

            var app = builder.Build();


            app.UseStaticFiles();
            app.UseRouting();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(policy =>
                policy.WithOrigins("http://localhost:4200", "https://127.0.0.1:4200")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials());

            app.UseAuthentication();
            app.UseAuthorization();     

            app.MapControllers();

            app.MapHub<UserNotificationHub>("/hubs/userHub");
            app.MapHub<OrderNotificationHub>("/hubs/orderHub");
            app.MapHub<CompanyNotificationHub>("/hubs/companiesHub");

            app.MapFallbackToFile("/index.html");

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<AccountDbContext>();
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
                await context.Database.MigrateAsync();
                await Seed.SeedUsers(userManager, roleManager);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred during migration");
            }


            app.Run();
        }
        }
}
