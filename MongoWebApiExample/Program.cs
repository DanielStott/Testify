using Mongo2Go;
using MongoWebApiExample;
using MongoWebApiExample.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<MongoDbRunner>(_ => MongoDbRunner.Start());
builder.Services.AddTransient<WeatherForecastRepository>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var serviceScope = app.Services.CreateScope();
var repo = serviceScope.ServiceProvider.GetService<WeatherForecastRepository>();
await repo?.AddRange(new[]
{
    new WeatherForecast(new DateOnly(2022, 1, 1), 15, "Summary 1"),
    new WeatherForecast(new DateOnly(2022, 1, 2), 20, "Summary 2"),
    new WeatherForecast(new DateOnly(2022, 1, 3), 25, "Summary 3")
})!;

await app.RunAsync();

// Make the implicit Program class public so test projects can access it
public partial class Program { }
