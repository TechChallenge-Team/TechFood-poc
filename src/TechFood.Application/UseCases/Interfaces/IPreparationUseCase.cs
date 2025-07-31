using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechFood.Application.Models.OrderMonitor;
using TechFood.Application.Models.Preparation;

namespace TechFood.Application.UseCases.Interfaces;

public interface IPreparationUseCase
{
    Task<IEnumerable<GetPreparationMonitorResult>> GetAllPreparationOrdersAsync();

    Task<int> GetPreparationByOrderIdAsync(Guid orderId);

    Task<IEnumerable<GetPreparationResult>> GetAllAsync();

    Task<GetPreparationResult> GetByIdAsync(Guid id);

    Task StartAsync(Guid id);

    Task FinishAsync(Guid id);

    Task CancelAsync(Guid id);
}
