using AutoMapper;
using TechFood.Application.Models.Category;
using TechFood.Domain.Entities;

namespace TechFood.Application.Common.AutoMapper
{
    public class DtoToDomainProfile : Profile
    {
        public DtoToDomainProfile()
        {
            CreateMap<CreateCategoryRequest, Category>();
        }
    }
}
