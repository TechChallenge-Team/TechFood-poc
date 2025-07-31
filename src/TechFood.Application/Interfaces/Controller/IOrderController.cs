using TechFood.Application.Presenters;
using TechFood.Common.DTO;

namespace TechFood.Application.Interfaces.Controller;

public interface IOrderController
{
    Task<OrderPresenter?> CreateOrderAsync(CreateOrderRequestDTO request);
    Task FinishAsync(FinishOrderRequestDTO request);

}
