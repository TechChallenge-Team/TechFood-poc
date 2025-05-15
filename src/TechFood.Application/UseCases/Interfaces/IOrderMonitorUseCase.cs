using System.Collections.Generic;
using System.Threading.Tasks;
using TechFood.Application.Models.OrderMonitor;

namespace TechFood.Application.UseCases.Interfaces;

public interface IOrderMonitorUseCase
{
    Task<IEnumerable<GetOrderMonitorResult>> GetAllAsync();
}
