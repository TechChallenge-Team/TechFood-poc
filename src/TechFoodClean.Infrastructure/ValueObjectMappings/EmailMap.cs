using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFoodClean.Common.DTO.ValueObjects;

namespace TechFoodClean.Infrastructure.Data.ValueObjectMappings;

public static class EmailMap
{
    public static void MapEmail<TEntity>(this OwnedNavigationBuilder<TEntity, EmailDTO> navigationBuilder)
    where TEntity : class
    {
        navigationBuilder.WithOwner();

        navigationBuilder.Property(x => x.Address)
            .HasMaxLength(255)
            .HasColumnName("EmailAddress")
            .IsRequired();
    }
}
