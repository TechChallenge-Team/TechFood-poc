using Microsoft.EntityFrameworkCore;
using TechFood.Application.Interfaces.DataSource;
using TechFood.Common.DTO.Enums;
using TechFood.Common.Entities;
using TechFood.Infrastructure.Data.Contexts;

namespace TechFood.Infrastructure.Data.Repositories;

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
            .AsNoTracking()
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);

        return t;
    }

    public async Task UpdateAsync(OrderDTO order)
    {
        foreach (var hist in order.Historical)
        {
            if (hist.Id == Guid.Empty)
            {
                hist.Id = Guid.NewGuid();
                hist.OrderId = order.Id;
                dbContext.Entry(hist).State = EntityState.Added;
            }
            else
            {
                dbContext.Entry(hist).State = EntityState.Modified;
            }
        }

        await Task.FromResult(_orders.Update(order));
    }

}
