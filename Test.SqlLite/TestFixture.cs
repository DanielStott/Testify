using System.Net.Http.Json;
using Core;
using SqlLite;
using SqlLiteWebApiExample;
using SqlLiteWebApiExample.Repository;

namespace Tests;

[SetUpFixture]
public class TestFixture 
{
    public static HttpClient Client { get; private set; }

    [OneTimeSetUp]
    public void Setup()
    {
        var app = TestApplication<Program>
            .Create();

        app
            .AddInMemorySqlLite()
                .AddContext<WeatherForecastContext>();

        Client = app.Start();
    }
}