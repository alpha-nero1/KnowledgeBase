namespace ProperCleanArchitecture.Contracts.Order;

public class OrderDto : DtoBase
{
    public int OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int UserId { get; set; }
}