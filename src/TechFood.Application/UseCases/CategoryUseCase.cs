using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using TechFood.Application.Models;
using TechFood.Application.UseCases.Interfaces;
using TechFood.Domain.Entities;
using TechFood.Domain.Repositories;
using TechFood.Domain.Shared.Entities;
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

        public async Task<CategoryViewModel> AddCategoryAsync(CategoryViewModel category)
        {
            var categoryEntity = _mapper.Map<Category>(category);

            await _categoryRepository.AddAsync(categoryEntity);

            await _unitOfWork.CommitAsync();

            var result = _mapper.Map<CategoryViewModel>(categoryEntity);
            return result;
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            var entity = await _categoryRepository.GetByIdAsync(id);
      
            await _categoryRepository.DeleteAsync(entity);

            return true;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return categories
                .Select(category => _mapper.Map<Category, CategoryViewModel>(
                    category,
                    options => options.AfterMap((category, dto) =>
                    {
                        dto.ImageUrl = string.Concat(
                            _appConfiguration["TechFoodStaticImagesUrl"],
                            "/categories/",
                            category.ImageFileName);
                    })));
        }

        public async Task<CategoryViewModel> GetCategoryByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return _mapper.Map<Category, CategoryViewModel>(category);
        }


        public async Task<CategoryViewModel> UpdateCategoryAsync(Guid id, CategoryViewModel category)
        {
            return new CategoryViewModel();
            var categoryUpdated = _mapper.Map<CategoryViewModel, Category>(category);
            var categoryRepository = await _categoryRepository.GetByIdAsync(id);
           
        }
    }
}
