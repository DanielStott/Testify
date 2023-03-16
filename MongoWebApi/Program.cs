using Microsoft.Extensions.Logging.Abstractions;
using Mongo2Go;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoWebApi;
using MongoWebApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<MongoDbRunner>(_ => MongoDbRunner.Start(logger: NullLogger.Instance));
builder.Services.AddTransient<WeatherForecastRepository>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Uncomment this to run locally
// await SetupMongo(app);

await app.RunAsync();

async Task SetupMongo(IHost webApplication)
{
    using var serviceScope = webApplication.Services.CreateScope();
    var repo = serviceScope.ServiceProvider.GetService<WeatherForecastRepository>();
    if (repo is null)
        throw new InvalidOperationException("Could not get WeatherForecastRepository");

    await repo.CreateCollection();
    await repo.AddRange(new[]
    {
        new WeatherForecast(new DateTime(2022, 1, 1), 15, "Summary 1"),
        new WeatherForecast(new DateTime(2022, 1, 2), 20, "Summary 2"),
        new WeatherForecast(new DateTime(2022, 1, 3), 25, "Summary 3")
    });
}

// Make the implicit Program class public so test projects can access it
public partial class Program { }

