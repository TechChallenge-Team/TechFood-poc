using TechFood.Common.Entities;

namespace TechFood.Application.Interfaces.DataSource
{
    public interface IPaymentDataSource
    {
        Task<Guid> AddAsync(PaymentDTO payment);

        Task<PaymentDTO?> GetByIdAsync(Guid id);

        Task UpdateAsync(PaymentDTO payment);
    }
}
