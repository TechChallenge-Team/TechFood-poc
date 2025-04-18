using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFood.Domain.Entities;
using TechFood.Infra.Data.ValueObjectMappings;

namespace TechFood.Infra.Data.Mappings;

public class OrderMap : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Order");

        builder.HasOne(o => o.Customer)
            .WithMany()
            .IsRequired();

        builder.HasMany(o => o.Items)
               .WithOne()
               .IsRequired();

        builder.HasMany(o => o.Historical)
              .WithOne()
              .IsRequired();

        builder.OwnsOne(o => o.Payment, payment => payment!.MapPayment())
            .Navigation(o => o.Payment);
    }
}
