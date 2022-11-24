using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIExample.Repository;

namespace WebAPIExample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly WeatherForecastContext _weatherForecastContext;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(WeatherForecastContext weatherForecastContext, ILogger<WeatherForecastController> logger)
    {
        _weatherForecastContext = weatherForecastContext;
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast>? Get()
    {
        return _weatherForecastContext.WeatherForecasts;
    }
}