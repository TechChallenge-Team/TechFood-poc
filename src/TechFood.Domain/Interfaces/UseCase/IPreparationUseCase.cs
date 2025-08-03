using TechFood.Domain.Entities;

namespace TechFood.Domain.Interfaces.UseCase;

public interface IPreparationUseCase
{
    Task<Preparation> GetPreparationByOrderIdAsync(Guid orderId);

    Task<IEnumerable<Preparation>> GetAllAsync();

    Task<Preparation> GetByIdAsync(Guid id);

    Task<Preparation> StartAsync(Guid id);

    Task<Preparation> FinishAsync(Guid id);

    Task<Preparation> CancelAsync(Guid id);
}
