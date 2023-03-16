using Core;
using SqlLite;
using SqlLiteWebApi.Data;

namespace Test.SqlLite;

[SetUpFixture]
public class TestFixture
{
    private TestApplication<Program>? _app;

    [OneTimeSetUp]
    public void Setup()
    {
        _app = TestApplication<Program>
            .Create((builder, app) =>
            {
                app.AddInMemorySqlLite()
                    .AddContext<WeatherForecastContext, Program>();
            });

        _app.Start();
    }

    [OneTimeTearDown]
    public void TearDown() => _app?.Dispose();
}