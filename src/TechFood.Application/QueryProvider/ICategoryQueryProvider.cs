using System.Collections.Generic;
using System.Threading.Tasks;
using TechFood.Application.UseCases.Category.Queries;

namespace TechFood.Application.QueryProvider;

public interface ICategoryQueryProvider
{
    Task<IEnumerable<GetAllCategoryQuery.Result>> GetAllAsync(GetAllCategoryQuery query);

    Task<GetCategoryByIdQuery.Result?> GetByIdAsync(GetCategoryByIdQuery query);
}
