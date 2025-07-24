using TechFoodClean.Application.Presenters;
using TechFoodClean.Common.DTO.Payment;

namespace TechFoodClean.Application.Interfaces.Controller
{
    public interface IPaymentController
    {
        Task<PaymentPresenter?> GetByIdAsync(Guid id);

        Task<int> ConfirmAsync(Guid id);

        Task<PaymentPresenter?> CreateAsync(CreatePaymentRequestDTO data);
    }
}
