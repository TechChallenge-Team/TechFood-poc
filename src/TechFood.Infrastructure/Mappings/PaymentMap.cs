using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFood.Common.Entities;

namespace TechFood.Infrastructure.Data.Mappings;

public class PaymentMap : IEntityTypeConfiguration<PaymentDTO>
{
    public void Configure(EntityTypeBuilder<PaymentDTO> builder)
    {
        builder.ToTable("Payment");
    }
}
