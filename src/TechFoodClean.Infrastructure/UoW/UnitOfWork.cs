using TechFoodClean.Application.Interfaces.DataSource;
using TechFoodClean.Infrastructure.Data.Contexts;

namespace TechFoodClean.Infrastructure.Data.UoW;

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
