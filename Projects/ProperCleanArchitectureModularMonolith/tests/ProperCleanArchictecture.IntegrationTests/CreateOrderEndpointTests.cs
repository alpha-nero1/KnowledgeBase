using System.Net.Http.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using ProperCleanArchitecture.Application.Order.Commands.CreateOrderCommand;

namespace ProperCleanArchictecture.IntegrationTests;

public class CreateOrderEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public CreateOrderEndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Post_CreateOrder_ReturnsOk()
    {
        var client = _factory.CreateClient();
        var request = new CreateOrderCommand(1, new List<Guid>()); // Empty products for now
        var response = await client.PostAsJsonAsync("/api/orders", request);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
