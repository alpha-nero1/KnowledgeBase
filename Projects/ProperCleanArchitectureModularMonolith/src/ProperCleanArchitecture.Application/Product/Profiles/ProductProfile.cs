using AutoMapper;
using ProperCleanArchitecture.Application.Product.DTOs;
using ProperCleanArchitecture.Domain.Entities;

namespace ProperCleanArchitecture.Application.Product.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>();
    }
}