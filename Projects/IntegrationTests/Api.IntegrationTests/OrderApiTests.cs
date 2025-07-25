using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Api.IntegrationTests;

public class OrderApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    public OrderApiTests(WebApplicationFactory<Program> factory) => _client = factory.CreateClient();

    [Fact]
    public async Task CreateOrder_ReturnsOrderId()
    {
        var order = new { ProductName = "Widget", Quantity = 2 };
        var response = await _client.PostAsJsonAsync("/api/order", order);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<OrderResponse>();
        Assert.NotNull(result);
        Assert.True(result.orderId > 0);
    }

    private class OrderResponse
    {
        public int orderId { get; set; }
    }
}
