using TechFoodClean.Domain.Entities;

namespace TechFoodClean.Application.Interfaces.Gateway
{
    public interface IPaymentGateway
    {
        Task<Guid> AddAsync(Payment payment);

        Task<Payment?> GetByIdAsync(Guid id);
        Task UpdateAsync(Payment product);
    }
}
