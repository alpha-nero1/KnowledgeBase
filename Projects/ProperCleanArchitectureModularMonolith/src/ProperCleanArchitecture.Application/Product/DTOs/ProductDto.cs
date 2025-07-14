namespace ProperCleanArchitecture.Application.Product.DTOs;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
}