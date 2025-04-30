using AutoMapper;
using TechFood.Application.Models.Category;
using TechFood.Domain.Entities;

namespace TechFood.Application.Common.AutoMapper
{
    public class DomainToDtoProfile : Profile
    {
        public DomainToDtoProfile()
        {
            CreateMap<Category, CreateCategoryResponse>();
        }
    }
}
