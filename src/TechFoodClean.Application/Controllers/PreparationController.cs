using TechFoodClean.Application.Gateway;
using TechFoodClean.Application.Interfaces.Controller;
using TechFoodClean.Application.Interfaces.DataSource;
using TechFoodClean.Application.Presenters;
using TechFoodClean.Domain.Interfaces.UseCase;
using TechFoodClean.Domain.UseCases;

namespace TechFoodClean.Application.Controllers;

public class PreparationController : IPreparationController

{
    private readonly IPreparationUseCase _preparationUseCase;
    
    public PreparationController(
        IPreparationDataSource preparationDataSource,
        IUnitOfWorkDataSource unitOfWork
        )
    {
        var preparationGateway = new PreparationGateway(preparationDataSource, unitOfWork);
        _preparationUseCase = new PreparationUseCase(preparationGateway);
        
    }
    public async Task<IEnumerable<object>> GetAllPreparationOrdersAsync()
    {
        var preparations = await _preparationUseCase.GetAllPreparationOrdersAsync();
    
        return preparations.Select(preparation => new PreparationPresenter(
            preparation.Id,
            preparation.Status,
            preparation.CreatedAt,
            preparation.StartedAt,
            preparation.FinishedAt,
            // preparation.CanceledAt,
            preparation.OrderId
        ));
    }

    public Task<object> GetPreparationByOrderIdAsync(Guid orderId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<object>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task StartAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task FinishAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task CancelAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
