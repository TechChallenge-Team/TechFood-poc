using TechFood.Application.Presenters;
using TechFood.Common.DTO;

namespace TechFood.Application.Interfaces.Controller;

public interface IOrderController
{
    Task<CreateOrderPresenter?> CreateOrderAsync(CreateOrderRequestDTO request);

    Task FinishAsync(Guid id);

    Task<IEnumerable<OrderPresenter>> GetAllDoneAndInPreparationAsync();
    Task<OrderPresenter> GetById(Guid id);
}
