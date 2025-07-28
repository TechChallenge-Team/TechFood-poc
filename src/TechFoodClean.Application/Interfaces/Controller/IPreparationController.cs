using Microsoft.AspNetCore.Mvc;
using TechFoodClean.Application.Presenters;
using TechFoodClean.Domain.Entities;

namespace TechFoodClean.Application.Interfaces.Controller;

public interface IPreparationController
{
    Task<IEnumerable<object>> GetAllPreparationOrdersAsync();
    Task<object> GetPreparationByOrderIdAsync(Guid orderId);
    Task<IEnumerable<object>> GetAllAsync();
    Task StartAsync(Guid id);
    Task FinishAsync(Guid id);
    Task CancelAsync(Guid id);
}
