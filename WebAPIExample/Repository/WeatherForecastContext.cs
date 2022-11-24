using Microsoft.EntityFrameworkCore;

namespace WebAPIExample.Repository;

public sealed class WeatherForecastContext : DbContext
{
    public WeatherForecastContext(DbContextOptions options)
        :base(options)
    {
        if (Database.EnsureCreated())
        {
            WeatherForecasts?.Add(new WeatherForecast(new DateOnly(2000,1,1), 1, "Rain"));
        }
    }

    public DbSet<WeatherForecast>? WeatherForecasts { get; init; }
}