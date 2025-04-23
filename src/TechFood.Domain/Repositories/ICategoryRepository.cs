using TechFood.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace TechFood.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(Guid id);
    }
}
