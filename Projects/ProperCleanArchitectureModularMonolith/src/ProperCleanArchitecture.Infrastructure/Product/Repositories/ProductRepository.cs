using ProperCleanArchitecture.Application.Product.DTOs;
using ProperCleanArchitecture.Application.Product.Interfaces;

namespace ProperCleanArchitecture.Infrastructure.Products
{
    public class ProductRepository : IProductRepository
    {
        public ProductRepository()
        {
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
        }
    }
}