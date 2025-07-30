using TechFoodClean.Common.DTO.Payment;
using TechFoodClean.Domain.Entities;

namespace TechFoodClean.Domain.Interfaces.UseCase
{
    public interface IPaymentUseCase
    {
        Task<Payment?> GetByIdAsync(Guid id);

        Task<int> ConfirmAsync(Guid id);

        Task<Payment?> CreateAsync(CreatePaymentRequestDTO data);

        Task<Payment?> GetByOrderIdAsync(Guid id);
    }
}
