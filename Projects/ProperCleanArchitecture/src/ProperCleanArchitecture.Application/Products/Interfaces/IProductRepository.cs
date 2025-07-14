using ProperCleanArchitecture.Application.Products.Dtos;

namespace ProperCleanArchitecture.Application.Products.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
}