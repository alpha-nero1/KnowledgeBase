using AutoMapper;
using MediatR;
using ProperCleanArchitecture.Application.Product.DTOs;
using ProperCleanArchitecture.Application.Product.Interfaces;
using ProperCleanArchitecture.Application.Shared.Exceptions;

namespace ProperCleanArchitecture.Application.Product.Queries;

public record ListProductsQuery : IRequest<IEnumerable<ProductDto>>
{

}

public class ListProductsHandler : IRequestHandler<ListProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ListProductsHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> Handle(ListProductsQuery request, CancellationToken ct)
    {
        var product = await _productRepository.ListAsync(ct);
        if (product.Count() == 0)
        {
            throw new EntityNotFoundException();
        }
        return product.Select(_mapper.Map<ProductDto>);
    }
}