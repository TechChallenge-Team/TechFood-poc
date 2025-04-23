using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TechFood.Application.Models;
using TechFood.Application.UseCases.Interfaces;
using TechFood.Domain.Entities;
using TechFood.Domain.Repositories;

namespace TechFood.Application.UseCases;

internal class ProductUseCase(
    IProductRepository productRepository,
    ICategoryRepository categoryRepository,
    IMapper mapper) : IProductUseCase
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    public async Task<ProductResponseDto> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        return _mapper.Map<Product, ProductResponseDto>(product);
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        await _productRepository.DeleteAsync(product);

        return;
    }

    public async Task CreateAsync(ProductRequestDto request)
    {
        var category = await GetCategoryByIdAsync(request.CategoryId);

        await _productRepository.CreateAsync(_mapper.Map<ProductRequestDto, Product>(request));
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();

        return products.Select(_mapper.Map<Product, ProductResponseDto>);
    }

    public async Task UpdateAsync(Guid id, ProductRequestDto request)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            throw new Exception("Produto não encontrado.");
        }

        //adicionar uma validação de request alterado?
        var category = await GetCategoryByIdAsync(request.CategoryId);

        product.Update(request.Name, request.Description, request.Price, category.Id);

        await _productRepository.UpdateAsync();

        return;
    }

    public async Task UpdateOutOfStockAsync(Guid id, bool outOfStock)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            throw new Exception("Produto não encontrado.");
        }

        product.SetOutOfStock(outOfStock);

        await _productRepository.UpdateAsync();
    }

    private async Task<Category> GetCategoryByIdAsync(Guid categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);

        if (category == null)
        {
            throw new Exception("Categoria inválida");
        }

        return category;
    }
}
