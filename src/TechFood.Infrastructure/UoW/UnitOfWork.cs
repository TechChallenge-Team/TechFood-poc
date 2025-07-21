using TechFood.Application.Interfaces.DataSource;
using TechFood.Infrastructure.Data.Contexts;

namespace TechFood.Infrastructure.Data.UoW;

public class UnitOfWork(TechFoodContext dbContext) : IUnitOfWorkDataSource
{
    public async Task<bool> CommitAsync()
    {
        var success = await dbContext.SaveChangesAsync() > 0;
        return success;
    }

    public Task RollbackAsync()
    {
        return Task.CompletedTask;
    }
}
