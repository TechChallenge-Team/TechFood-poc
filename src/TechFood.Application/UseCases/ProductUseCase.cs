using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using TechFood.Application.Common.Exceptions;
using TechFood.Application.Common.Resources;
using TechFood.Application.Common.Services.Interfaces;
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
    IImageUrlResolver imageUrlResolver,
    ILocalDiskImageStorageService localDiskImageStorageService,
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper)
    : IProductUseCase
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IMapper _mapper = mapper;
    private readonly ILocalDiskImageStorageService _localDiskImageStorageService = localDiskImageStorageService;
    private readonly IImageUrlResolver _imageUrlResolver = imageUrlResolver;

    public async Task<IEnumerable<GetProductResult>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();

        await _unitOfWork.CommitAsync();

        return products
                .Select(product => _mapper.Map<Product, GetProductResult>(
                    product,
                    options => options.AfterMap((product, dto) =>
                    {
                        dto.ImageUrl = _imageUrlResolver.BuildFilePath(_httpContextAccessor.HttpContext!.Request,
                                                                        nameof(Product).ToLower(),
                                                                        product.ImageFileName);
                    })));
    }

    public async Task<GetProductResult?> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        return product is null ?
            null : _mapper.Map<Product, GetProductResult>(product, options => options.AfterMap((product, dto) =>
            {
                dto.ImageUrl = _imageUrlResolver.BuildFilePath(_httpContextAccessor.HttpContext!.Request,
                                                                 nameof(Product).ToLower(),
                                                                 product.ImageFileName);
            }));
    }

    public async Task<CreateProductResult> CreateAsync(CreateProductRequest request)
    {
        var category = await GetCategoryByIdAsync(request.CategoryId)
                ?? throw new NotFoundException(Exceptions.Product_CaregoryNotFound);

        var imageFileName = _imageUrlResolver.CreateImageFileName(request.Name, request.File.ContentType);

        await _localDiskImageStorageService.SaveAsync(
                request.File.OpenReadStream(),
                imageFileName, nameof(Product));

        var productEntity = new Product(request.Name, request.Description, request.CategoryId, imageFileName, request.Price);

        await _productRepository.AddAsync(productEntity);

        await _unitOfWork.CommitAsync();

        var response = _mapper.Map<CreateProductResult>(productEntity, options => options.AfterMap((product, dto) =>
        {
            dto.ImageUrl = _imageUrlResolver.BuildFilePath(_httpContextAccessor.HttpContext!.Request, nameof(Product).ToLower(), imageFileName);
        }));
        return response;
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

        var imageFileName = product.ImageFileName;

        if (request.File != null)
        {
            imageFileName = _imageUrlResolver.CreateImageFileName(request.Name, request.File.ContentType);
            await _localDiskImageStorageService.SaveAsync(
                    request.File.OpenReadStream(),
                    imageFileName, nameof(Product));

            await _localDiskImageStorageService.DeleteAsync(product.ImageFileName, nameof(Product));
        }

        product!.Update(
            request.Name,
            request.Description,
            imageFileName,
            request.Price,
            category.Id);

        await _unitOfWork.CommitAsync();

        return _mapper.Map<Product, UpdateProductResult>(product, options => options.AfterMap((product, dto) =>
        {
            dto.ImageUrl = _imageUrlResolver.BuildFilePath(_httpContextAccessor.HttpContext!.Request,
                                                                nameof(Product).ToLower(),
                                                                product.ImageFileName);
        }));
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
        await _localDiskImageStorageService.DeleteAsync(product.ImageFileName, nameof(Product));

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
