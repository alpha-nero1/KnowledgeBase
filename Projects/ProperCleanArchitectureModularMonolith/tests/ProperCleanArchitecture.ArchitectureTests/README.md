# Clean Architecture Validation Tests

This project contains automated tests that enforce Clean Architecture principles and will **fail the build** if anti-patterns are detected.

## What's Protected

### 🏗️ Layer Dependencies
- **Domain Layer**: Cannot depend on Application, Infrastructure, or API layers
- **Application Layer**: Cannot depend on Infrastructure or API layers
- **Controllers**: Cannot directly access Infrastructure or Domain layers

### 📝 Naming Conventions
- Commands must end with `Command`
- Queries must end with `Query`
- Handlers must end with `Handler`
- Controllers must end with `Controller`
- DTOs must end with `Dto`
- Validators must end with `Validator`

### 🔒 Architecture Rules
- **Domain Entities**: Should not have dependencies on external frameworks (EF, ASP.NET, etc.)
- **Controllers**: Should not directly use Entity Framework
- **Application Services**: Should not return Domain entities directly
- **Commands/Queries**: Should be immutable (init-only properties)
- **Repository Interfaces**: Must be defined in Application layer, not Infrastructure

### 🎯 Dependency Injection Rules
- **Handlers**: Cannot depend on other handlers directly
- **Application Services**: Must depend on interfaces, not concrete implementations
- **No Static Dependencies**: Application layer should avoid static dependencies like `DateTime.Now`

## How It Works

These tests use **NetArchTest.Rules** to analyze your compiled assemblies and validate architectural constraints at build time.

### Running the Tests

```bash
# Run all architecture tests
dotnet test tests/ProperCleanArchitecture.ArchitectureTests/

# Run specific test categories
dotnet test --filter "FullyQualifiedName~CleanArchitectureTests"
dotnet test --filter "FullyQualifiedName~NamingConventionTests"
dotnet test --filter "FullyQualifiedName~DependencyValidationTests"
```

### Build Integration

These tests run as part of your build pipeline. If any architectural violation is detected:

1. ❌ **Build will fail**
2. 📝 **Detailed violation report** will be shown
3. 🚫 **Deployment will be blocked**

## Example Violations

### ❌ Bad: Domain depending on Infrastructure
```csharp
// In Domain layer - VIOLATION!
using ProperCleanArchitecture.Infrastructure;

public class Product
{
    public void Save(AppDbContext context) // ❌ Direct EF dependency
    {
        context.Products.Add(this);
    }
}
```

### ✅ Good: Clean separation
```csharp
// In Domain layer - CORRECT!
public class Product
{
    public string Name { get; private set; }

    public Product(string name)
    {
        Name = name;
    }
}
```

### ❌ Bad: Controller accessing Infrastructure
```csharp
// In API layer - VIOLATION!
public class ProductController : ControllerBase
{
    private readonly AppDbContext _context; // ❌ Direct EF dependency

    public async Task<IActionResult> Get()
    {
        var products = await _context.Products.ToListAsync(); // ❌ Direct database access
        return Ok(products);
    }
}
```

### ✅ Good: Using MediatR
```csharp
// In API layer - CORRECT!
public class ProductController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await Mediator.Send(new GetProductsQuery()); // ✅ Through Application layer
        return Ok(products);
    }
}
```

## Benefits

1. **🛡️ Prevents Architecture Drift**: Catches violations early in development
2. **📚 Self-Documenting**: Tests serve as living documentation of architectural decisions
3. **🔄 Continuous Validation**: Runs automatically on every build
4. **👥 Team Alignment**: Ensures all developers follow the same architectural principles
5. **🚀 Maintainability**: Keeps codebase clean and maintainable over time

## Extending the Tests

You can add new architectural rules by:

1. Creating new test methods in existing test classes
2. Adding new test classes for specific validation scenarios
3. Customizing the validation logic for your specific needs

Example of adding a new rule:

```csharp
[Fact]
public void Services_Should_Not_Have_Async_Void_Methods()
{
    var result = Types.InAssembly(ApplicationAssembly)
        .That()
        .HaveNameEndingWith("Service")
        .Should()
        .NotHaveMethodsWithReturnType(typeof(void))
        .And()
        .HaveNameStartingWith("Async")
        .GetResult();

    Assert.True(result.IsSuccessful, "Services should not have async void methods");
}
```

## Configuration

The tests automatically discover your project assemblies. If you rename projects or add new layers, update the assembly references in the test classes:

```csharp
private static readonly Assembly DomainAssembly = typeof(Domain.Entities.Product).Assembly;
private static readonly Assembly ApplicationAssembly = typeof(Application.Product.DependencyInjection).Assembly;
// ... etc
```
