using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFoodClean.Common.DTO;

namespace TechFoodClean.Infrastructure.Data.Mappings;

public class PreparationMap : IEntityTypeConfiguration<PreparationDTO>
{
    public void Configure(EntityTypeBuilder<PreparationDTO> builder)
    {
        builder.ToTable("Preparation");
    }
}

