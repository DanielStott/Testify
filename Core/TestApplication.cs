using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Core;

public abstract class TestApplication : TestApplication<Program> {}
public class TestApplication<T> : WebApplicationFactory<T>, IDisposable where T : class
{
    public IWebHostBuilder Builder { get; }
    public List<ITestDb> Dbs { get; } = new();
    private HttpClient? Client;
    protected TestApplication()
        => Builder = CreateWebHostBuilder() ?? new WebHostBuilder();

    public static TestApplication<T>? Instance { get; private set; }

    public static TestApplication<T> Create()
    {
        Instance = new TestApplication<T>();
        return Instance;
    }

    public static TestApplication<T> Create(Action<IWebHostBuilder> configure)
    {
        Instance = new TestApplication<T>();
        Instance.WithWebHostBuilder(configure);
        return Instance;
    }

    public HttpClient Start()
        => Client ??= CreateClient();

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
        base.Dispose();
    }
}