using Core;
using SqlLite;
using SqlLiteWebApiExample.Repository;

namespace Test.SqlLite;

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