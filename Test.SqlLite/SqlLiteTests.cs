using System.Net;
using Core;
using SqlLiteWebApi;

namespace Test.SqlLite;

public class SqlLiteTests : BaseTest<Program>
{
    [Test]
    public async Task get_dynamic_weather_forecasts()
    {
        var (response, content) = await Api.Get("/WeatherForecast");
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(content, Has.Count.EqualTo(3));
        });
    }

    [Test]
    public async Task get_weather_forecasts()
    {
        var (response, content) = await Api.Get<List<WeatherForecast>>("/WeatherForecast");
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(content, Has.Count.EqualTo(3));
        });
    }
}