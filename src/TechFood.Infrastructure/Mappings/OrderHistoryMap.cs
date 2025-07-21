using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFood.Common.Entities;

namespace TechFood.Infrastructure.Data.Mappings;

public class OrderHistoryMap : IEntityTypeConfiguration<OrderHistoryDTO>
{
    public void Configure(EntityTypeBuilder<OrderHistoryDTO> builder)
    {
        builder.ToTable("OrderHistory");
    }
}
