using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using static EasyTest.TestApplication;

namespace EasyTest;


[SetUpFixture]
public class TestApi
{
    [OneTimeSetUp]
    public async Task Setup()
    {
        var app = TestApplication
                .Create();
        
        app
            .AddInMemorySqlLite()
                .AddContext<DbContext>()
                .AddContext<DbContext>()
                .AddContext<DbContext>();
        app.AddInMemoryMongo();

        var client = app.Start();
    }
    
}

