using Microsoft.AspNetCore.Mvc;
using TechFood.Application.Models.OrderMonitor;
using TechFood.Application.UseCases.Interfaces;

namespace TechFood.Api.Controllers;

[ApiController()]
[Route("v1/[controller]")]
public class OrderMonitorController(IOrderMonitorUseCase orderMonitorUseCase) : ControllerBase
{
    private readonly IOrderMonitorUseCase
        _orderMonitorRepository = orderMonitorUseCase;

    [HttpGet]
    public Task<IEnumerable<GetOrderMonitorResult>> GetAllAsync()
    {
        return _orderMonitorRepository.GetAllAsync();
    }
}
