using ProperCleanArchitecture.Application.Product.DTOs;

namespace ProperCleanArchitecture.Application.Product.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
}