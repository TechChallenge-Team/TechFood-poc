using TechFoodClean.Application.Interfaces.DataSource;
using TechFoodClean.Infrastructure.Data.Contexts;

namespace TechFoodClean.Infrastructure.Data;

public class AnotherUnitOfWork(TechFoodContext dbContext) : IUnitOfWorkDataSource
{
    private readonly TechFoodContext _context = dbContext;

    public async Task<bool> CommitAsync()
    {
        var success = (await _context.SaveChangesAsync()) > 0;
        return success;
    }

    public Task RollbackAsync()
    {
        return Task.CompletedTask;
    }
}
