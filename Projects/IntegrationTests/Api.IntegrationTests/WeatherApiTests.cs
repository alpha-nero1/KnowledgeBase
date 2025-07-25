using System.Net;
using System.Net.Http.Json;
using Api.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Api.IntegrationTests
{
    public class WeatherApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public WeatherApiTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetWeather_ReturnsWeatherForecasts()
        {
            var response = await _client.GetAsync("api/weather");
            response.EnsureSuccessStatusCode();
            var forecasts = await response.Content.ReadFromJsonAsync<WeatherForecast[]>();
            Assert.NotNull(forecasts);
            Assert.True(forecasts.Length > 0);
        }
    }
}
