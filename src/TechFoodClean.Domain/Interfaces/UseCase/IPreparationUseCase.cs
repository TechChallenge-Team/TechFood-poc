using TechFoodClean.Domain.Entities;

namespace TechFoodClean.Domain.Interfaces.UseCase;

public interface IPreparationUseCase
{
    Task<IEnumerable<Preparation>> GetAllPreparationOrdersAsync();

    Task<int> GetPreparationByOrderIdAsync(Guid orderId);

    Task<IEnumerable<Preparation>> GetAllAsync();

    Task<Preparation> GetByIdAsync(Guid id);

    Task StartAsync(Guid id);

    Task FinishAsync(Guid id);

    Task CancelAsync(Guid id);
}
