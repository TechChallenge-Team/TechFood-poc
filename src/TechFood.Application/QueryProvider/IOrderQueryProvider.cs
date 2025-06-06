using System.Collections.Generic;
using System.Threading.Tasks;
using TechFood.Application.UseCases.Order.Queries;

namespace TechFood.Application.QueryProvider;

public interface IOrderQueryProvider
{
    Task<IEnumerable<GetOrdersQuery.Result>> GetOrdersAsync(GetOrdersQuery query);

    Task<IEnumerable<GetReadyOrdersQuery.Result>> GetReadyOrdersAsync(GetReadyOrdersQuery query);
}
