using Microsoft.Extensions.Logging.Abstractions;
using Mongo2Go;
using MongoWebApi;
using MongoWebApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Uncomment this to run locally
// builder.Services.AddSingleton<MongoDbRunner>(_ => MongoDbRunner.Start(logger: NullLogger.Instance));
builder.Services.AddTransient<WeatherForecastRepository>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();

// Make the implicit Program class public so test projects can access it
public partial class Program { }

