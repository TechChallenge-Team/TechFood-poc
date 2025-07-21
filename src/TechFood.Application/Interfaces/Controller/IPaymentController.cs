using TechFood.Application.Presenters;
using TechFood.Common.DTO.Payment;

namespace TechFood.Application.Interfaces.Controller
{
    public interface IPaymentController
    {
        Task<int> ConfirmAsync(Guid id);

        Task<PaymentPresenter?> CreateAsync(CreatePaymentRequestDTO data);
    }
}
