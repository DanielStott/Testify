using Mongo2Go;
using MongoDB.Driver;

namespace MongoWebApiExample.Data;

public class WeatherForecastRepository
{
    private readonly MongoClient _client;

    public WeatherForecastRepository(MongoDbRunner runner) => _client = new MongoClient(runner.ConnectionString);

    public async Task CreateCollection()
    {
        var database = _client.GetDatabase("WeatherForecastDb");
        await database.CreateCollectionAsync("WeatherForecasts");
    }

    public async Task<List<WeatherForecast>> GetAll() =>
        (await _client
            .GetDatabase("WeatherForecastDb")
            .GetCollection<WeatherForecast>("WeatherForecasts")
            .FindAsync(FilterDefinition<WeatherForecast>.Empty))
        .ToList();

    public async Task AddRange(IEnumerable<WeatherForecast> weatherForecasts) =>
        await _client
            .GetDatabase("WeatherForecastDb")
            .GetCollection<WeatherForecast>("WeatherForecasts")
            .InsertManyAsync(weatherForecasts);
}