using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using SqlLite;

namespace EasyTest.Tests;

[SetUpFixture]
public class Tests 
{
    [OneTimeSetUp]
    public async Task Setup()
    {
        var app = TestApplication<WebAPIExample.Program>
            .Create();

        app
            .AddInMemorySqlLite()
            .AddContext<DbContext>();

        var client = app.Start();
    }
    
}