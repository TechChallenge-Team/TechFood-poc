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
    public async Task<IEnumerable<Preparation>> GetAllAsync()
    {
        var result = await _preparations
            .Where(query => query.Status == PreparationStatusType.Pending ||
                             query.Status == PreparationStatusType.InProgress||
                             query.Status == PreparationStatusType.Done)
            .ToListAsync();

        return result;
    }

    public async Task<Preparation?> GetByOrderIdAsync(Guid orderId)
    {
        return await _preparations.Where(x => x.OrderId == orderId).FirstOrDefaultAsync();
    }
}
