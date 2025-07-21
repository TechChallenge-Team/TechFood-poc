using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFood.Common.Entities;

namespace TechFood.Infrastructure.Data.Mappings;

public class PreparationMap : IEntityTypeConfiguration<PreparationDTO>
{
    public void Configure(EntityTypeBuilder<PreparationDTO> builder)
    {
        builder.ToTable("Preparation");
    }
}

