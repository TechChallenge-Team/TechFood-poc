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
            .IsRequired(false);

        builder.HasOne(o => o.Payment)
            .WithOne()
            .HasForeignKey<Order>("PaymentId")
            .IsRequired(false);

        builder.HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey("OrderId")
            .IsRequired();

        builder.HasMany(o => o.Historical)
            .WithOne()
            .HasForeignKey("OrderId")
            .IsRequired();
    }
}
