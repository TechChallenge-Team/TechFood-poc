using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFood.Domain.Entities;

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

        builder.HasOne(a => a.Category)
            .WithMany()
            .IsRequired();

        builder.Property(a => a.ImageFileName)
            .HasMaxLength(50)
            .IsRequired();
    }
}
