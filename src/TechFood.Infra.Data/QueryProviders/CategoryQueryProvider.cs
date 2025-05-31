using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechFood.Application.Common.Services.Interfaces;
using TechFood.Application.QueryProvider;
using TechFood.Application.UseCases.Category.Queries;
using TechFood.Domain.Entities;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.QueryProviders;

internal class CategoryQueryProvider(
    TechFoodContext techFoodContext,
    IImageUrlResolver imageUrl) : ICategoryQueryProvider
{
    public async Task<IEnumerable<GetAllCategoryQuery.Result>> GetAllAsync(GetAllCategoryQuery query)
    {
        return await techFoodContext.Categories
            .OrderBy(category => category.SortOrder)
            .Select(category => new GetAllCategoryQuery.Result
            {
                Id = category.Id,
                Name = category.Name,
                ImageUrl = imageUrl.BuildFilePath(nameof(Category).ToLower(), category.ImageFileName)
            })
            .ToListAsync();
    }

    public async Task<GetCategoryByIdQuery.Result?> GetByIdAsync(GetCategoryByIdQuery query)
    {
        return await techFoodContext.Categories
            .Where(x => x.Id == query.Id)
            .Select(category => new GetCategoryByIdQuery.Result
            {
                Id = category.Id,
                Name = category.Name,
                ImageUrl = imageUrl.BuildFilePath(nameof(Category).ToLower(), category.ImageFileName)
            })
            .FirstOrDefaultAsync();
    }
}
