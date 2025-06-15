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
        var categories = await techFoodContext.Categories
            .AsNoTracking()
            .OrderBy(c => c.SortOrder)
            .Select(category => new GetMenuQuery.Result.Category
            {
                Id = category.Id,
                Name = category.Name,
                ImageUrl = imageUrl.BuildFilePath(nameof(Category).ToLower(), category.ImageFileName),
                SortOrder = category.SortOrder,
                Products = techFoodContext.Products
                    .Where(p => p.CategoryId == category.Id)
                    .Select(product => new GetMenuQuery.Result.Product
                    {
                        Id = product.Id,
                        CategoryId = product.CategoryId,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        ImageUrl = imageUrl.BuildFilePath(nameof(Product).ToLower(), product.ImageFileName)
                    }).ToList()
            }).ToListAsync();

        return new()
        {
            Categories = categories
        };
    }
}
