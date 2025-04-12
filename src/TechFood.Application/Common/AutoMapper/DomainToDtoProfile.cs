using AutoMapper;
using TechFood.Application.Models;
using TechFood.Domain.Entities;

namespace TechFood.Application.Common.AutoMapper
{
    public class DomainToDtoProfile : Profile
    {
        public DomainToDtoProfile()
        {
            CreateMap<Category, CategoryDto>();
        }
    }
}
