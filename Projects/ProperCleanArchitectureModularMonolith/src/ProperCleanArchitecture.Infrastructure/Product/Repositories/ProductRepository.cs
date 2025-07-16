using Microsoft.EntityFrameworkCore;
using ProperCleanArchitecture.Application.Product.Interfaces;
using ProperCleanArchitecture.Infrastructure.Data;

namespace ProperCleanArchitecture.Infrastructure.Products;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Read operations
    public async Task<IEnumerable<Domain.Entities.Product>> ListAsync(CancellationToken ct)
        => await _dbContext.Products
            .Where(p => p.DisabledAt == null)
            .OrderBy(p => p.Name)
            .ToListAsync(ct);

    public async Task<Domain.Entities.Product?> GetByIdAsync(Guid id, CancellationToken ct)
        => await _dbContext.Products
            .Where(p => p.ProductId == id && p.DisabledAt == null)
            .FirstOrDefaultAsync(ct);

    public async Task<IEnumerable<Domain.Entities.Product>> GetByNameAsync(string name, CancellationToken ct)
        => await _dbContext.Products
            .Where(p => p.Name.Contains(name) && p.DisabledAt == null)
            .OrderBy(p => p.Name)
            .ToListAsync(ct);

    public async Task<bool> ExistsAsync(Guid id, CancellationToken ct)
        => await _dbContext.Products
            .AnyAsync(p => p.ProductId == id && p.DisabledAt == null, ct);

    // Create operation
    public async Task<Domain.Entities.Product> CreateAsync(Domain.Entities.Product product, CancellationToken ct)
    {
        product.CreatedAt = DateTime.UtcNow;
        product.UpdatedAt = DateTime.UtcNow;

        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync(ct);

        return product;
    }

    // Update operation
    public async Task<Domain.Entities.Product> UpdateAsync(Domain.Entities.Product product, CancellationToken ct)
    {
        var existingProduct = await _dbContext.Products
            .FirstOrDefaultAsync(p => p.ProductId == product.ProductId, ct);

        if (existingProduct == null)
            throw new InvalidOperationException($"Product with ID {product.ProductId} not found");

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(ct);

        return existingProduct;
    }

    // Delete operations
    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct)
    {
        var product = await _dbContext.Products
            .FirstOrDefaultAsync(p => p.ProductId == id && p.DisabledAt == null, ct);

        if (product == null)
            return false;

        product.DisabledAt = DateTime.UtcNow;
        product.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(ct);

        return true;
    }

    // Bulk operations
    public async Task<IEnumerable<Domain.Entities.Product>> CreateBulkAsync(IEnumerable<Domain.Entities.Product> products, CancellationToken ct)
    {
        var now = DateTime.UtcNow;
        var productList = products.ToList();

        foreach (var product in productList)
        {
            product.CreatedAt = now;
            product.UpdatedAt = now;
        }

        _dbContext.Products.AddRange(productList);
        await _dbContext.SaveChangesAsync(ct);

        return productList;
    }

    public async Task<int> GetCountAsync(CancellationToken ct)
        => await _dbContext.Products
            .CountAsync(p => p.DisabledAt == null, ct);
}