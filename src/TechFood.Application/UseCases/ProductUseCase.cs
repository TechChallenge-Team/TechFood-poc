using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TechFood.Application.Models.Product;
using TechFood.Application.UseCases.Interfaces;
using TechFood.Domain.Entities;
using TechFood.Domain.Repositories;
using TechFood.Domain.Shared.Exceptions;
using TechFood.Domain.UoW;

namespace TechFood.Application.UseCases;

internal class ProductUseCase(
    IProductRepository productRepository,
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IProductUseCase
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<GetProductResult> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        await _unitOfWork.CommitAsync();

        return _mapper.Map<Product, GetProductResult>(product);
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        await _productRepository.DeleteAsync(product);

        await _unitOfWork.CommitAsync();

        return;
    }

    public async Task CreateAsync(CreateProductRequest request)
    {
        await GetCategoryByIdAsync(request.CategoryId);

        await _productRepository.CreateAsync(_mapper.Map<CreateProductRequest, Product>(request, destination =>
        destination.AfterMap((src, dest) => {
            dest.SetCategory(request.CategoryId);
            dest.SetOutOfStock(true);
        }
        )));

        await _unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<GetProductResult>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();

        await _unitOfWork.CommitAsync();

        return products.Select(_mapper.Map<Product, GetProductResult>);
    }

    public async Task UpdateAsync(Guid id, UpdateProductRequest request)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            throw new DomainException("Produto não encontrado.");
        }

        var category = await GetCategoryByIdAsync(request.CategoryId);

        product.Update(request.Name, request.Description,
            request.ImageFileName, request.Price, category.Id);

        await _unitOfWork.CommitAsync();

        return;
    }

    public async Task UpdateOutOfStockAsync(Guid id, bool outOfStock)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            throw new DomainException("Produto não encontrado.");
        }

        product.SetOutOfStock(outOfStock);

        await _unitOfWork.CommitAsync();
    }

    private async Task<Category> GetCategoryByIdAsync(Guid categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);

        if (category == null)
        {
            throw new DomainException("Categoria inválida");
        }

        return category;
    }
}
