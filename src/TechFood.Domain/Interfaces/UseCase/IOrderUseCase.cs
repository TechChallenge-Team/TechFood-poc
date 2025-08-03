using TechFood.Common.DTO;
using TechFood.Domain.Entities;

namespace TechFood.Domain.Interfaces.UseCase;

public interface IOrderUseCase
{
    Task<Order> CreateOrderAsync(CreateOrderRequestDTO request);

    Task<Order> GetOrderByIdAsync(Guid orderId);

    Task FinishAsync(Guid orderId);

    Task<IEnumerable<Order>> GetAllDoneAndInPreparationAsync();

    Task<Order> GetByIdAsync(Guid id);
}

