using TechFoodClean.Application.Gateway;
using TechFoodClean.Application.Interfaces.Controller;
using TechFoodClean.Application.Interfaces.DataSource;
using TechFoodClean.Application.Presenters;
using TechFoodClean.Common.DTO;
using TechFoodClean.Domain.Interfaces.UseCase;
using TechFoodClean.Domain.UseCases;

namespace TechFoodClean.Application.Controllers;

public class OrderController : IOrderController
{
    private readonly IOrderUseCase _orderUseCase;

    public OrderController(
        IOrderDataSource orderDataSource,
        IProductDataSource productDataSource,
        IPreparationDataSource preparationDataSource,
        IUnitOfWorkDataSource unitOfWork
        )
    {
        var orderGateway = new OrderGateway(orderDataSource, unitOfWork);
        var productGateway = new ProductGateway(productDataSource, unitOfWork);
        var preparationGateway = new PreparationGateway(preparationDataSource, unitOfWork);
        _orderUseCase = new OrderUseCase(orderGateway, productGateway, preparationGateway);
    }

    public async Task<OrderPresenter?> CreateOrderAsync(CreateOrderRequestDTO request)
    {

        var order = await _orderUseCase.CreateOrderAsync(request);

        return order is not null ?
                   OrderPresenter.Create(order) :
                   null;
    }

    public async Task FinishAsync(FinishOrderRequestDTO request)
    {
        await _orderUseCase.FinishAsync(request);

        return;
    }
}
