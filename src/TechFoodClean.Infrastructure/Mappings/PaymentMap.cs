using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFoodClean.Common.Entities;

namespace TechFoodClean.Infrastructure.Data.Mappings;

public class PaymentMap : IEntityTypeConfiguration<PaymentDTO>
{
    public void Configure(EntityTypeBuilder<PaymentDTO> builder)
    {
        builder.ToTable("Payment");
    }
}
