using MediatR;
using ProperCleanArchitecture.Contracts.Product;

namespace ProperCleanArchitecture.Application.Product.Queries.ListProducts;

public record ListProductsQuery : IRequest<IEnumerable<ProductDto>>
{}
