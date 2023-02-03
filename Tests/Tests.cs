using System.Net.Http.Json;
using Core;
using SqlLite;
using WebAPIExample;
using WebAPIExample.Repository;

namespace Tests;

[SetUpFixture]
public class Tests 
{
    private HttpClient? _client;

    [OneTimeSetUp]
    public void Setup()
    {
        var app = TestApplication<Program>
            .Create();

        app
            .AddInMemorySqlLite()
                .AddContext<WeatherForecastContext>();

        _client = app.Start();
    }

    [Test]
    public async Task can_get_weather_forecasts()
    {
        var responseMessage = await _client.GetAsync("/WeatherForecast");
        var weatherForecasts = await responseMessage.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>();
        Assert.That(weatherForecasts.Count(), Is.EqualTo(1));
    }
}