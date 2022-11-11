using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace EasyTest.Tests;

[SetUpFixture]
public class Tests 
{
    [OneTimeSetUp]
    public async Task Setup()
    {
        var app = TestApplication
            .Create();

        app
            .AddInMemorySqlLite()
            .AddContext<DbContext>();
        app.AddInMemoryMongo();

        var client = app.Start();
    }
    
}