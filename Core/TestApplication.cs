using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
    private readonly Action<IHostBuilder> _configureHost;

    private TestApplication()
        => Builder = CreateWebHostBuilder() ?? new WebHostBuilder();

    private TestApplication(Action<IHostBuilder> configureHost) => _configureHost = configureHost;

    public static TestApplication<T> Create()
    {
        var instance = new TestApplication<T>();
        Test.Instance = instance;
        return instance;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        _configureHost.Invoke(builder);
        return base.CreateHost(builder);
    }

    public static TestApplication<T> Create(Action<IHostBuilder> configureHost)
    {
        var instance = new TestApplication<T>(configureHost);
        Test.Instance = instance;
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