using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechFood.Application.Models.OrderMonitor;
using TechFood.Domain.Shared.Interfaces;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.Repositories;

internal class OrderMonitorRepository(TechFoodContext dbContext) : IReadOnlyQuery<GetOrderMonitorResult>
{
    private readonly TechFoodContext _dbContext = dbContext;

    public async Task<IEnumerable<GetOrderMonitorResult>> GetAllAsync()
    {
        var orders = await _dbContext.Orders
          .Where(o => o.Status == Domain.Enums.OrderStatusType.Received
              || o.Status == Domain.Enums.OrderStatusType.InPreparation
              || o.Status == Domain.Enums.OrderStatusType.Done)
          .Select(order => new GetOrderMonitorResult
          {
              OrderId = order.Id,
              Status = order.Status,
              Products = order.Items
                  .Select(oi => new ProductResult
                  {
                      Id = oi.ProductId,
                      Name = _dbContext.Products
                          .Where(p => p.Id == oi.ProductId)
                          .Select(p => p.Name)
                          .FirstOrDefault(),
                      Quantity = oi.Quantity

                  })
                  .ToList()
          })
          .ToListAsync();

        return orders;
    }
}
