using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechFood.Application.Common.Services.Interfaces;
using TechFood.Application.QueryProvider;
using TechFood.Application.UseCases.Preparation.Queries;
using TechFood.Domain.Entities;
using TechFood.Domain.Enums;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.QueryProviders;

internal class PreparationQueryProvider(
    IImageUrlResolver imageUrlResolver,
    TechFoodContext techFoodContext) : IPreparationQueryProvider
{
    public async Task<GetPreparationByIdQuery.Result?> GetByIdAsync(GetPreparationByIdQuery query)
    {
        return await techFoodContext.Preparations
            .AsNoTracking()
            .Where(order => order.Id == query.Id)
            .Select(preparation => new GetPreparationByIdQuery.Result
            {
                Id = preparation.Id,
                OrderId = preparation.OrderId,
                CreatedAt = preparation.CreatedAt,
                StartedAt = preparation.StartedAt,
                ReadyAt = preparation.ReadyAt,
                Status = preparation.Status
            })
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<GetDailyPreparationsQuery.Result>> GetDailyPreparationsAsync(GetDailyPreparationsQuery query)
    {
        var status = new PreparationStatusType[]
        {
            PreparationStatusType.Pending,
            PreparationStatusType.Started
        };

        var products = await techFoodContext.Products.AsNoTracking().ToListAsync();

        var result = await techFoodContext.Orders
            .AsNoTracking()
            .Include(order => order.Items)
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
            .Where(query => status.Contains(query.Preparation.Status))
            .OrderBy(query => query.Preparation.CreatedAt)
            .ToListAsync();

        return result.Select(data => new GetDailyPreparationsQuery.Result
        {
            Id = data.Preparation.Id,
            Status = data.Preparation.Status,
            CreatedAt = data.Preparation.CreatedAt,
            StartedAt = data.Preparation.StartedAt,
            ReadyAt = data.Preparation.ReadyAt,
            OrderId = data.Order.Id,
            Number = data.Order.Number,
            Amount = data.Order.Amount,
            Items = data.Order.Items.Select(item =>
            {
                var product = products.FirstOrDefault(p => p.Id == item.ProductId);
                return new GetDailyPreparationsQuery.Result.PreparationItem
                {
                    Id = item.Id,
                    Name = product!.Name,
                    ImageUrl = imageUrlResolver.BuildFilePath(nameof(Product).ToLower(), product.ImageFileName),
                    Quantity = item.Quantity
                };
            }).ToList()
        });
    }

    public async Task<IEnumerable<GetTrackingPreparationsQuery.Result>> GetTrackingItemsAsync(GetTrackingPreparationsQuery query)
    {
        var orderStatus = new OrderStatusType[]
        {
            OrderStatusType.Received,
            OrderStatusType.InPreparation,
            OrderStatusType.Ready
        };
        var preparationStatus = new PreparationStatusType[]
        {
            PreparationStatusType.Pending,
            PreparationStatusType.Started,
            PreparationStatusType.Ready
        };

        var result = await techFoodContext.Orders
            .AsNoTracking()
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
            .Where(query =>
                orderStatus.Contains(query.Order.Status) &&
                preparationStatus.Contains(query.Preparation.Status))
            .ToListAsync();

        return result.Select(data => new GetTrackingPreparationsQuery.Result
        {
            Id = data.Preparation.Id,
            Status = data.Preparation.Status,
            OrderId = data.Order.Id,
            Number = data.Order.Number
        });
    }
}
