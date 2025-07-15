using Microsoft.EntityFrameworkCore;
using ProperCleanArchitecture.Application.Product.Interfaces;
using ProperCleanArchitecture.Infrastructure.Data;

namespace ProperCleanArchitecture.Infrastructure.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Domain.Entities.Product>> ListAsync(CancellationToken ct)
            => await _dbContext.Products.ToListAsync(ct);
    }
}