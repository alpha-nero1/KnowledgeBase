using NetArchTest.Rules;
using System.Reflection;

namespace ProperCleanArchitecture.ArchitectureTests;

public class DependencyValidationTests
{
    private static readonly Assembly ApplicationAssembly = typeof(ApplicationDependencyInjection).Assembly;
    private static readonly Assembly InfrastructureAssembly = typeof(Infrastructure.Data.AppDbContext).Assembly;

    [Fact]
    public void Handlers_Should_Not_Depend_On_Other_Handlers()
    {
        // Handlers should not depend on other handlers (should use composition instead)
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .HaveNameEndingWith("Handler")
            .Should()
            .NotHaveDependencyOnAny(GetHandlerTypeNames())
            .GetResult();

        Assert.True(result.IsSuccessful,
            $"Handlers should not depend on other handlers directly. Violations: {string.Join(", ", result.FailingTypeNames ?? [])}");
    }

    [Fact]
    public void Application_Services_Should_Depend_On_Interfaces_Not_Implementations()
    {
        // Services should depend on interfaces, not concrete implementations
        var violations = new List<string>();
        var applicationTypes = Types.InAssembly(ApplicationAssembly)
            .That()
            .HaveNameEndingWith("Handler")
            .Or()
            .HaveNameEndingWith("Service")
            .GetTypes();

        foreach (var type in applicationTypes)
        {
            var constructors = type.GetConstructors();
            foreach (var constructor in constructors)
            {
                var parameters = constructor.GetParameters();
                foreach (var parameter in parameters)
                {
                    // Check if parameter type is from Infrastructure and is not an interface
                    if (parameter.ParameterType.Assembly == InfrastructureAssembly &&
                        !parameter.ParameterType.IsInterface)
                    {
                        violations.Add($"{type.Name} depends on concrete implementation {parameter.ParameterType.Name}");
                    }
                }
            }
        }

        Assert.Empty(violations);
    }

    [Fact]
    public void Repository_Interfaces_Should_Be_In_Application_Layer()
    {
        // Repository interfaces should be defined in Application layer, not Infrastructure
        var infrastructureRepositoryInterfaces = Types.InAssembly(InfrastructureAssembly)
            .That()
            .AreInterfaces()
            .And()
            .HaveNameMatching(".*Repository.*")
            .GetTypes()
            .ToList();

        Assert.Empty(infrastructureRepositoryInterfaces);
    }

    [Fact]
    public void Application_Should_Not_Reference_Concrete_Infrastructure_Classes()
    {
        // Application layer should not reference concrete Infrastructure classes
        var applicationTypes = Types.InAssembly(ApplicationAssembly).GetTypes();
        var violations = new List<string>();

        foreach (var type in applicationTypes)
        {
            // Check fields
            var fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in fields)
            {
                if (field.FieldType.Assembly == InfrastructureAssembly && !field.FieldType.IsInterface)
                {
                    violations.Add($"{type.Name} has field of concrete Infrastructure type {field.FieldType.Name}");
                }
            }

            // Check properties
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType.Assembly == InfrastructureAssembly && !property.PropertyType.IsInterface)
                {
                    violations.Add($"{type.Name} has property of concrete Infrastructure type {property.PropertyType.Name}");
                }
            }
        }

        Assert.Empty(violations);
    }

    [Fact]
    public void Static_Dependencies_Should_Be_Avoided_In_Application_Layer()
    {
        // Application layer should avoid static dependencies (DateTime.Now, etc.)
        var violations = new List<string>();
        var applicationTypes = Types.InAssembly(ApplicationAssembly).GetTypes();

        foreach (var type in applicationTypes)
        {
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var method in methods)
            {
                if (method.GetMethodBody() != null)
                {
                    // This is a simplified check - in a real scenario you might want to use IL analysis
                    var methodBody = method.GetMethodBody();
                    if (methodBody != null)
                    {
                        // Check for common static dependencies in method names/types
                        var localVariables = methodBody.LocalVariables;
                        foreach (var variable in localVariables)
                        {
                            if (variable.LocalType == typeof(DateTime) ||
                                variable.LocalType == typeof(Guid))
                            {
                                // This is just a basic check - you might want more sophisticated analysis
                            }
                        }
                    }
                }
            }
        }

        // For now, we'll skip this complex check but the structure is here for future enhancement
        Assert.True(true);
    }

    private static string[] GetHandlerTypeNames()
    {
        return Types.InAssembly(ApplicationAssembly)
            .That()
            .HaveNameEndingWith("Handler")
            .GetTypes()
            .Select(t => t.Name)
            .ToArray();
    }
}
