using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIExample.Repository;

namespace WebAPIExample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly WeatherForecastContext _weatherForecastContext;

    public WeatherForecastController(WeatherForecastContext weatherForecastContext)
    {
        _weatherForecastContext = weatherForecastContext;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<List<WeatherForecast>> Get()
    {
        return await _weatherForecastContext.WeatherForecasts!.ToListAsync();
    }
}