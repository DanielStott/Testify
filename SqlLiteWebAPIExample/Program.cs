using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SqlLiteWebApiExample.Repository;

namespace SqlLiteWebApiExample;

public class Program {
    private static SqliteConnection? _connection;

    public static async Task Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open(); 
        builder.Services.AddDbContext<WeatherForecastContext>(options => options.UseSqlite(_connection));
        builder.Services.AddControllers();

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        await SetupDatabase(app);
        await app.RunAsync();
    }

    private async static Task SetupDatabase(IHost app)
    {
        using var serviceScope = app.Services.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<WeatherForecastContext>();
        await context!.Database.EnsureCreatedAsync();
    }
}
