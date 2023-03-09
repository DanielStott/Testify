using Microsoft.AspNetCore.Mvc;
using MongoWebApiExample.Data;

namespace MongoWebApiExample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly WeatherForecastRepository _weatherForecastRepository;

    public WeatherForecastController(WeatherForecastRepository weatherForecastRepository)
    {
        _weatherForecastRepository = weatherForecastRepository;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        return await _weatherForecastRepository.GetAll();
    }
}