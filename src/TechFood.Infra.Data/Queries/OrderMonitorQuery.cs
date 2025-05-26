using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechFood.Application.Common.Services.Interfaces;
using TechFood.Application.Models.OrderMonitor;
using TechFood.Domain.Entities;
using TechFood.Domain.Enums;
using TechFood.Domain.Shared.Interfaces;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.Queries;

internal class OrderMonitorQuery(TechFoodContext dbContext, IImageUrlResolver imageUrlResolver) : IReadOnlyQuery<GetPreparationMonitorResult>
{
    private readonly TechFoodContext _dbContext = dbContext;
    private readonly IImageUrlResolver _imageUrlResolver = imageUrlResolver;
    public async Task<IEnumerable<GetPreparationMonitorResult>> GetAllAsync()
    {
        var preparations = await _dbContext.Preparations
        .Where(p => p.Status != PreparationStatusType.Cancelled)
        .Join(_dbContext.Orders,
            prep => prep.OrderId,
            order => order.Id,
            (prep, order) => new { prep, order })
        .SelectMany(po => po.order.Items.Select(item => new { po.prep, item }))
        .Join(_dbContext.Products,
            poi => poi.item.ProductId,
            product => product.Id,
            (poi, product) => new { poi.prep, poi.item, product })
        .GroupBy(x => new { x.prep.Id, x.prep.Number, x.prep.Status, x.prep.OrderId })
        .Select(g => new GetPreparationMonitorResult
        {
            preparationId = g.Key.Id,
            OrderId = g.Key.OrderId,
            Number = g.Key.Number,
            Status = g.Key.Status,
            Products = g.Select(x => new ProductResult
            {
                Name = x.product.Name,
                ImageUrl = x.product.ImageFileName,
                Quantity = x.item.Quantity
            }).ToList()
        })
        .ToListAsync();

        foreach (var prep in preparations)
        {
            foreach (var product in prep.Products)
            {
                product.ImageUrl = _imageUrlResolver.BuildFilePath(nameof(Product).ToLower(), product.ImageUrl);
            }
        }

        return preparations;
    }
}
