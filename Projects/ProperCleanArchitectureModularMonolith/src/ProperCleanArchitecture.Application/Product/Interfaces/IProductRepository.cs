namespace ProperCleanArchitecture.Application.Product.Interfaces;

public interface IProductRepository
{
    // Read operations
    Task<IEnumerable<Domain.Entities.Product>> ListAsync(CancellationToken ct);
    Task<Domain.Entities.Product?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IEnumerable<Domain.Entities.Product>> GetByNameAsync(string name, CancellationToken ct);
    Task<bool> ExistsAsync(Guid id, CancellationToken ct);

    // Create operation
    Task<Domain.Entities.Product> CreateAsync(Domain.Entities.Product product, CancellationToken ct);

    // Update operation
    Task<Domain.Entities.Product> UpdateAsync(Domain.Entities.Product product, CancellationToken ct);

    // Delete operations
    Task<bool> DeleteAsync(Guid id, CancellationToken ct);

    // Bulk operations
    Task<IEnumerable<Domain.Entities.Product>> CreateBulkAsync(IEnumerable<Domain.Entities.Product> products, CancellationToken ct);
    Task<int> GetCountAsync(CancellationToken ct);
}