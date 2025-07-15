namespace ProperCleanArchitecture.Application.Product.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Domain.Entities.Product>> ListAsync(CancellationToken ct);
}