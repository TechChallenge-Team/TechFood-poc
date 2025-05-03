using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TechFood.Application.Common.Exceptions;
using TechFood.Application.Common.Resources;
using TechFood.Application.Models.Customer;
using TechFood.Application.Models.Product;
using TechFood.Application.UseCases.Interfaces;
using TechFood.Domain.Entities;
using TechFood.Domain.Repositories;
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
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<GetProductResult>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();

        await _unitOfWork.CommitAsync();

        return products.Select(_mapper.Map<GetProductResult>);
    }

    public async Task<GetProductResult?> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        return product is null ?
            null : _mapper.Map<GetProductResult>(product);
    }

    public async Task<CreateProductResult> CreateAsync(CreateProductRequest request)
    {
        await GetCategoryByIdAsync(request.CategoryId);

        var product = await _productRepository.AddAsync(
            _mapper.Map<CreateProductRequest, Product>(request, destination =>
        destination.AfterMap((src, dest) =>
        {
            dest.SetCategory(request.CategoryId);
            dest.SetOutOfStock(true);
        }
        )));

        await _unitOfWork.CommitAsync();

        return new CreateProductResult(product);
    }

    public async Task<UpdateProductResult?> UpdateAsync(Guid id, UpdateProductRequest request)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return null;
        }

        var category = await GetCategoryByIdAsync(request.CategoryId)
                ?? throw new NotFoundException(Exceptions.Product_CaregoryNotFound);

        product!.Update(
            request.Name,
            request.Description,
            request.ImageFileName,
            request.Price,
            category.Id);

        await _unitOfWork.CommitAsync();

        return _mapper.Map<UpdateProductResult>(product);
    }

    public async Task<UpdateProductResult?> UpdateOutOfStockAsync(Guid id, bool outOfStock)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return null;
        }

        product.SetOutOfStock(outOfStock);

        await _unitOfWork.CommitAsync();

        return _mapper.Map<UpdateProductResult>(product);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
        {
            return false;
        }

        await _productRepository.DeleteAsync(product);

        await _unitOfWork.CommitAsync();

        return true;
    }

    private async Task<Category> GetCategoryByIdAsync(Guid categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);

        return category ?? throw new NotFoundException(Exceptions.Product_CaregoryNotFound);
    }
}
