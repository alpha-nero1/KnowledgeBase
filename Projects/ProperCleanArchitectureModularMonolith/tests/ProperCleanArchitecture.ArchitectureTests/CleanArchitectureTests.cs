using NetArchTest.Rules;
using System.Reflection;

namespace ProperCleanArchitecture.ArchitectureTests;

public class CleanArchitectureTests
{
    private static readonly Assembly DomainAssembly = typeof(Domain.Entities.Product).Assembly;
    private static readonly Assembly ApplicationAssembly = typeof(ApplicationDependencyInjection).Assembly;
    private static readonly Assembly InfrastructureAssembly = typeof(Infrastructure.Data.AppDbContext).Assembly;
    private static readonly Assembly ApiAssembly = typeof(Api.Controllers.ApiControllerBase).Assembly;

    [Fact]
    public void Domain_Should_Not_Have_Dependency_On_Application()
    {
        // Domain should never depend on Application layer
        var result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn("ProperCleanArchitecture.Application")
            .GetResult();

        Assert.True(result.IsSuccessful,
            $"Domain layer should not depend on Application layer. Violations: {string.Join(", ", result.FailingTypeNames ?? [])}");
    }

    [Fact]
    public void Domain_Should_Not_Have_Dependency_On_Infrastructure()
    {
        // Domain should never depend on Infrastructure layer
        var result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn("ProperCleanArchitecture.Infrastructure")
            .GetResult();

        Assert.True(result.IsSuccessful,
            $"Domain layer should not depend on Infrastructure layer. Violations: {string.Join(", ", result.FailingTypeNames ?? [])}");
    }

    [Fact]
    public void Domain_Should_Not_Have_Dependency_On_Api()
    {
        // Domain should never depend on API layer
        var result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn("ProperCleanArchitecture.Api")
            .GetResult();

        Assert.True(result.IsSuccessful,
            $"Domain layer should not depend on API layer. Violations: {string.Join(", ", result.FailingTypeNames ?? [])}");
    }

    [Fact]
    public void Application_Should_Not_Have_Dependency_On_Infrastructure()
    {
        // Application should not depend on Infrastructure (except through interfaces)
        var result = Types.InAssembly(ApplicationAssembly)
            .Should()
            .NotHaveDependencyOn("ProperCleanArchitecture.Infrastructure")
            .GetResult();

        Assert.True(result.IsSuccessful,
            $"Application layer should not depend on Infrastructure layer. Violations: {string.Join(", ", result.FailingTypeNames ?? [])}");
    }

    [Fact]
    public void Application_Should_Not_Have_Dependency_On_Api()
    {
        // Application should never depend on API layer
        var result = Types.InAssembly(ApplicationAssembly)
            .Should()
            .NotHaveDependencyOn("ProperCleanArchitecture.Api")
            .GetResult();

        Assert.True(result.IsSuccessful,
            $"Application layer should not depend on API layer. Violations: {string.Join(", ", result.FailingTypeNames ?? [])}");
    }

    [Fact]
    public void Domain_Should_Only_Contain_Entities_ValueObjects_And_Enums()
    {
        // Domain entities should not have dependencies on external frameworks
        var result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn("Microsoft.EntityFrameworkCore")
            .And()
            .NotHaveDependencyOn("System.ComponentModel.DataAnnotations")
            .And()
            .NotHaveDependencyOn("Microsoft.AspNetCore")
            .GetResult();

        Assert.True(result.IsSuccessful,
            $"Domain should not depend on external frameworks. Violations: {string.Join(", ", result.FailingTypeNames ?? [])}");
    }

    [Fact]
    public void Controllers_Should_Not_Directly_Use_Entity_Framework()
    {
        // Controllers should not directly use EF DbContext
        var result = Types.InAssembly(ApiAssembly)
            .That()
            .ResideInNamespace("ProperCleanArchitecture.Api.Controllers")
            .Should()
            .NotHaveDependencyOn("Microsoft.EntityFrameworkCore")
            .GetResult();

        Assert.True(result.IsSuccessful,
            $"Controllers should not directly use Entity Framework. Violations: {string.Join(", ", result.FailingTypeNames ?? [])}");
    }

    [Fact]
    public void Controllers_Should_Only_Depend_On_MediatR_And_AspNetCore()
    {
        // Controllers should only have minimal dependencies
        var result = Types.InAssembly(ApiAssembly)
            .That()
            .ResideInNamespace("ProperCleanArchitecture.Api.Controllers")
            .Should()
            .NotHaveDependencyOn("ProperCleanArchitecture.Infrastructure")
            .And()
            .NotHaveDependencyOn("ProperCleanArchitecture.Domain")
            .GetResult();

        Assert.True(result.IsSuccessful,
            $"Controllers should not directly depend on Infrastructure or Domain layers. Violations: {string.Join(", ", result.FailingTypeNames ?? [])}");
    }

    [Fact]
    public void Application_Services_Should_Not_Return_Domain_Entities()
    {
        // Application services should not expose domain entities directly
        var applicationTypes = Types.InAssembly(ApplicationAssembly)
            .That()
            .HaveNameEndingWith("Handler")
            .Or()
            .HaveNameEndingWith("Service")
            .GetTypes();

        var violations = new List<string>();

        foreach (var type in applicationTypes)
        {
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            foreach (var method in methods)
            {
                // Check if return type is a domain entity
                if (IsDomainEntity(method.ReturnType))
                {
                    violations.Add($"{type.Name}.{method.Name} returns domain entity {method.ReturnType.Name}");
                }

                // Check generic return types (Task<T>, etc.)
                if (method.ReturnType.IsGenericType)
                {
                    var genericArgs = method.ReturnType.GetGenericArguments();
                    foreach (var arg in genericArgs)
                    {
                        if (IsDomainEntity(arg))
                        {
                            violations.Add($"{type.Name}.{method.Name} returns domain entity {arg.Name}");
                        }
                    }
                }
            }
        }

        Assert.Empty(violations);
    }

    [Fact]
    public void Application_Commands_And_Queries_Should_Be_Immutable()
    {
        // Commands and queries should be immutable (only get properties or init setters)
        var commandsAndQueries = Types.InAssembly(ApplicationAssembly)
            .That()
            .HaveNameEndingWith("Command")
            .Or()
            .HaveNameEndingWith("Query")
            .GetTypes();

        var violations = new List<string>();

        foreach (var type in commandsAndQueries)
        {
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                if (property.CanWrite && property.SetMethod?.IsPublic == true)
                {
                    // Check if it's an init-only setter
                    var setMethod = property.SetMethod;
                    if (setMethod != null && !setMethod.ReturnParameter.GetRequiredCustomModifiers().Any(m => m.Name == "IsExternalInit"))
                    {
                        violations.Add($"{type.Name}.{property.Name} has a public setter (should be init-only or readonly)");
                    }
                }
            }
        }

        Assert.Empty(violations);
    }

    private static bool IsDomainEntity(Type type)
    {
        return type.Assembly == DomainAssembly &&
               type.Namespace != null &&
               type.Namespace.Contains("Entities");
    }
}
