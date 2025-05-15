using System.Linq;
using System.Threading.Tasks;
using TechFood.Application.Common.Services.Interfaces;
using TechFood.Application.Models.Menu;
using TechFood.Application.UseCases.Interfaces;
using TechFood.Domain.Entities;
using TechFood.Domain.Repositories;

namespace TechFood.Application.UseCases;

internal class MenuUseCase(
    ICategoryRepository categoryRepository,
    IProductRepository productRepository,
    IImageUrlResolver imageUrlResolver) : IMenuUseCase
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IImageUrlResolver _imageUrlResolver = imageUrlResolver;

    public async Task<GetMenuResult> GetAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        var products = await _productRepository.GetAllAsync();

        var menu = new GetMenuResult
        {
            Categories = categories.Select(c => new GetMenuResult.Category
            {
                Id = c.Id,
                Name = c.Name,
                SortOrder = c.SortOrder,
                ImageUrl = _imageUrlResolver.BuildFilePath(
                    nameof(Category).ToLower(),
                    c.ImageFileName),
                Products = products.Where(p => p.CategoryId == c.Id).Select(p => new GetMenuResult.Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    CategoryId = p.CategoryId,
                    Description = p.Description,
                    Price = p.Price,
                    ImageUrl = _imageUrlResolver.BuildFilePath(
                        nameof(Product).ToLower(),
                        p.ImageFileName)
                }).ToList()
            }).ToList()
        };

        return menu;
    }
}
