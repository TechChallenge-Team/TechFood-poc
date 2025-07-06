using Microsoft.EntityFrameworkCore;
using TechFoodClean.Application.Interfaces.DataSource;
using TechFoodClean.Common.DTO.Enums;
using TechFoodClean.Common.Entities;
using TechFoodClean.Infrastructure.Data.Contexts;

namespace TechFoodClean.Infrastructure.Data.Repositories;

internal class OrderRepository(TechFoodContext dbContext) : IOrderDataSource
{
    private readonly DbSet<OrderDTO> _orders = dbContext.Orders;

    public async Task<Guid> AddAsync(OrderDTO order)
    {
        var entry = await _orders.AddAsync(order);

        return entry.Entity.Id;
    }

    public async Task<List<OrderDTO>> GetAllDoneAndInPreparationAsync()
    {
        var orders = await _orders.AsNoTracking()
                                  .Where(x => x.Status == OrderStatusTypeDTO.PreparationDone || x.Status == OrderStatusTypeDTO.InPreparation)
                                  .OrderBy(c => c.CreatedAt).ToListAsync();

        return orders;
    }

    public async Task<OrderDTO?> GetByIdAsync(Guid id)
    {
        var t = await _orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);

        return t;
    }
}
