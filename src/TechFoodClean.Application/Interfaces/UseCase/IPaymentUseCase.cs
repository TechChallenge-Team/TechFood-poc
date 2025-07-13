using TechFoodClean.Common.DTO.Payment;
using TechFoodClean.Domain.Entities;

namespace TechFoodClean.Application.Interfaces.UseCase
{
    public interface IPaymentUseCase
    {
        Task<int> ConfirmAsync(Guid id);

        Task<Payment?> CreateAsync(CreatePaymentRequestDTO data);
    }
}
