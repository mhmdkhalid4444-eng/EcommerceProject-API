using Ecommerce.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Data.Configurations
{
    internal class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
           
            builder.Property(o => o.Price).HasColumnType("decimal(10,2)");
            builder.OwnsOne(o => o.Product, product =>
            {
                product.Property(p => p.ProductName).HasMaxLength(100);
                product.Property(p => p.PictureUrl).HasMaxLength(200);
               
            });
        }
    }
}
