using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFoodClean.Common.DTO;

namespace TechFoodClean.Infrastructure.Data.Mappings;

public class OrderHistoryMap : IEntityTypeConfiguration<OrderHistoryDTO>
{
    public void Configure(EntityTypeBuilder<OrderHistoryDTO> builder)
    {
        builder.ToTable("OrderHistory");
    }
}
