using System;
using System.Threading.Tasks;
using TechFood.Application.Models.Payment;

namespace TechFood.Application.UseCases.Interfaces;

public interface IPaymentUseCase
{
    Task ConfirmAsync(Guid id);

    Task<CreatePaymentResult?> CreateAsync(CreatePaymentRequest data);
}
