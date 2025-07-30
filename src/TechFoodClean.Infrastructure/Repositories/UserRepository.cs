using Microsoft.EntityFrameworkCore;
using TechFoodClean.Application.Interfaces.DataSource;
using TechFoodClean.Common.DTO;
using TechFoodClean.Infrastructure.Data.Contexts;

namespace TechFoodClean.Infrastructure.Data.Repositories
{
    internal class UserRepository(TechFoodContext dbContext) : IUserDataSource
    {
        private readonly TechFoodContext _dbContext = dbContext;

        public async Task<Guid> CreateAsync(UserDTO user)
        {
            var entry = await _dbContext.AddAsync(user);

            return entry.Entity.Id;
        }

        public async Task<UserDTO?> GetByUsernameOrEmailAsync(string username)
        {
            return await _dbContext
                .Users
                .FirstOrDefaultAsync(
                    u => u.Username == username || (u.Email != null && u.Email.Address! == username));
        }
    }
}
