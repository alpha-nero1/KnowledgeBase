using AutoMapper;
using MediatR;
using ProperCleanArchitecture.Application.Product.Interfaces;
using ProperCleanArchitecture.Application.Shared.Exceptions;
using ProperCleanArchitecture.Contracts.Product;

namespace ProperCleanArchitecture.Application.Product.Queries.ListProducts;

public class ListProductsQueryHandler : IRequestHandler<ListProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ListProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
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