using System.Net.Http.Json;
using SqlLiteWebApiExample;

namespace Test.SqlLite;

public class SqlLiteTests
{
    [Test]
    public async Task get_weather_forecasts()
    {
        var responseMessage = await TestFixture.Client.GetAsync("/WeatherForecast");
        var weatherForecasts = await responseMessage.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>();
        Assert.That(weatherForecasts.Count(), Is.EqualTo(3));
    }
}