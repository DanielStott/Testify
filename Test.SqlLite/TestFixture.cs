using Core;
using SqlLite;
using SqlLiteWebApi.Data;

namespace Test.SqlLite;

[SetUpFixture]
public class TestFixture
{
    private TestApplication<Program> _app;

    [OneTimeSetUp]
    public void Setup()
    {
        _app = TestApplication<Program>
            .Create();

        _app
            .AddInMemorySqlLite()
                .AddContext<WeatherForecastContext>();

        _app.Start();
    }

    [OneTimeTearDown]
    public void TearDown() => _app?.Dispose();
}