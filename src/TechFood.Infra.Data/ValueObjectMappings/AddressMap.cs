using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFood.Domain.Entities;

namespace TechFood.Infra.Data.ValueObjectMappings;

public static class AddressMap
{
    public static void MapAddress<TEntity>(this OwnedNavigationBuilder<TEntity, Category> navigationBuilder) where TEntity : class
    {
        navigationBuilder.WithOwner();

        navigationBuilder.Property(a => a.Name)
            .HasMaxLength(50)
            .HasColumnName("AddressCity")
            .IsRequired();

        navigationBuilder.Property(a => a.State)
            .HasMaxLength(50)
            .HasColumnName("AddressState")
            .IsRequired();

        navigationBuilder.Property(a => a.StreetName)
            .HasMaxLength(50)
            .HasColumnName("AddressStreetName")
            .IsRequired();

        navigationBuilder.Property(a => a.StreetNumber)
            .HasMaxLength(10)
            .HasColumnName("AddressStreetNumber")
            .IsRequired();

        navigationBuilder.Property(a => a.ZipCode)
            .HasMaxLength(8)
            .HasColumnName("AddressZipCode")
            .IsRequired();
    }
}
