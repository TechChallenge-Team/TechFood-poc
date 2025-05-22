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

public class PreparationRepository(TechFoodContext dbContext) : IPreparationRepository
{
    private readonly DbSet<Order> _orders = dbContext.Orders;
    private readonly DbSet<Preparation> _preparations = dbContext.Preparations;

    public async Task<Guid> AddAsync(Preparation preparation)
    {
        var entry = await _preparations.AddAsync(preparation);

        return entry.Entity.Id;
    }

    public Task<Preparation?> GetByIdAsync(Guid id)
    {
        var preparation = _preparations
            .FirstOrDefaultAsync(p => p.Id == id);

        return preparation;
    }

    //NOTES: Check the queryobject pattern
    public async Task<List<(Preparation Preparation, Order Order)>> GetAllAsync()
    {
        var result = await _orders
            .Join(
                _preparations,
                order => order.Id,
                preparation => preparation.OrderId,
                (order, preparation) => new
                {
                    Order = order,
                    Preparation = preparation
                }
            )
            .Where(query => query.Order.Status == OrderStatusType.Paid ||
                             query.Order.Status == OrderStatusType.InPreparation ||
                             query.Order.Status == OrderStatusType.PreparationDone)
            .ToListAsync();

        return result.Select(q => (q.Preparation, q.Order)).ToList();
    }
}
