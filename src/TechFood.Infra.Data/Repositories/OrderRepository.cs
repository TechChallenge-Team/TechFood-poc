using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechFood.Domain.Entities;
using TechFood.Domain.Enums;
using TechFood.Domain.Repositories;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.Repositories;

internal class OrderRepository(TechFoodContext dbContext) : IOrderRepository
{
    private readonly DbSet<Order> _orders = dbContext.Orders;

    public async Task<Guid> AddAsync(Order order)
    {
        var entry = await _orders.AddAsync(order);

        return entry.Entity.Id;
    }

    public async Task<List<Order>> GetAllDoneAndInPreparationAsync()
    {
        var orders = await _orders.AsNoTracking()
                                  .Where(x => x.Status == OrderStatusType.PreparationDone || x.Status == OrderStatusType.InPreparation)
                                  .OrderBy(c => c.CreatedAt).ToListAsync();

        return orders;
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        var t = await _orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id);

        return t;
    }
}
