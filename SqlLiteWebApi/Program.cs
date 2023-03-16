using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SqlLiteWebApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Uncomment this to run locally
SetupDatabase(builder);

builder.Services.AddControllers();
var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();

void SetupDatabase(WebApplicationBuilder webApplicationBuilder)
{
    var connection = new SqliteConnection("DataSource=:memory:");
    connection.Open();
    webApplicationBuilder.Services.AddDbContext<WeatherForecastContext>(options => options.UseSqlite(connection));
}

// Make the implicit Program class public so test projects can access it
public partial class Program { }
