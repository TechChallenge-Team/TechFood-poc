using System.Collections.Generic;
using System.Threading.Tasks;
using TechFood.Application.UseCases.Preparation.Queries;

namespace TechFood.Application.QueryProvider;

public interface IPreparationQueryProvider
{
    Task<GetPreparationByIdQuery.Result?> GetByIdAsync(GetPreparationByIdQuery query);

    Task<IEnumerable<GetDailyPreparationsQuery.Result>> GetDailyPreparationsAsync(GetDailyPreparationsQuery query);

    Task<IEnumerable<GetTrackingPreparationsQuery.Result>> GetTrackingItemsAsync(GetTrackingPreparationsQuery query);
}
