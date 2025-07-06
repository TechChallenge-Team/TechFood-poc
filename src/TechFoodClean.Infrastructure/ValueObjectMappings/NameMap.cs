using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFoodClean.Common.DTO.ValueObjects;

namespace TechFoodClean.Infrastructure.DataValueObjectMappings;

public static class NameMap
{
    public static void MapName<TEntity>(this OwnedNavigationBuilder<TEntity, NameDTO> navigationBuilder)
        where TEntity : class
    {
        navigationBuilder.WithOwner();

        navigationBuilder.Property(x => x.FullName)
            .HasMaxLength(255)
            .HasColumnName("NameFullName")
            .IsRequired();
    }
}
