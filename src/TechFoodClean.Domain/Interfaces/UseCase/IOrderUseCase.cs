using TechFoodClean.Domain.Entities;

namespace TechFoodClean.Domain.Interfaces.UseCase;

public interface IOrderUseCase
{
    Task<Order> CreateOrderAsync(CreateOrderRequestDTO request);
    Task FinishAsync(FinishOrderRequestDTO request);
}
