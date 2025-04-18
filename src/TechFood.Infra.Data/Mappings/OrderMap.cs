using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFood.Domain.Entities;

namespace TechFood.Infra.Data.Mappings;

public class OrderMap : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Order");

        builder.HasOne(o => o.Customer)
            .WithMany()
            .IsRequired();

        builder.HasOne(o => o.Payment)
           .WithOne(o => o.Order)
           .HasForeignKey<Payment>(o => o.OrderId)
           .IsRequired();

        builder.HasMany(o => o.Items)
            .WithOne(o => o.Order)
            .IsRequired();

        builder.HasMany(o => o.Historical)
            .WithOne(o => o.Order)
            .IsRequired();
    }
}
