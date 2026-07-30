using Ecommerce.Application.Contracts;
using Ecommerce.Application.Services;
using Ecommerce.Domain.Contracts;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.DataSeeding;
using Ecommerce.Infrastructure.Identity.Data;
using Ecommerce.Infrastructure.Identity.Entities;
using Ecommerce.Infrastructure.Identity.Service;
using Ecommerce.Infrastructure.Payments;
using Ecommerce.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration )
        {
            var jwtSettings = configuration.GetSection("JWT").Get<JwtSettings>()
               ?? throw new InvalidOperationException("JwtSettings is not configured.");

            services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<StoreIdentitydbcontext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));

            });


            services.AddKeyedScoped<IDataSeeder, CatalogDataSeeder>("Catalog");
            services.AddKeyedScoped<IDataSeeder, IdentityDataSeeder>("Identity");
            services.AddScoped<IUnitOfWork,UnitOfWork>();

            services.AddSingleton<IConnectionMultiplexer>(config =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")!);
            });
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddSingleton<ICacheRepository, CacheRepository>();

            services.AddIdentityCore<ApplicationUser>()
                   .AddRoles<IdentityRole>()
                   .AddEntityFrameworkStores<StoreIdentitydbcontext>();

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ITokenService, TokenService>();
            
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.SaveToken = true;
                opt.RequireHttpsMetadata = true;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    RequireSignedTokens = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };
            });
<<<<<<< HEAD
=======
            services.Configure<PaymentGatewaySettings>(
            configuration.GetSection("Stripe"));
>>>>>>> master
            services.AddSingleton<IPaymentGateway, StripePaymentGateway>();
            return services;
        }
    }
}
