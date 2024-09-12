using InternshipTradingApp.AccountManagement.Interfaces;
using AccountServices = InternshipTradingApp.AccountManagement.Services;
using InternshipTradingApp.CompanyInventory.Infrastructure.CompanyDataAccess;
using Microsoft.EntityFrameworkCore;
using InternshipTradingApp.CompanyInventory;
using InternshipTradingApp.AccountManagement.Data;
using InternshipTradingApp.AccountManagement.Helpers;
using Microsoft.OpenApi.Models;
using Stripe;
using InternshipTradingApp.AccountManagement.Services;
using InternshipTradingApp.OrderManagementSystem.Data;
using InternshipTradingApp.OrderManagementSystem;

namespace InternshipTradingApp.Server.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddControllers();
            services.AddCompanyInventoryModule();
            services.AddOrderManagementModule();

            services.AddDbContext<CompanyDbContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            services.AddDbContext<AccountDbContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            services.AddDbContext<OrderDbContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            StripeConfiguration.ApiKey = config["Stripe:SecretKey"];

            services.AddCors();
            
            services.AddScoped<ITokenService, AccountServices.TokenService>();
            services.AddScoped<IBankAccountService, AccountServices.BankAccountService>();
            services.AddScoped<IFundsService, AccountServices.FundsService>();
            services.AddScoped<IUserNotificationService, UserNotificationService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddEndpointsApiExplorer();

            services.AddSignalR();

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            return services;
        }
    }
}
