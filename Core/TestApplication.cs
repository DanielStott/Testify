using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace EasyTest;

public abstract class TestApplication : TestApplication<Program> {}
public class TestApplication<T> : WebApplicationFactory<T> where T : class
{
    public IWebHostBuilder Builder { get; }
    public List<ITestDb> Dbs { get; set; } = new();
    private HttpClient? Client;
    protected TestApplication()
        => Builder = CreateWebHostBuilder() ?? new WebHostBuilder();

    public static TestApplication<T> Create() => new ();
    
    public static TestApplication<T> Create(Action<IWebHostBuilder> configure)
    {
        var testApplication = new TestApplication<T>();
        testApplication.WithWebHostBuilder(configure); 
        return testApplication;
    }

    public HttpClient Start()
        => Client ??= CreateClient();

    public TResponse? GetService<TResponse>() where TResponse : class
    {
        var scope = Services
            .CreateScope();

        return scope
            .ServiceProvider
            .GetService<TResponse>();
    }
}