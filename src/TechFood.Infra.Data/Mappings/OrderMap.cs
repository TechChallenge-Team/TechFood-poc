using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFood.Domain.Entities;

namespace TechFood.Infra.Data.Mappings;

public class OrderMap : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Order");

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .IsRequired();

        builder.HasOne(o => o.Payment)
           .WithOne()
           .HasForeignKey<Payment>(o => o.OrderId)
           .IsRequired();

        builder.HasMany(o => o.Items)
            .WithOne()
            .IsRequired();

        builder.HasMany(o => o.Historical)
            .WithOne()
            .IsRequired();
    }
}
