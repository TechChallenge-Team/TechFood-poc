using TechFoodClean.Domain.Entities;
using TechFoodClean.Domain.Interfaces.Gateway;
using TechFoodClean.Domain.Interfaces.UseCase;

namespace TechFoodClean.Domain.UseCases;

public class PreparationUseCase : IPreparationUseCase
{
    private readonly IPreparationGateway _preparationGateway;
        
    public PreparationUseCase(
        IPreparationGateway preparationGateway)
    {
        _preparationGateway = preparationGateway;
    }
    
    public Task<IEnumerable<Preparation>> GetAllPreparationOrdersAsync()
    {
        return _preparationGateway.GetAllAsync();
    }

    public Task<int> GetPreparationByOrderIdAsync(Guid orderId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Preparation>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Preparation> GetByIdAsync(Guid id)
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
