using System.Collections.Generic;
using System.Threading.Tasks;
using TechFood.Application.Models.OrderMonitor;
using TechFood.Application.UseCases.Interfaces;
using TechFood.Domain.Shared.Interfaces;

namespace TechFood.Application.UseCases;

public class OrderMonitorUseCase(
    IReadOnlyQuery<GetOrderMonitorResult> orderMonitorRepository
    ) : IOrderMonitorUseCase
{
    private readonly IReadOnlyQuery<GetOrderMonitorResult> _orderMonitorRepository = orderMonitorRepository;

    public async Task<IEnumerable<GetOrderMonitorResult>> GetAllAsync()
    {
        var result = await _orderMonitorRepository.GetAllAsync();

        return result;
    }
}
