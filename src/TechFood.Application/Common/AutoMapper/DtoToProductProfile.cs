using AutoMapper;
using TechFood.Application.Models;
using TechFood.Domain.Entities;

namespace TechFood.Application.Common.AutoMapper;

internal class DtoToProductProfile : Profile
{
    public DtoToProductProfile()
    {
        CreateMap<ProductRequestDto, Product>();
    }
}
