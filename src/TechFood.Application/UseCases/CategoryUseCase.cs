using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechFood.Application.Models;
using TechFood.Application.UseCases.Interfaces;
using TechFood.Domain.Entities;
using TechFood.Domain.Repositories;

namespace TechFood.Application.UseCases
{
    internal class CategoryUseCase(
        IMapper mapper,
        ICategoryRepository categoryRepository,
        IConfiguration appConfiguration) : ICategoryUseCase
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IConfiguration _appConfiguration = appConfiguration;


        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return categories
                .Select(category => _mapper.Map<Category, CategoryDto>(
                    category,
                    options => options.AfterMap((category, dto) =>
                    {
                        dto.ImageUrl = string.Concat(
                            _appConfiguration["TechFoodStaticImagesUrl"],
                            "/discoveries/",
                            category.ImageFileName);
                    })));
        }
    }
}
