using Core;
using SqlLite;
using SqlLiteWebApi.Data;

namespace Test.SqlLite;

[SetUpFixture]
public class TestFixture
{
    [OneTimeSetUp]
    public void Setup()
    {
        var app = TestApplication<Program>
            .Create();

        app
            .AddInMemorySqlLite()
                .AddContext<WeatherForecastContext>();

        app.Start();
    }
}