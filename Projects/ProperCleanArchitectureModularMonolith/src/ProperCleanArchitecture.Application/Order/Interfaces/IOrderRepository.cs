namespace ProperCleanArchitecture.Application.Order.Interfaces;

public interface IOrderRepository
{
    // Read operations
    Task<IEnumerable<Domain.Entities.Order>> ListAsync(CancellationToken ct);
    Task<Domain.Entities.Order?> GetByIdAsync(int id, CancellationToken ct);
    Task<IEnumerable<Domain.Entities.Order>> GetByUserIdAsync(int userId, CancellationToken ct);
    Task<IEnumerable<Domain.Entities.Order>> GetByProductIdAsync(Guid productId, CancellationToken ct);
    Task<bool> ExistsAsync(int id, CancellationToken ct);

    // Create operation
    Task<Domain.Entities.Order> CreateAsync(Domain.Entities.Order order, CancellationToken ct);

    // Update operation
    Task<Domain.Entities.Order> UpdateAsync(Domain.Entities.Order order, CancellationToken ct);

    // Delete operations
    Task<bool> DeleteAsync(int id, CancellationToken ct);

    // Bulk operations
    Task<IEnumerable<Domain.Entities.Order>> CreateBulkAsync(IEnumerable<Domain.Entities.Order> orders, CancellationToken ct);
    Task<int> GetCountAsync(CancellationToken ct);
    Task<int> GetCountByUserIdAsync(int userId, CancellationToken ct);
}
