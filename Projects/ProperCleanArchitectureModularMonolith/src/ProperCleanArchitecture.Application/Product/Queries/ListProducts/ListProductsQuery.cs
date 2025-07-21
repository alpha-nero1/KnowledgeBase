using MediatR;
using ProperCleanArchitecture.Contracts.Product;

namespace ProperCleanArchitecture.Application.Product.Queries.ListProducts;

/// <summary>
/// List all the products for purchase in the system.
/// </summary>
public record ListProductsQuery : IRequest<IEnumerable<ProductDto>>;
