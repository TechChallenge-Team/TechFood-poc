using TechFoodClean.Common.DTO;

namespace TechFoodClean.Application.Interfaces.DataSource
{
    public interface IPaymentDataSource
    {
        Task<Guid> AddAsync(PaymentDTO payment);

        Task<PaymentDTO?> GetByIdAsync(Guid id);

        Task UpdateAsync(PaymentDTO payment);

        Task<PaymentDTO?> GetByOrderIdAsync(Guid id);
    }
}
