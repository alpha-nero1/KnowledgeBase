namespace ProperCleanArchitecture.Domain.Entities;

public class Product : EntityBase
{
    public Guid ProductId { get; set; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
}