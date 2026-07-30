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
    internal class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.Property(o => o.SubTotal).HasColumnType("decimal(10,2)");
            builder.Property(o => o.BuyerEmail).IsRequired().HasMaxLength(256);
            builder.Property(o => o.Status).HasConversion<string>().HasMaxLength(50);

            builder.HasMany(o => o.Items)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.DeliveryMethod)
                   .WithMany()
                   .HasForeignKey(o => o.DeliveryMethodId);

            builder.OwnsOne(o => o.ShipToAddress,address =>
            {
                address.Property(x => x.FirstName).HasMaxLength(50);
                address.Property(a => a.LastName).HasMaxLength(50);
                address.Property(a => a.Street).HasMaxLength(50);
                address.Property(a => a.City).IsRequired().HasMaxLength(50);
                address.Property(a => a.Country).HasMaxLength(50);
         
            });


        }
    }
}
