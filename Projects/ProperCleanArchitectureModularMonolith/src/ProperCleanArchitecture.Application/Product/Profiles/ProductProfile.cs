using AutoMapper;
using ProperCleanArchitecture.Contracts.Product;

namespace ProperCleanArchitecture.Application.Product.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Domain.Entities.Product, ProductDto>();
    }
}