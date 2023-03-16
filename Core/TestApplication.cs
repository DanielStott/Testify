using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public interface ITestApplication : IDisposable
{
    HttpClient GetClient();
}

public sealed class TestApplication<T> : WebApplicationFactory<T>, ITestApplication where T : class
{

    public IWebHostBuilder Builder { get; }
    public List<ITestDb> Dbs { get; } = new();
    private HttpClient? _client;

    private TestApplication()
        => Builder = CreateWebHostBuilder() ?? new WebHostBuilder();

    public static TestApplication<T> Create()
    {
        var instance = new TestApplication<T>();
        Test.Instance = instance;
        return instance;
    }

    public static TestApplication<T> Create(Action<IWebHostBuilder> configure)
    {
        var instance = new TestApplication<T>();
        Test.Instance = instance;
        instance.WithWebHostBuilder(configure);
        return instance;
    }

    public HttpClient Start()
        => _client ??= CreateClient();

    public HttpClient GetClient()
    {
        return CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost"),
        });
    }

    public TResponse? GetService<TResponse>() where TResponse : class
    {
        var scope = Services
            .CreateScope();

        return scope
            .ServiceProvider
            .GetService<TResponse>();
    }

    protected override void Dispose(bool disposing)
    {
        foreach (var db in Dbs)
            db.Dispose();
        base.Dispose(disposing);
    }
}