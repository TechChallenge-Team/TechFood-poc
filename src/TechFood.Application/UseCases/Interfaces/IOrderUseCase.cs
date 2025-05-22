using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechFood.Application.Models.Order;

namespace TechFood.Application.UseCases.Interfaces;

public interface IOrderUseCase
{
    Task<List<GetAllOrderResponse>> GetAllDoneAndInPreparationAsync();

    Task<CreateOrderResult> CreateAsync(CreateOrderRequest request);

    Task<bool> PrepareAsync(Guid orderId);

    Task<bool> FinishAsync(Guid orderId);
}
