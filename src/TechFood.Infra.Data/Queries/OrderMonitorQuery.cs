using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechFood.Application.Common.Services.Interfaces;
using TechFood.Application.Models.OrderMonitor;
using TechFood.Domain.Entities;
using TechFood.Domain.Shared.Interfaces;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.Queries;

internal class OrderMonitorQuery(TechFoodContext dbContext, IImageUrlResolver imageUrlResolver) : IReadOnlyQuery<GetPreparationMonitorResult>
{
    private readonly TechFoodContext _dbContext = dbContext;
    private readonly IImageUrlResolver _imageUrlResolver = imageUrlResolver;
    public async Task<IEnumerable<GetPreparationMonitorResult>> GetAllAsync()
    {
        var orders = await _dbContext.Orders
       .Where(o => o.Status == Domain.Enums.OrderStatusType.Paid
           || o.Status == Domain.Enums.OrderStatusType.InPreparation)
       .Select(order => new
       {
           order.Id,
           order.Status,
           order.Number,
           Products = order.Items.Select(oi => new
           {
               oi.ProductId,
               oi.Quantity,
               oi.UnitPrice,
               ProductName = _dbContext.Products
                   .Where(p => p.Id == oi.ProductId)
                   .Select(p => p.Name)
                   .FirstOrDefault(),
               ImageFileName = _dbContext.Products
                   .Where(p => p.Id == oi.ProductId)
                   .Select(p => p.ImageFileName)
                   .FirstOrDefault()
           }).ToList()
       }).ToListAsync();

        var result = orders.Select(order => new GetPreparationMonitorResult
        {
            OrderId = order.Id,
            Status = order.Status,
            Number = order.Number,
            Products = order.Products.Select(p => new ProductResult
            {
                Id = p.ProductId,
                Name = p.ProductName!,
                Quantity = p.Quantity,
                ImageUrl = _imageUrlResolver.BuildFilePath(nameof(Product).ToLower(), p.ImageFileName!)
            })
        });

        return result;
    }
}
