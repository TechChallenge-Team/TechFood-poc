using System.Collections.Generic;
using System.Threading.Tasks;
using TechFood.Application.Models;

namespace TechFood.Application.UseCases.Interfaces
{
    public interface ICategoryUseCase
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();

    }
}
