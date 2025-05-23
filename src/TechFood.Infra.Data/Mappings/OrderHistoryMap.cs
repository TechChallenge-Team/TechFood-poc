using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFood.Domain.Entities;

namespace TechFood.Infra.Data.Mappings;

public class OrderHistoryMap : IEntityTypeConfiguration<OrderHistory>
{
    public void Configure(EntityTypeBuilder<OrderHistory> builder)
    {
        builder.ToTable("OrderHistory");
    }
}
