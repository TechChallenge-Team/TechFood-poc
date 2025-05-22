using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechFood.Application.Common.Services.Interfaces;
using TechFood.Application.Models.OrderMonitor;
using TechFood.Application.UseCases.Interfaces;
using TechFood.Domain.Entities;
using TechFood.Domain.Shared.Interfaces;

namespace TechFood.Application.UseCases;

public class OrderMonitorUseCase(
    IReadOnlyQuery<GetOrderMonitorResult> orderMonitorRepository,
    IImageUrlResolver imageUrlResolver
    ) : IOrderMonitorUseCase
{
    private readonly IReadOnlyQuery<GetOrderMonitorResult> _orderMonitorRepository = orderMonitorRepository;
    private readonly IImageUrlResolver _imageUrlResolver = imageUrlResolver;

    public async Task<IEnumerable<GetOrderMonitorResult>> GetAllAsync()
    {
        var result = await _orderMonitorRepository.GetAllAsync();

        
        return result;
    }
}
