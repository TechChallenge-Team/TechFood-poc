using TechFoodClean.Common.Entities;

namespace TechFoodClean.Application.Interfaces.DataSource
{
    public interface IPaymentDataSource
    {
        Task<Guid> AddAsync(PaymentDTO payment);

        Task<PaymentDTO?> GetByIdAsync(Guid id);

        Task UpdateAsync(PaymentDTO payment);
    }
}
