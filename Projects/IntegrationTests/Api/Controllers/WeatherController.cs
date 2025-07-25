using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class WeatherForecast
{
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public string Summary { get; set; } = "";
}

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private static readonly string[] Summaries = [
        "Freezing", 
        "Bracing", 
        "Chilly", 
        "Cool", 
        "Mild", 
        "Warm", 
        "Balmy", 
        "Hot", 
        "Sweltering", 
        "Scorching"
    ];

    [HttpGet]
    public IActionResult Get()
    {
        var rng = new Random();
        var weather = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = rng.Next(-20, 55),
            Summary = Summaries[rng.Next(Summaries.Length)]
        }).ToArray();
        return Ok(weather);
    }
}
