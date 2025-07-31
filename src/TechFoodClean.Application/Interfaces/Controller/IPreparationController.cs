using TechFood.Application.Models.OrderMonitor;
using TechFoodClean.Application.Controllers;
using TechFoodClean.Application.Presenters;

namespace TechFoodClean.Application.Interfaces.Controller;

public interface IPreparationController
{
    Task<IEnumerable<PreparationMonitorPresenter>> GetAllPreparationOrdersAsync();
    Task<PreparationPresenter> GetPreparationByOrderIdAsync(Guid orderId);
    Task<IEnumerable<PreparationPresenter>> GetAllAsync();
    Task StartAsync(Guid id);
    Task FinishAsync(Guid id);
    Task CancelAsync(Guid id);
}
