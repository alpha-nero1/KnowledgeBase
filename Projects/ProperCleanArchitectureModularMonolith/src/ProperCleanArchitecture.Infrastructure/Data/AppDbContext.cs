using Microsoft.EntityFrameworkCore;
using ProperCleanArchitecture.Domain.Entities;

namespace ProperCleanArchitecture.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Domain.Entities.Order> Orders => Set<Domain.Entities.Order>();
    public DbSet<User> Users => Set<User>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
}
