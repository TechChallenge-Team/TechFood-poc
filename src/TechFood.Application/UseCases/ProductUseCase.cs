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
    IMapper mapper) : IProductUseCase
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ProductResponseDto> GetProductByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        return _mapper.Map<Product, ProductResponseDto>(product);
    }

    public async Task DeleteProductAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        await _productRepository.DeleteAsync(product);

        return;
    }

    public Task CreateProductAsync(ProductRequestDto request)
        => _productRepository.CreateAsync(_mapper.Map<ProductRequestDto, Product>(request));

    public async Task<IEnumerable<ProductResponseDto>> GetProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return products.Select(_mapper.Map<Product, ProductResponseDto>);
    }

    public async Task UpdateProductAsync(Guid id, ProductRequestDto request)
    {
        var product = await _productRepository.GetByIdAsync(id);

        //var category = await _categoryRepository.GetByIdAsync(request.CategoryId);

        if (product is null)
        {
            throw new Exception("Produto não encontrado.");
        }

        product.Update(request.Name, request.Description, request.Price);

        await _productRepository.UpdateAsync();

        return;
    }
}
