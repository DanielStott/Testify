﻿using System.Net.Http.Json;
using SqlLiteWebApiExample;

namespace Tests;

public class SqlLiteTests
{
    [Test]
    public async Task can_get_weather_forecasts()
    {
        var responseMessage = await TestFixture.Client.GetAsync("/WeatherForecast");
        var weatherForecasts = await responseMessage.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>();
        Assert.That(weatherForecasts.Count(), Is.EqualTo(3));
    }
}