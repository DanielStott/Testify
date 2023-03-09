using Microsoft.EntityFrameworkCore;

namespace SqlLiteWebApiExample.Data;

public class WeatherForecastContext : DbContext
{
    public WeatherForecastContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<WeatherForecast> WeatherForecasts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WeatherForecast>().HasData(
            new WeatherForecast(new DateOnly(2022, 1, 1), 15, "Summary 1"),
            new WeatherForecast(new DateOnly(2022, 1, 2), 20, "Summary 2"),
            new WeatherForecast(new DateOnly(2022, 1, 3), 25, "Summary 3")
        );
    }
}