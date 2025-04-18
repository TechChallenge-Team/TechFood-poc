using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechFood.Domain.Entities;
using TechFood.Domain.Repositories;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.Repositories
{
    public class CategoryRepository(TechFoodContext dbContext) : ICategoryRepository
    {
        private readonly DbSet<Category> _categories = dbContext.Categories;

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categories.AsNoTracking().ToListAsync();
        }
    }
}
