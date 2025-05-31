using System.Collections.Generic;
using System.Threading.Tasks;
using TechFood.Application.UseCases.Product.Queries;

namespace TechFood.Application.QueryProvider;

public interface IProductQueryProvider
{
    Task<IEnumerable<GetAllProductQuery.Result>> GetAllAsync(GetAllProductQuery request);

    Task<GetProductByIdQuery.Result?> GetByIdAsync(GetProductByIdQuery request);
}
