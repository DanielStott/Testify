using WebAPIExample.Repository;

namespace WebAPIExample;
public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSqlite<WeatherForecastContext>(builder.Configuration.GetConnectionString("DefaultConnection"));
        builder.Services.AddControllers();

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
