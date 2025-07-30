using TechFoodClean.Common.DTO;
using TechFoodClean.Domain.Entities;

namespace TechFoodClean.Domain.Interfaces.UseCase;

public interface IOrderUseCase
{
    Task<Order> CreateOrderAsync(CreateOrderRequestDTO request);

    Task<Order> GetOrderByIdAsync(Guid orderId);

    Task FinishAsync(FinishOrderRequestDTO request);
}
