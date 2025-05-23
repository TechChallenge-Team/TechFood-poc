using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechFood.Application.QueryProvider;
using TechFood.Application.UseCases.Preparation.Queries;
using TechFood.Domain.Enums;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.QueryProviders;

internal class PreparationQueryProvider(TechFoodContext techFoodContext) : IPreparationQueryProvider
{
    public async Task<IEnumerable<GetMonitorPreparationsQuery.Result>> GetMonitorPreparationsAsync(GetMonitorPreparationsQuery query)
    {
        var result = await techFoodContext.Orders
            .Join(
                techFoodContext.Preparations,
                order => order.Id,
                preparation => preparation.OrderId,
                (order, preparation) => new
                {
                    Order = order,
                    Preparation = preparation
                }
            )
            .Where(query => query.Order.Status == OrderStatusType.Paid ||
                             query.Order.Status == OrderStatusType.InPreparation ||
                             query.Order.Status == OrderStatusType.PreparationDone)
            .ToListAsync();

        return result.Select(data => new GetMonitorPreparationsQuery.Result
        {
            Id = data.Preparation.Id,
            CreatedAt = data.Preparation.CreatedAt,
            StartedAt = data.Preparation.StartedAt,
            FinishedAt = data.Preparation.FinishedAt,
            OrderId = data.Order.Id,
            Number = data.Order.Number,
            Status = data.Preparation.Status
        });
    }

    public async Task<GetPreparationByIdQuery.Result?> GetByIdAsync(GetPreparationByIdQuery query)
    {
        return await techFoodContext.Preparations
           .Where(x => x.Id == query.Id)
           .Select(preparation => new GetPreparationByIdQuery.Result
           {
               Id = preparation.Id,
               OrderId = preparation.OrderId,
               CreatedAt = preparation.CreatedAt,
               StartedAt = preparation.StartedAt,
               FinishedAt = preparation.FinishedAt,
               Status = preparation.Status
           })
           .FirstOrDefaultAsync();
    }
}
