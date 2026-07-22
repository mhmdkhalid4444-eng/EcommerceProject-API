using Ecommerce.Domain.Contracts;
using Ecommerce.Infrastructure.Identity.Data;
using Ecommerce.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.DataSeeding
{
    internal class IdentityDataSeeder : IDataSeeder
    {
        private readonly StoreIdentitydbcontext _dbcontext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<IdentityDataSeeder> _logger;
        public IdentityDataSeeder(StoreIdentitydbcontext dbcontext,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, ILogger<IdentityDataSeeder> logger)
        {
            _dbcontext = dbcontext;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;

        }
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                var pendingMigrations = await _dbcontext.Database.GetPendingMigrationsAsync(ct);
                if (pendingMigrations.Any())
                    await _dbcontext.Database.MigrateAsync(ct);

                if (!await _roleManager.Roles.AnyAsync(ct))
                {

                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }

                if (!await _userManager.Users.AnyAsync(ct))
                {

                    var admin = new ApplicationUser()
                    {
                        DisplayName = "seif",
                        Email = "seif@gmail.com",
                        UserName = "seif",
                        PhoneNumber = "1234567890",
                    };
                    var result = await _userManager.CreateAsync(admin, "P@ssword123");
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(admin, "SuperAdmin");


                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            _logger.LogError("{Code}: {Description}", error.Code, error.Description);
                        }
                    }
                }
            }


            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while seeding identity data");
                return;
            }



            }
        }
    }

