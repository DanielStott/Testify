using System.ComponentModel.DataAnnotations;

namespace SqlLiteWebApi;

public class WeatherForecast
{
    [Key]
    public Guid Id { get; set; }
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }

    public WeatherForecast(DateTime date, int temperatureC, string? summary)
    {
        Id = Guid.NewGuid();
        Date = date;
        TemperatureC = temperatureC;
        Summary = summary;
    }
}