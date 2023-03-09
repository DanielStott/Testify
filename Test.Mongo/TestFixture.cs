using Core;
using Mongo;

namespace Test.Mongo;

[SetUpFixture]
public class TestFixture
{
    public static HttpClient Client { get; private set; }

    [OneTimeSetUp]
    public void Setup()
    {
        var app = TestApplication<Program>
            .Create();

        app
            .AddInMemoryMongo();

        Client = app.Start();
    }
}