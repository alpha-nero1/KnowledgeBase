namespace ProperCleanArchitecture.Domain.Entities;

public class Order : EntityBase
{
    public int ParentOrderId { get; set; }
    public int OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int UserId { get; set; }
    public virtual Product Product { get; set; } = null!;
    public virtual User User { get; set; } = null!;
    public virtual Order ParentOrder { get; set; } = null!;
}