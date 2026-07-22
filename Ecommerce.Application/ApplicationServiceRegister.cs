using Ecommerce.Application.Contracts;
using Ecommerce.Application.Profiles;
using Ecommerce.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application
{
    public static class ApplicationServiceRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(c=> { }, typeof(ApplicationServiceRegister).Assembly);

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBasketservice, BasketService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
