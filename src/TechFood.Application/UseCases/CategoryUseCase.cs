using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TechFood.Application.Common.Services.Interfaces;
using TechFood.Application.Models.Category;
using TechFood.Application.UseCases.Interfaces;
using TechFood.Domain.Entities;
using TechFood.Domain.Repositories;
using TechFood.Domain.UoW;

namespace TechFood.Application.UseCases;

internal class CategoryUseCase(
    IMapper mapper,
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork,
    ILocalDiskImageStorageService localDiskImageStorageService,
    IImageUrlResolver imageUrlResolver
    ) : ICategoryUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILocalDiskImageStorageService _localDiskImageStorageService = localDiskImageStorageService;
    private readonly IImageUrlResolver _imageUrlResolver = imageUrlResolver;

    public async Task<CategoryResponse> AddAsync(CreateCategoryRequest category)
    {
        var imageFileName = _imageUrlResolver.CreateImageFileName(category.Name, category.File.ContentType);

        var categoryEntity = new Category(category.Name, imageFileName, 0);

        await _localDiskImageStorageService.SaveAsync(
            category.File.OpenReadStream(),
            imageFileName, nameof(Category));

        await _categoryRepository.AddAsync(categoryEntity);

        await _unitOfWork.CommitAsync();

        var result = _mapper.Map<CategoryResponse>(categoryEntity, options => options.AfterMap((category, dto) =>
        {
            dto.ImageUrl = _imageUrlResolver.BuildFilePath(nameof(Category).ToLower(), imageFileName);
        }));
        return result;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category != null)
        {
            await _categoryRepository.DeleteAsync(category);
            await _unitOfWork.CommitAsync();

            await _localDiskImageStorageService.DeleteAsync(category.ImageFileName, nameof(Category));

            return true;
        }

        return false;
    }

    public async Task<IEnumerable<CategoryResponse>> ListAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();

        return categories
            .Select(category => _mapper.Map<Category, CategoryResponse>(
                category,
                options => options.AfterMap((category, dto) =>
                {
                    dto.ImageUrl = _imageUrlResolver.BuildFilePath(nameof(Category).ToLower(),
                                                                    category.ImageFileName);
                })));
    }

    public async Task<CategoryResponse?> GetByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        return category is null ? null : _mapper.Map<Category, CategoryResponse>(category,
               options => options.AfterMap((category, dto) =>
               {
                   dto.ImageUrl = _imageUrlResolver.BuildFilePath(nameof(Category).ToLower(),
                                                                   category.ImageFileName);
               }));
    }

    public async Task<CategoryResponse?> UpdateAsync(Guid id, UpdateCategoryRequest updateCategoryRequest)
    {

        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
        {
            return null;
        }

        var imageFileName = category.ImageFileName;

        if (updateCategoryRequest.File != null)
        {
            imageFileName = _imageUrlResolver.CreateImageFileName(updateCategoryRequest.Name, updateCategoryRequest.File.ContentType);

            await _localDiskImageStorageService.SaveAsync(
                    updateCategoryRequest.File.OpenReadStream(),
                    imageFileName, nameof(Category));

            await _localDiskImageStorageService.DeleteAsync(category.ImageFileName, nameof(Category));
        }

        category.UpdateAsync(updateCategoryRequest.Name, imageFileName);

        await _unitOfWork.CommitAsync();

        return _mapper.Map<Category, CategoryResponse>(category, options => options.AfterMap((category, dto) =>
        {
            dto.ImageUrl = _imageUrlResolver.BuildFilePath(nameof(Category).ToLower(),
                                                            category.ImageFileName);
        }));

    }
}
