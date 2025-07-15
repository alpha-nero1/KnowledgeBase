# Clean Architecture Validation Demo

This script demonstrates how the architecture tests catch Clean Architecture violations and fail the build.

## 🚨 Current Violations Detected

### 1. Naming Convention Violation
**Issue**: `ListProductsHandler` is in Queries folder but doesn't end with "Query"  
**Location**: `ProperCleanArchitecture.Application.Product.Queries.ListProductsHandler`  
**Rule**: All classes in Queries folders must end with "Query"

**How to Fix:**
```bash
# Rename the class or create a proper Query class
# Example: ListProductsHandler should be ListProductsQuery
```

### 2. Domain Design Violation  
**Issue**: Domain entities only have parameterless constructors  
**Entities**: Order, Product, User  
**Rule**: Domain entities should have meaningful constructors, not just parameterless ones for EF

**How to Fix:**
```csharp
// ❌ Bad - Only parameterless constructor
public class Product
{
    public Product() { }
}

// ✅ Good - Meaningful constructor with parameterless for EF
public class Product  
{
    private Product() { } // For EF
    
    public Product(string name, decimal price) // Domain constructor
    {
        Name = name;
        Price = price;
    }
}
```

## 🔧 Running Architecture Tests

```bash
# Run all architecture tests
dotnet test tests/ProperCleanArchitecture.ArchitectureTests/

# Run specific test categories
dotnet test --filter "FullyQualifiedName~CleanArchitectureTests"
dotnet test --filter "FullyQualifiedName~NamingConventionTests"  
dotnet test --filter "FullyQualifiedName~DependencyValidationTests"
```

## ✅ Tests That Passed (21/23)

✅ **Layer Dependencies**: No violations found
- Domain doesn't depend on Application/Infrastructure/API
- Application doesn't depend on Infrastructure/API  
- Controllers don't directly access Infrastructure/Domain

✅ **Most Naming Conventions**: Properly enforced
- Commands end with "Command"
- Controllers end with "Controller"  
- DTOs end with "Dto"
- Validators end with "Validator"

✅ **Architecture Rules**: Clean separation maintained
- No direct EF usage in controllers
- No static dependencies in Application layer
- Repository interfaces in Application layer
- Proper dependency injection patterns

## 🎯 Build Integration

When violations are detected:

1. ❌ **Build FAILS** (exit code 1)
2. 📝 **Detailed violation report** shown  
3. 🚫 **Deployment blocked** until fixed
4. 👥 **Team alignment** enforced automatically

## 📈 Benefits Demonstrated

1. **🛡️ Prevents Architecture Drift**: Caught real violations in your code
2. **📚 Self-Documenting**: Tests show exactly what's expected  
3. **🔄 Continuous Validation**: Runs automatically on every build
4. **👥 Team Alignment**: Consistent architectural decisions
5. **🚀 Maintainability**: Keeps codebase clean over time

## 🔧 Fix Violations Script

```bash
# To fix the current violations and see all tests pass:

# 1. Fix naming convention (if ListProductsHandler should be a Query)
# Rename: ListProductsHandler -> ListProductsQuery

# 2. Fix domain entities (add proper constructors)
# Update Product.cs, Order.cs, User.cs with meaningful constructors

# 3. Re-run tests
dotnet test tests/ProperCleanArchitecture.ArchitectureTests/

# Expected result: All 23 tests should pass
```

This demonstrates that your Clean Architecture protection system is working perfectly! 🎉
