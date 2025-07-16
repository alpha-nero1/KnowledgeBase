using Microsoft.EntityFrameworkCore;
using ProperCleanArchitecture.Application.Order.Interfaces;
using ProperCleanArchitecture.Infrastructure.Data;
using OrderEntity = ProperCleanArchitecture.Domain.Entities.Order;

namespace ProperCleanArchitecture.Infrastructure.Order.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _dbContext;

    public OrderRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Read operations
    public async Task<IEnumerable<OrderEntity>> ListAsync(CancellationToken ct)
        => await _dbContext.Orders
            .Include(o => o.Product)
            .Include(o => o.User)
            .Where(o => o.DisabledAt == null)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync(ct);

    public async Task<OrderEntity?> GetByIdAsync(int id, CancellationToken ct)
        => await _dbContext.Orders
            .Include(o => o.Product)
            .Include(o => o.User)
            .Where(o => o.OrderId == id && o.DisabledAt == null)
            .FirstOrDefaultAsync(ct);

    public async Task<IEnumerable<OrderEntity>> GetByUserIdAsync(int userId, CancellationToken ct)
        => await _dbContext.Orders
            .Include(o => o.Product)
            .Include(o => o.User)
            .Where(o => o.UserId == userId && o.DisabledAt == null)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync(ct);

    public async Task<IEnumerable<OrderEntity>> GetByProductIdAsync(Guid productId, CancellationToken ct)
        => await _dbContext.Orders
            .Include(o => o.Product)
            .Include(o => o.User)
            .Where(o => o.ProductId == productId && o.DisabledAt == null)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync(ct);

    public async Task<bool> ExistsAsync(int id, CancellationToken ct)
        => await _dbContext.Orders
            .AnyAsync(o => o.OrderId == id && o.DisabledAt == null, ct);

    // Create operation
    public async Task<OrderEntity> CreateAsync(OrderEntity order, CancellationToken ct)
    {
        order.CreatedAt = DateTime.UtcNow;
        order.UpdatedAt = DateTime.UtcNow;

        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync(ct);

        // Return the order with navigation properties loaded
        return await GetByIdAsync(order.OrderId, ct) ?? order;
    }

    // Update operation
    public async Task<OrderEntity> UpdateAsync(OrderEntity order, CancellationToken ct)
    {
        var existingOrder = await _dbContext.Orders
            .FirstOrDefaultAsync(o => o.OrderId == order.OrderId, ct);

        if (existingOrder == null)
            throw new InvalidOperationException($"Order with ID {order.OrderId} not found");

        // Validate that Product and User exist if they're being changed
        if (existingOrder.ProductId != order.ProductId)
        {
            var productExists = await _dbContext.Products
                .AnyAsync(p => p.ProductId == order.ProductId && p.DisabledAt == null, ct);
            if (!productExists)
                throw new InvalidOperationException($"Product with ID {order.ProductId} not found or is disabled");
        }

        if (existingOrder.UserId != order.UserId)
        {
            var userExists = await _dbContext.Users
                .AnyAsync(u => u.UserId == order.UserId && u.DisabledAt == null, ct);
            if (!userExists)
                throw new InvalidOperationException($"User with ID {order.UserId} not found or is disabled");
        }

        existingOrder.ProductId = order.ProductId;
        existingOrder.UserId = order.UserId;
        existingOrder.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(ct);

        // Return the order with navigation properties loaded
        return await GetByIdAsync(existingOrder.OrderId, ct) ?? existingOrder;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var order = await _dbContext.Orders
            .FirstOrDefaultAsync(o => o.OrderId == id && o.DisabledAt == null, ct);

        if (order == null)
            return false;

        order.DisabledAt = DateTime.UtcNow;
        order.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(ct);

        return true;
    }

    // Bulk operations
    public async Task<IEnumerable<OrderEntity>> CreateBulkAsync(IEnumerable<OrderEntity> orders, CancellationToken ct)
    {
        var now = DateTime.UtcNow;
        var orderList = orders.ToList();

        // Validate all products and users exist
        var productIds = orderList.Select(o => o.ProductId).Distinct().ToList();
        var userIds = orderList.Select(o => o.UserId).Distinct().ToList();

        var existingProductIds = await _dbContext.Products
            .Where(p => productIds.Contains(p.ProductId) && p.DisabledAt == null)
            .Select(p => p.ProductId)
            .ToListAsync(ct);

        var existingUserIds = await _dbContext.Users
            .Where(u => userIds.Contains(u.UserId) && u.DisabledAt == null)
            .Select(u => u.UserId)
            .ToListAsync(ct);

        var missingProducts = productIds.Except(existingProductIds).ToList();
        var missingUsers = userIds.Except(existingUserIds).ToList();

        if (missingProducts.Any())
            throw new InvalidOperationException($"Products not found or disabled: {string.Join(", ", missingProducts)}");

        if (missingUsers.Any())
            throw new InvalidOperationException($"Users not found or disabled: {string.Join(", ", missingUsers)}");

        foreach (var order in orderList)
        {
            order.CreatedAt = now;
            order.UpdatedAt = now;
        }

        _dbContext.Orders.AddRange(orderList);
        await _dbContext.SaveChangesAsync(ct);

        return orderList;
    }

    public async Task<int> GetCountAsync(CancellationToken ct)
        => await _dbContext.Orders
            .CountAsync(o => o.DisabledAt == null, ct);

    public async Task<int> GetCountByUserIdAsync(int userId, CancellationToken ct)
        => await _dbContext.Orders
            .CountAsync(o => o.UserId == userId && o.DisabledAt == null, ct);
}