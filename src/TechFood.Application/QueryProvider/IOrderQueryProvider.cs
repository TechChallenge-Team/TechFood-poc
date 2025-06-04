using System.Collections.Generic;
using System.Threading.Tasks;
using TechFood.Application.UseCases.Order.Queries;

namespace TechFood.Application.QueryProvider;

public interface IOrderQueryProvider
{
    Task<IEnumerable<GetDailyOrdersQuery.Result>> GetDailyOrdersAsync(GetDailyOrdersQuery query);

    Task<IEnumerable<GetReadyOrdersQuery.Result>> GetReadyOrdersAsync(GetReadyOrdersQuery query);
}
