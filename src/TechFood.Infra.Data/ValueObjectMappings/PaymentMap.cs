using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFood.Domain.ValueObjects;

namespace TechFood.Infra.Data.ValueObjectMappings;

public static class PaymentMap
{
    public static void MapPayment<TEntity>(this OwnedNavigationBuilder<TEntity, Payment> navigationBuilder) where TEntity : class
    {
        navigationBuilder.WithOwner();

        navigationBuilder.Property(p => p.Type)
            .HasColumnName("PaymentType")
            .IsRequired();

        navigationBuilder.Property(p => p.Amount)
            .HasColumnName("PaymentAmount")
            .IsRequired();

        navigationBuilder.Property(p => p.PaidAt)
            .HasColumnName("PaymentPaidAt")
            .IsRequired();
    }
}
