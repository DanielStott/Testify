using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SqlLite;
using SqlLiteWebApi.Data;

namespace Test.SqlLite;

[SetUpFixture]
public class TestFixture
{
    private TestApplication<Program>? _app;
    private static global::SqlLite.SqlLite _sqlDb = new ();

    [OneTimeSetUp]
    public void Setup()
    {
        _app = TestApplication<Program>
            .Create(builder => builder
                .ConfigureServices((_, services) =>
                {
                    services.RemoveAll<WeatherForecastContext>();
                    services.AddDbContext<WeatherForecastContext>(options => options.UseSqlite(_sqlDb.Connection));
                }));
        // _app
        //     .AddInMemorySqlLite()
        //         .AddContext<WeatherForecastContext>();

        _app.Start();
    }

    [OneTimeTearDown]
    public void TearDown() => _app?.Dispose();
}