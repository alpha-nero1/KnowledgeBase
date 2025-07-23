using System.Net.Http.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using ProperCleanArchitecture.Application.Order.Commands.CreateOrderCommand;
using ProperCleanArchitecture.Api;

namespace ProperCleanArchictecture.IntegrationTests;

public class CreateOrderEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public CreateOrderEndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task Post_CreateOrder_ReturnsOk()
    {
        var request = new CreateOrderCommand(1, new List<Guid>()); // Empty products for now
        var response = await _client.PostAsJsonAsync("/api/orders", request);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
