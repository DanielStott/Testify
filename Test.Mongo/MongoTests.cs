using System.Net.Http.Json;
using MongoWebApiExample;

namespace Test.Mongo;

public class MongoTests
{
    [Test]
    public async Task get_weather_forecasts()
    {
        var responseMessage = await TestFixture.Client.GetAsync("/WeatherForecast");
        var weatherForecasts = await responseMessage.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>();
        Assert.That(weatherForecasts?.Count(), Is.EqualTo(3));
    }
}