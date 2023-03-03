using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SqlLiteWebApiExample.Repository;

var builder = WebApplication.CreateBuilder(args);

var connection = new SqliteConnection("DataSource=:memory:");
connection.Open();
builder.Services.AddDbContext<WeatherForecastContext>(options => options.UseSqlite(connection));
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var serviceScope = app.Services.CreateScope();
var context = serviceScope.ServiceProvider.GetService<WeatherForecastContext>();
await context!.Database.EnsureCreatedAsync();

await app.RunAsync();

// Make the implicit Program class public so test projects can access it
public partial class Program { }
