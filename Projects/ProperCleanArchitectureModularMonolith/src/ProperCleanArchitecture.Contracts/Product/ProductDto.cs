namespace ProperCleanArchitecture.Contracts.Product;

public class ProductDto : DtoBase
{
    public Guid ProductId { get; set; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
}