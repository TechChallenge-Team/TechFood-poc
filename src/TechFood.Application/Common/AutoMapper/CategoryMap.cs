using AutoMapper;
using TechFood.Application.Models.Category;
using TechFood.Domain.Entities;

namespace TechFood.Application.Common.AutoMapper;

public class CategoryMap : Profile
{
    public CategoryMap()
    {
        CreateMap<Category, GetCategoryResult>();
        CreateMap<Category, UpdateCategoryResult>();
    }
}
