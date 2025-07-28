using Microsoft.EntityFrameworkCore;
using TechFoodClean.Application.Interfaces.DataSource;
using TechFoodClean.Common.DTO.Enums;
using TechFoodClean.Common.Entities;
using TechFoodClean.Infrastructure.Data.Contexts;

namespace TechFoodClean.Infrastructure.Data.Repositories;

public class PreparationRepository(TechFoodContext dbContext) : IPreparationDataSource
{
    private readonly DbSet<PreparationDTO> _preparations = dbContext.Preparations;

    public async Task<Guid> AddAsync(PreparationDTO preparation)
    {
        var entry = await _preparations.AddAsync(preparation);

        return entry.Entity.Id;
    }

    public Task<PreparationDTO?> GetByIdAsync(Guid id)
    {
        var preparation = _preparations
            .FirstOrDefaultAsync(p => p.Id == id);

        return preparation;
    }

    //NOTES: Check the queryobject pattern
    public async Task<IEnumerable<PreparationDTO>> GetAllAsync()
    {
        var result = await _preparations
            .Where(query => query.Status == PreparationStatusTypeDTO.Pending ||
                             query.Status == PreparationStatusTypeDTO.InProgress ||
                             query.Status == PreparationStatusTypeDTO.Done)
            .ToListAsync();

        return result;
    }

    public async Task<PreparationDTO?> GetByOrderIdAsync(Guid orderId)
    {
        return await _preparations.FirstOrDefaultAsync(x => x.OrderId == orderId);
    }

    public async Task UpdateAsync(PreparationDTO updatedData)
    {
        var local = _preparations.Local.FirstOrDefault(x => x.Id == updatedData.Id);
        if (local != null)
        {
            _preparations.Entry(local).State = EntityState.Detached;
        }

        _preparations.Update(updatedData);
    }
}