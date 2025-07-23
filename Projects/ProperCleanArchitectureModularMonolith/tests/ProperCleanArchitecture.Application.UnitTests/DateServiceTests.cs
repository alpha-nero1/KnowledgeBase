using System;
using ProperCleanArchitecture.Application.Shared.Services;
using Xunit;

namespace ProperCleanArchictecture.UnitTests;

public class DateServiceTests
{
    [Fact]
    public void UtcNow_ShouldBeCloseToSystemUtcNow()
    {
        // Arrange
        var service = new DateService();
        var before = DateTime.UtcNow;
        // Act
        var result = service.UtcNow;
        var after = DateTime.UtcNow;
        // Assert
        Assert.True(result >= before && result <= after);
    }
}
