using System.ComponentModel.DataAnnotations;

namespace WebAPIExample;

public class WeatherForecast
{
    [Key]
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }

    public WeatherForecast(DateOnly date, int temperatureC, string? summary)
    {
        Id = Guid.NewGuid();
        Date = date;
        TemperatureC = temperatureC;
        Summary = summary;
    }
}