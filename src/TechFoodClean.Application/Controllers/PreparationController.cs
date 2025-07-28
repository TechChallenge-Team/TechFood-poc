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
            preparation.OrderId
        ));
    }

    public async Task<object> GetPreparationByOrderIdAsync(Guid orderId)
    {
        var result = await _preparationUseCase.GetPreparationByOrderIdAsync(orderId);
        return result is not null ? 
            new PreparationPresenter(
                result.Id,
                result.Status,
                result.CreatedAt,
                result.StartedAt,
                result.FinishedAt,
                result.OrderId
            ) : null;
    }

    public async Task<IEnumerable<object>> GetAllAsync()
    {
        var preparations = await _preparationUseCase.GetAllAsync();
    
        return preparations.Select(preparation => new PreparationPresenter(
            preparation.Id,
            preparation.Status,
            preparation.CreatedAt,
            preparation.StartedAt,
            preparation.FinishedAt,
            preparation.OrderId
        ));
    }

    public async Task StartAsync(Guid id)
    {
      await _preparationUseCase.StartAsync(id);
    }

    public async Task FinishAsync(Guid id)
    {
        await _preparationUseCase.FinishAsync(id);
    }

    public async Task CancelAsync(Guid id)
    {
        await _preparationUseCase.CancelAsync(id);
    }
}
