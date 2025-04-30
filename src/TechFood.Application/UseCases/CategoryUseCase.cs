using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
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
        IConfiguration appConfiguration,
        IUnitOfWork unitOfWork) : ICategoryUseCase
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IConfiguration _appConfiguration = appConfiguration;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<CreateCategoryResponse> AddCategoryAsync(CreateCategoryRequest category)
        {
            var categoryEntity = new Category(category.Name, category.ImageFileName);

            await _categoryRepository.AddAsync(categoryEntity);

            await _unitOfWork.CommitAsync();

            var result = _mapper.Map<CreateCategoryResponse>(categoryEntity);
            return result;
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            var entity = await _categoryRepository.GetByIdAsync(id);

            if (entity != null)
            {
                await _categoryRepository.DeleteAsync(entity);
                await _unitOfWork.CommitAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<CreateCategoryResponse>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return categories
                .Select(category => _mapper.Map<Category, CreateCategoryResponse>(
                    category,
                    options => options.AfterMap((category, dto) =>
                    {
                        dto.ImageFileName = string.Concat(
                            _appConfiguration["TechFoodStaticImagesUrl"],
                            "/categories/",
                            category.ImageFileName);
                    })));
        }

        public async Task<CreateCategoryResponse> GetCategoryByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category != null)
            {
                return _mapper.Map<Category, CreateCategoryResponse>(category,
                   options => options.AfterMap((category, dto) =>
                   {
                       dto.ImageFileName = string.Concat(
                           _appConfiguration["TechFoodStaticImagesUrl"],
                           "/categories/",
                           category.ImageFileName);
                   }));
            }

            return null;
        }

        public async Task<CreateCategoryResponse> UpdateCategoryAsync(Guid id, CreateCategoryRequest category)
        {
            var categoryRepository = await _categoryRepository.GetByIdAsync(id);

            if (categoryRepository != null)
            {
                categoryRepository.UpdateCategory(category.Name, category.ImageFileName);

                await _unitOfWork.CommitAsync();

                return _mapper.Map<Category, CreateCategoryResponse>(categoryRepository);
            }

            return null;
        }
    }
}
