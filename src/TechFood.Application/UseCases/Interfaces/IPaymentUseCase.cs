using System.Threading.Tasks;
using TechFood.Application.Models.Payment;

namespace TechFood.Application.UseCases.Interfaces;

public interface IPaymentUseCase
{
    Task<CreatePaymentResult?> CreateAsync(CreatePaymentRequest data);
}
