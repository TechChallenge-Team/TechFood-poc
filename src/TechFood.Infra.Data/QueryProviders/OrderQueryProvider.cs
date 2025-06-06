using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechFood.Application.Common.Services.Interfaces;
using TechFood.Application.QueryProvider;
using TechFood.Application.UseCases.Order.Queries;
using TechFood.Domain.Entities;
using TechFood.Domain.Enums;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.QueryProviders;

internal class OrderQueryProvider(
    IImageUrlResolver imageUrlResolver,
    TechFoodContext techFoodContext) : IOrderQueryProvider
{
    public async Task<IEnumerable<GetOrdersQuery.Result>> GetOrdersAsync(GetOrdersQuery query)
    {
        var products = await techFoodContext.Products.ToListAsync();

        var result = await techFoodContext.Orders
            .Include(order => order.Items)
            .OrderBy(order => order.CreatedAt)
            .Join(
                techFoodContext.Customers,
                order => order.CustomerId,
                customer => customer.Id,
                (order, customer) => new
                {
                    Order = order,
                    Customer = customer
                }
            )
            .ToListAsync();

        return result.Select(data => new GetOrdersQuery.Result
        {
            Id = data.Order.Id,
            Status = data.Order.Status,
            CreatedAt = data.Order.CreatedAt,
            Number = data.Order.Number,
            Amount = data.Order.Amount,
            Customer = new GetOrdersQuery.Result.OrderCustomer
            {
                Name = data.Customer.Name.FullName,
            },
            Items = data.Order.Items.Select(item =>
            {
                var product = products.FirstOrDefault(p => p.Id == item.ProductId);
                return new GetOrdersQuery.Result.OrderItem
                {
                    Id = item.Id,
                    Name = product!.Name,
                    ImageUrl = imageUrlResolver.BuildFilePath(nameof(Product).ToLower(), product.ImageFileName),
                    Price = item.UnitPrice,
                    Quantity = item.Quantity
                };
            }).ToList()
        });
    }

    public async Task<IEnumerable<GetReadyOrdersQuery.Result>> GetReadyOrdersAsync(GetReadyOrdersQuery query)
    {
        var result = await techFoodContext.Orders
            .Include(order => order.Items)
            .Where(order => order.Status == OrderStatusType.Ready)
            .OrderBy(order => order.CreatedAt)
            .ToListAsync();

        return result.Select(data => new GetReadyOrdersQuery.Result
        {
            Id = data.Id,
            Number = data.Number,
            CreatedAt = data.CreatedAt
        });
    }
}
