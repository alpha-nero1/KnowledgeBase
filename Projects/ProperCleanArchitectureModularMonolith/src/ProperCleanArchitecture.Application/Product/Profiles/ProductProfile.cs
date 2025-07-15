using AutoMapper;
using ProperCleanArchitecture.Application.Product.DTOs;

namespace ProperCleanArchitecture.Application.Product.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Domain.Entities.Product, ProductDto>();
    }
}