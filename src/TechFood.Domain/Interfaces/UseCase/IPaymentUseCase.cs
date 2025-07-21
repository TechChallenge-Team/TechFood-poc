using TechFood.Common.DTO.Payment;
using TechFood.Domain.Entities;

namespace TechFood.Domain.Interfaces.UseCase
{
    public interface IPaymentUseCase
    {
        Task<int> ConfirmAsync(Guid id);

        Task<Payment?> CreateAsync(CreatePaymentRequestDTO data);
    }
}
