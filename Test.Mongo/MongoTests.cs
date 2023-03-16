using System.Net.Http.Json;
using Core;
using MongoWebApi;

namespace Test.Mongo;

public class MongoTests : BaseTest
{
    [Test]
    public async Task get_weather_forecasts()
    {
        var responseMessage = await Api.GetAsync("/WeatherForecast");
        var weatherForecasts = await responseMessage.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>();
        Assert.That(weatherForecasts?.Count(), Is.EqualTo(3));
    }
}