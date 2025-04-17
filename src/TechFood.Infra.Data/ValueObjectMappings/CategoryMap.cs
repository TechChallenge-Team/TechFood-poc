using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFood.Domain.Entities;

namespace TechFood.Infra.Data.ValueObjectMappings;

public static class CategoryMap
{
    public static void MapAddress<TEntity>(this OwnedNavigationBuilder<TEntity, Category> navigationBuilder) where TEntity : class
    {
        navigationBuilder.WithOwner();

        navigationBuilder.Property(a => a.Name)
            .HasMaxLength(50)
            .HasColumnName("AddressCity")
            .IsRequired();
    }
}
