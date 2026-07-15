using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Entities.Products;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.DataSeeding
{
    internal class CatalogDataSeeder(StoreDbContext dbContext , ILogger<CatalogDataSeeder> logger) : IDataSeeder
    {
        
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                var PendingMigrations = await dbContext.Database.GetPendingMigrationsAsync(ct);
                if (PendingMigrations.Any())
                    await dbContext.Database.MigrateAsync(ct);

                var seedroot = Path.Combine(AppContext.BaseDirectory, "DataSeed");
                
               await SeedIfEmptyAsync<ProductBrand , int>(seedroot,"brands.json",ct);
                await SeedIfEmptyAsync<ProductType, int>(seedroot, "types.json", ct);
                await SeedIfEmptyAsync<Product, int>(seedroot, "products.json", ct);

                 var result=await dbContext.SaveChangesAsync(ct);

                if (result > 0)
                    logger.LogInformation($"{result} rows added");
                else
                    logger.LogInformation($"Database already seeded");



            }
            catch 
            {
            }
        }

        private async Task SeedIfEmptyAsync<T,Tkey>(string rootpath , string filename, CancellationToken ct) where T : BaseEntity<Tkey>
        {
            if (await dbContext.Set<T>().AnyAsync())
            {
                logger.LogInformation("Table already has data");
                return;
            } 

            var filepath = Path.Combine(rootpath, filename);

            if (!File.Exists(filepath))
            {
                logger.LogInformation($"file {filename} is not exists");
                return;
            }
          using  var filestream = File.OpenRead(filepath);

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

          var items =  await  JsonSerializer.DeserializeAsync<List<T>>(filestream , options , ct);

            if(items?.Any() ?? false)
                dbContext.Set<T>().AddRange(items);

 


    }
    }
}

