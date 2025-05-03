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

namespace TechFood.Application.UseCases;

internal class CategoryUseCase(
    IMapper mapper,
    ICategoryRepository categoryRepository,
    IConfiguration appConfiguration,
    IUnitOfWork unitOfWork) : ICategoryUseCase
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IConfiguration _appConfiguration = appConfiguration;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<GetCategoryResult>> ListAllAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();

        return categories
            .Select(category => _mapper.Map<Category, GetCategoryResult>(
                category,
                options => options.AfterMap((category, dto) =>
                {
                    dto.ImageFileName = string.Concat(
                        _appConfiguration["TechFoodStaticImagesUrl"],
                        "/categories/",
                        category.ImageFileName);
                })));
    }

    public async Task<GetCategoryResult?> GetByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
        {
            return null;
        }

        return _mapper.Map<Category, GetCategoryResult>(category,
               options => options.AfterMap((category, dto) =>
               {
                   dto.ImageFileName = string.Concat(
                       _appConfiguration["TechFoodStaticImagesUrl"],
                       "/categories/",
                       category.ImageFileName);
               }));
    }

    public async Task<CreateCategoryResult> AddAsync(CreateCategoryRequest category)
    {
        var categoryEntity = new Category(category.Name, category.ImageFileName);

        await _categoryRepository.AddAsync(categoryEntity);

        await _unitOfWork.CommitAsync();

        var result = _mapper.Map<CreateCategoryResult>(categoryEntity);

        return result;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _categoryRepository.GetByIdAsync(id);

        if (entity == null)
        {
            return false;
        }

        await _categoryRepository.DeleteAsync(entity);

        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<UpdateCategoryResult?> UpdateAsync(Guid id, UpdateCategoryRequest request)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category == null)
        {
            return null;
        }

        category.UpdateAsync(request.Name, request.ImageFileName);

        await _unitOfWork.CommitAsync();

        return _mapper.Map<UpdateCategoryResult>(category);
    }
}
