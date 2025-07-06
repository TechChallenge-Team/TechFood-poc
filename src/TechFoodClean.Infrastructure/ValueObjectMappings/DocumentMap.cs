using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFoodClean.Common.DTO.ValueObjects;

namespace TechFoodClean.Infrastructure.Data.ValueObjectMappings;

public static class DocumentMap
{
    public static void MapDocument<TEntity>(this OwnedNavigationBuilder<TEntity, DocumentDTO> navigationBuilder)
       where TEntity : class
    {
        navigationBuilder.WithOwner();

        navigationBuilder.Property(x => x.Value)
            .HasMaxLength(20)
            .HasColumnName("DocumentValue")
            .IsRequired();

        navigationBuilder.Property(x => x.Type)
            .HasColumnName("DocumentType")
            .IsRequired();
    }
}
