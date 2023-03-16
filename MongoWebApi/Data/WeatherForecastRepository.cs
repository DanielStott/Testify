using Mongo2Go;
using MongoDB.Driver;

namespace MongoWebApi.Data;

public class WeatherForecastRepository
{
    private readonly MongoClient _client;

    public WeatherForecastRepository(MongoDbRunner runner) => _client = new MongoClient(runner.ConnectionString);

    public void CreateCollection()
    {
        if(_client.GetDatabase("WeatherForecastDb").ListCollectionNames().ToList().Contains("WeatherForecasts"))
            return;

        var database = _client.GetDatabase("WeatherForecastDb");
        database.CreateCollection("WeatherForecasts");
        AddRange(new []
        {
            new WeatherForecast(new DateTime(2022, 1, 1), 15, "Summary 1"),
            new WeatherForecast(new DateTime(2022, 1, 2), 20, "Summary 2"),
            new WeatherForecast(new DateTime(2022, 1, 3), 25, "Summary 3")
        });
    }

    public async Task<List<WeatherForecast>> GetAll() =>
        (await _client
            .GetDatabase("WeatherForecastDb")
            .GetCollection<WeatherForecast>("WeatherForecasts")
            .FindAsync(FilterDefinition<WeatherForecast>.Empty))
        .ToList();

    public void AddRange(IEnumerable<WeatherForecast> weatherForecasts) =>
        _client
            .GetDatabase("WeatherForecastDb")
            .GetCollection<WeatherForecast>("WeatherForecasts")
            .InsertMany(weatherForecasts);
}