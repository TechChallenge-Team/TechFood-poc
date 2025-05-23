using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechFood.Application.Common.Services.Interfaces;
using TechFood.Application.QueryProvider;
using TechFood.Application.UseCases.Product.Queries;
using TechFood.Domain.Entities;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.QueryProviders;

internal class ProductQueryProvider(TechFoodContext techFoodContext, IImageUrlResolver imageUrl) : IProductQueryProvider
{
    public async Task<IEnumerable<GetAllProductQuery.Result>> GetAllAsync(GetAllProductQuery request)
    {
        return await techFoodContext.Products
            .Select(product => new GetAllProductQuery.Result
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = product.CategoryId,
                Description = product.Description,
                OutOfStock = product.OutOfStock,
                Price = product.Price,
                ImageUrl = imageUrl.BuildFilePath(nameof(Product).ToLower(), product.ImageFileName)
            }).ToListAsync();
    }

    public Task<GetProductByIdQuery.Result?> GetByIdAsync(GetProductByIdQuery request)
    {
        return techFoodContext.Products
            .Where(product => product.Id == request.Id)
            .Select(product => new GetProductByIdQuery.Result
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = product.CategoryId,
                Description = product.Description,
                OutOfStock = product.OutOfStock,
                Price = product.Price,
                ImageUrl = imageUrl.BuildFilePath(nameof(Product).ToLower(), product.ImageFileName)
            }).FirstOrDefaultAsync();
    }
}
