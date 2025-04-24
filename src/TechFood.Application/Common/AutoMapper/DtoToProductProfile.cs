using AutoMapper;
using TechFood.Application.Models.Product;
using TechFood.Domain.Entities;

namespace TechFood.Application.Common.AutoMapper;

internal class DtoToProductProfile : Profile
{
    public DtoToProductProfile()
    {
        CreateMap<CreateProductRequest, Product>();
    }
}
