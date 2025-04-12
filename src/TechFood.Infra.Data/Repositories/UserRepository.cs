using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechFood.Domain.Entities;
using TechFood.Domain.Repositories;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.Repositories
{
    public class UserRepository(TechFoodContext dbContext) : IUserRepository
    {
        private readonly DbSet<User> _user = dbContext.Users;

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _user.FindAsync(id);
            return user;
        }
    }
}
