using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using TechFood.Application.Common.Services.Interfaces;
using TechFood.Application.Models.Category;
using TechFood.Application.UseCases.Interfaces;
using TechFood.Domain.Entities;
using TechFood.Domain.Repositories;
using TechFood.Domain.UoW;

namespace TechFood.Application.UseCases
{
    internal class CategoryUseCase(
        IMapper mapper,
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork,
        ILocalDiskImageStorageService localDiskImageStorageService,
        IHttpContextAccessor httpContextAccessor,
        IImageUrlResolver imageUrlResolver
        ) : ICategoryUseCase
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILocalDiskImageStorageService _localDiskImageStorageService = localDiskImageStorageService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IImageUrlResolver _imageUrlResolver = imageUrlResolver;

        public async Task<CategoryResponse> AddCategoryAsync(CreateCategoryRequest category)
        {
            var imageFileName = _imageUrlResolver.CreateImageFileName(category.Name, category.File.ContentType);

            var categoryEntity = new Category(category.Name, imageFileName);

            await _localDiskImageStorageService.SaveAsync(
                category.File.OpenReadStream(),
                imageFileName, nameof(Category));

            await _categoryRepository.AddAsync(categoryEntity);

            await _unitOfWork.CommitAsync();

            var result = _mapper.Map<CategoryResponse>(categoryEntity, options => options.AfterMap((category, dto) =>
            {
                dto.FilePath = _imageUrlResolver.BuildFilePath(_httpContextAccessor.HttpContext?.Request, nameof(Category).ToLower(), imageFileName);
            }));
            return result;
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category != null)
            {
                _categoryRepository.Delete(category);
                await _unitOfWork.CommitAsync();

                await _localDiskImageStorageService.DeleteAsync(category.ImageFileName, nameof(Category));

                return true;
            }

            return false;
        }

        public async Task<IEnumerable<CategoryResponse>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return categories
                .Select(category => _mapper.Map<Category, CategoryResponse>(
                    category,
                    options => options.AfterMap((category, dto) =>
                    {
                        dto.FilePath = _imageUrlResolver.BuildFilePath(_httpContextAccessor.HttpContext?.Request,
                                                                        nameof(Category).ToLower(),
                                                                        category.ImageFileName);
                    })));
        }

        public async Task<CategoryResponse> GetCategoryByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category != null)
            {
                return _mapper.Map<Category, CategoryResponse>(category,
                   options => options.AfterMap((category, dto) =>
                   {
                       dto.FilePath = _imageUrlResolver.BuildFilePath(_httpContextAccessor.HttpContext?.Request,
                                                                        nameof(Category).ToLower(),
                                                                        category.ImageFileName);
                   }));
            }

            return null;
        }

        public async Task<CategoryResponse> UpdateCategoryAsync(Guid id, UpdateCategoryRequest updateCategoryRequest)
        {

            var categoryRepository = await _categoryRepository.GetByIdAsync(id);

            var imageFileName = categoryRepository?.ImageFileName;

            if (categoryRepository != null)
            {
                if (updateCategoryRequest.File != null)
                {
                    imageFileName = _imageUrlResolver.CreateImageFileName(updateCategoryRequest.Name, updateCategoryRequest.File.ContentType);

                    await _localDiskImageStorageService.SaveAsync(
                            updateCategoryRequest.File.OpenReadStream(),
                            imageFileName, nameof(Category));

                    await _localDiskImageStorageService.DeleteAsync(categoryRepository.ImageFileName, nameof(Category));
                }

                categoryRepository.UpdateCategory(updateCategoryRequest.Name, imageFileName);

                await _unitOfWork.CommitAsync();

                return _mapper.Map<Category, CategoryResponse>(categoryRepository, options => options.AfterMap((category, dto) =>
                {
                    dto.FilePath = _imageUrlResolver.BuildFilePath(_httpContextAccessor.HttpContext?.Request,
                                                                        nameof(Category).ToLower(),
                                                                        category.ImageFileName);
                }));
            }

            return null;
        }
    }
}
