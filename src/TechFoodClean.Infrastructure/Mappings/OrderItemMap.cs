using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFoodClean.Common.DTO;

namespace TechFoodClean.Infrastructure.Data.Mappings;

public class OrderItemMap : IEntityTypeConfiguration<OrderItemDTO>
{
    public void Configure(EntityTypeBuilder<OrderItemDTO> builder)
    {
        builder.ToTable("OrderItem");

        builder.HasOne<ProductDTO>()
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
