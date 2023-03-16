using Microsoft.AspNetCore.Mvc;
using MongoWebApi.Data;

namespace MongoWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly WeatherForecastRepository _weatherForecastRepository;

    public WeatherForecastController(WeatherForecastRepository weatherForecastRepository)
    {
        _weatherForecastRepository = weatherForecastRepository;

        _weatherForecastRepository.CreateCollection();
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get() =>
        await _weatherForecastRepository.GetAll();
}