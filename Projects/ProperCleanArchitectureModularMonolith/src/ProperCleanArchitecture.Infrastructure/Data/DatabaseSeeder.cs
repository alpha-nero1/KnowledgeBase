using Microsoft.EntityFrameworkCore;
using ProperCleanArchitecture.Domain.Entities;

namespace ProperCleanArchitecture.Infrastructure.Data;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // Check if data already exists
        if (await context.Users.AnyAsync() || await context.Products.AnyAsync())
            return;

        var now = DateTime.UtcNow;

        // Seed Users
        var users = new List<User>
        {
            new User
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Pasword = "hashedpassword123", // In real app, this would be properly hashed
                CreatedAt = now,
                UpdatedAt = now
            },
            new User
            {
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                Pasword = "hashedpassword456",
                CreatedAt = now,
                UpdatedAt = now
            },
            new User
            {
                FirstName = "Bob",
                MiddleName = "M",
                LastName = "Johnson",
                Email = "bob.johnson@example.com",
                Pasword = "hashedpassword789",
                CreatedAt = now,
                UpdatedAt = now
            }
        };

        context.Users.AddRange(users);
        await context.SaveChangesAsync();

        // Seed Products
        var products = new List<Product>
        {
            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Laptop Computer",
                Price = 999.99m,
                CreatedAt = now,
                UpdatedAt = now
            },
            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Wireless Mouse",
                Price = 29.99m,
                CreatedAt = now,
                UpdatedAt = now
            },
            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "Mechanical Keyboard",
                Price = 149.99m,
                CreatedAt = now,
                UpdatedAt = now
            },
            new Product
            {
                ProductId = Guid.NewGuid(),
                Name = "USB-C Hub",
                Price = 79.99m,
                CreatedAt = now,
                UpdatedAt = now
            }
        };

        context.Products.AddRange(products);
        await context.SaveChangesAsync();

        // Seed Orders
        var orders = new List<Domain.Entities.Order>
        {
            new Domain.Entities.Order
            {
                ProductId = products[0].ProductId, // Laptop
                UserId = users[0].UserId, // John Doe
                CreatedAt = now,
                UpdatedAt = now
            },
            new Domain.Entities.Order
            {
                ProductId = products[1].ProductId, // Mouse
                UserId = users[0].UserId, // John Doe
                CreatedAt = now,
                UpdatedAt = now
            },
            new Domain.Entities.Order
            {
                ProductId = products[2].ProductId, // Keyboard
                UserId = users[1].UserId, // Jane Smith
                CreatedAt = now,
                UpdatedAt = now
            }
        };

        context.Orders.AddRange(orders);
        await context.SaveChangesAsync();
    }
}
