namespace ProperCleanArchitecture.Domain.Entities;

public class Order
{
    public int OrderId { get; set; }
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; } = null!;
    public virtual Product User { get; set; } = null!;
}