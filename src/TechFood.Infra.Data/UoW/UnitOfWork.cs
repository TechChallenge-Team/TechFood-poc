using System.Threading.Tasks;
using TechFood.Domain.UoW;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.UoW
{
    public class UnitOfWork(TechFoodContext dbContext) : IUnitOfWork
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
}
