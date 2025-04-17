using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TechFood.Domain.Entities;
using TechFood.Infra.Data.ValueObjectMappings;

namespace TechFood.Infra.Data.Mappings;

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");

        builder.Property(a => a.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(a => a.Description)
            .HasMaxLength(255)
            .IsRequired();

        //builder.Property(a => a.CategoryId)
        //   .HasMaxLength(255)
        //   .IsRequired();

        builder.OwnsOne(a => a.Category, category => category.Name)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(a => a.ImageId)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(a => a.Price)
            .HasPrecision(6,2)
            .IsRequired();
    }
}
