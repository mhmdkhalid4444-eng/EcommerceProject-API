<<<<<<< HEAD

=======
>>>>>>> master
using Ecommerce.API.Extensions;
using Ecommerce.Application;
using Ecommerce.Application.Profiles;
using Ecommerce.Domain.Contracts;
using Ecommerce.Infrastructure;
using Ecommerce.Infrastructure.Identity.Service;
using Microsoft.Extensions.FileProviders;
<<<<<<< HEAD
=======
using Microsoft.OpenApi.Models;
>>>>>>> master

namespace Ecommerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
<<<<<<< HEAD

            builder.Services.AddControllers();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.Configure<UrlSettings>(builder.Configuration.GetSection("UrlSettings"));
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

=======
            builder.Services.AddControllers();

            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();

            builder.Services.Configure<UrlSettings>(
                builder.Configuration.GetSection("UrlSettings"));

            builder.Services.Configure<JwtSettings>(
                builder.Configuration.GetSection("Jwt"));

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Ecommerce API",
                    Version = "v1",
                    Description = "E-Commerce Web API built with ASP.NET Core"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your JWT token in the format: Bearer {your token}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
>>>>>>> master

            var app = builder.Build();

            await app.SeedAndMigrateDataAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles(new StaticFileOptions
            {
<<<<<<< HEAD
                FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Files")),
=======
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(builder.Environment.ContentRootPath, "Files")),
>>>>>>> master
                RequestPath = "/Files"
            });

            app.UseHttpsRedirection();
<<<<<<< HEAD
            
            app.UseAuthorization();

=======

            app.UseAuthentication();
            app.UseAuthorization();
>>>>>>> master

            app.MapControllers();

            app.Run();
        }
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> master
