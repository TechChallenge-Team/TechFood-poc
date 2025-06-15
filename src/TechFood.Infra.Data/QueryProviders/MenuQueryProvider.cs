using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechFood.Application.Common.Services.Interfaces;
using TechFood.Application.QueryProvider;
using TechFood.Application.UseCases.Menu.Queries;
using TechFood.Domain.Entities;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.QueryProviders;

internal class MenuQueryProvider(TechFoodContext techFoodContext, IImageUrlResolver imageUrl) : IMenuQueryProvider
{
    public async Task<GetMenuQuery.Result> GetAsync(GetMenuQuery query)
    {
        var data = await techFoodContext.Products
            .AsNoTracking()
            .Join(
                techFoodContext.Categories,
                product => product.CategoryId,
                category => category.Id,
                (product, category) => new
                {
                    Product = product,
                    Category = category
                })
            .ToListAsync();

        var categories = data.GroupBy(x => x.Category)
            .Select(g => new GetMenuQuery.Result.Category
            {
                Id = g.Key.Id,
                Name = g.Key.Name,
                ImageUrl = imageUrl.BuildFilePath(nameof(Category).ToLower(), g.Key.ImageFileName),
                SortOrder = g.Key.SortOrder,
                Products = g.Select(x => new GetMenuQuery.Result.Product
                {
                    Id = x.Product.Id,
                    CategoryId = x.Product.CategoryId,
                    Name = x.Product.Name,
                    Description = x.Product.Description,
                    Price = x.Product.Price,
                    ImageUrl = imageUrl.BuildFilePath(nameof(Product).ToLower(), x.Product.ImageFileName)
                }).ToList()
            }).ToList();

        return new()
        {
            Categories = categories
        };
    }
}
