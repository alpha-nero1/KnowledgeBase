using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

public class Order
{
    public int OrderId { get; set; }
    public string ProductName { get; set; } = "";
    public int Quantity { get; set; }
}

public class OrdersDbContext : DbContext
{
    public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options) { }
    public DbSet<Order> Orders => Set<Order>();
}

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrdersDbContext _db;
    public OrderController(OrdersDbContext db) => _db = db;

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] Order order)
    {
        _db.Orders.Add(order);
        await _db.SaveChangesAsync();
        return Ok(new { orderId = order.OrderId });
    }
}
