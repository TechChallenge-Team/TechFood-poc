using AutoMapper;
using TechFood.Application.Models.Product;
using TechFood.Domain.Entities;

namespace TechFood.Application.Common.AutoMapper;

internal class ProductToDtoProfile : Profile
{
    public ProductToDtoProfile()
    {
        CreateMap<Product, GetProductResult>();
    }
}
