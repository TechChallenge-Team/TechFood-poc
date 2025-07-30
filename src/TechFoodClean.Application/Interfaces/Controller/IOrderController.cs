using TechFoodClean.Application.Presenters;
using TechFoodClean.Common.DTO;

namespace TechFoodClean.Application.Interfaces.Controller;

public interface IOrderController
{
    Task<OrderPresenter?> CreateOrderAsync(CreateOrderRequestDTO request);
    Task FinishAsync(FinishOrderRequestDTO request);

}
