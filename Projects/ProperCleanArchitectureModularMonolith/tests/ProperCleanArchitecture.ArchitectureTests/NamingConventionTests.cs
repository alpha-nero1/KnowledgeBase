using NetArchTest.Rules;

namespace ProperCleanArchitecture.ArchitectureTests;

public class NamingConventionTests
{
    private static readonly System.Reflection.Assembly ApplicationAssembly = typeof(ApplicationDependencyInjection).Assembly;
    private static readonly System.Reflection.Assembly ApiAssembly = typeof(Api.Controllers.ApiControllerBase).Assembly;

    [Fact]
    public void Commands_Should_Have_Command_Suffix()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .ResideInNamespaceContaining("Commands")
            .And()
            .AreClasses()
            .And()
            .DoNotHaveNameEndingWith("Handler")
            .And()
            .DoNotHaveNameEndingWith("Validator")
            .Should()
            .HaveNameEndingWith("Command")
            .GetResult();

        Assert.True(result.IsSuccessful,
            $"All command classes should end with 'Command'. Violations: {string.Join(", ", result.FailingTypeNames ?? [])}");
    }

    [Fact]
    public void Queries_Should_Have_Query_Suffix()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .ResideInNamespaceContaining("Queries")
            .And()
            .AreClasses()
            .And()
            .DoNotHaveNameEndingWith("Handler")
            .And()
            .DoNotHaveNameEndingWith("Validator")
            .Should()
            .HaveNameEndingWith("Query")
            .GetResult();

        Assert.True(result.IsSuccessful,
            $"All query classes should end with 'Query'. Violations: {string.Join(", ", result.FailingTypeNames ?? [])}");
    }

    [Fact]
    public void Command_Handlers_Should_Have_Handler_Suffix()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .ResideInNamespaceContaining("Commands")
            .And()
            .AreClasses()
            .And()
            .DoNotHaveNameEndingWith("Command")
            .Should()
            .HaveNameEndingWith("Handler")
            .GetResult();

        Assert.True(result.IsSuccessful,
            $"All command handlers should end with 'Handler'. Violations: {string.Join(", ", result.FailingTypeNames ?? [])}");
    }

    [Fact]
    public void Query_Handlers_Should_Have_Handler_Suffix()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .ResideInNamespaceContaining("Queries")
            .And()
            .AreClasses()
            .And()
            .DoNotHaveNameEndingWith("Query")
            .Should()
            .HaveNameEndingWith("Handler")
            .GetResult();

        Assert.True(result.IsSuccessful,
            $"All query handlers should end with 'Handler'. Violations: {string.Join(", ", result.FailingTypeNames ?? [])}");
    }

    [Fact]
    public void Controllers_Should_Have_Controller_Suffix()
    {
        var result = Types.InAssembly(ApiAssembly)
            .That()
            .ResideInNamespace("ProperCleanArchitecture.Api.Controllers")
            .And()
            .AreClasses()
            .And()
            .DoNotHaveNameEndingWith("Base")
            .Should()
            .HaveNameEndingWith("Controller")
            .GetResult();

        Assert.True(result.IsSuccessful,
            $"All controllers should end with 'Controller'. Violations: {string.Join(", ", result.FailingTypeNames ?? [])}");
    }

    [Fact]
    public void DTOs_Should_Have_Dto_Suffix()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .ResideInNamespaceContaining("DTOs")
            .And()
            .AreClasses()
            .Should()
            .HaveNameEndingWith("Dto")
            .GetResult();

        Assert.True(result.IsSuccessful,
            $"All DTOs should end with 'Dto'. Violations: {string.Join(", ", result.FailingTypeNames ?? [])}");
    }

    [Fact]
    public void Validators_Should_Have_Validator_Suffix()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .ResideInNamespaceContaining("Validators")
            .And()
            .AreClasses()
            .Should()
            .HaveNameEndingWith("Validator")
            .GetResult();

        Assert.True(result.IsSuccessful,
            $"All validators should end with 'Validator'. Violations: {string.Join(", ", result.FailingTypeNames ?? [])}");
    }
}
