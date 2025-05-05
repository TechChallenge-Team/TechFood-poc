using AutoMapper;
using TechFood.Application.Models.Customer;
using TechFood.Application.Models.Product;
using TechFood.Domain.Entities;

namespace TechFood.Application.Common.AutoMapper;

internal class ProductToDtoProfile : Profile
{
    public ProductToDtoProfile()
    {
        CreateMap<Product, GetProductResult>();
        CreateMap<Product, UpdateProductResult>();
        CreateMap<Product, CreateCustomerResult>();
        CreateMap<Product, GetProductResult>();
    }
}
