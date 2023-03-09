using Core;
using Mongo;

namespace Test.Mongo;

[SetUpFixture]
public class TestFixture
{
    [OneTimeSetUp]
    public void Setup()
    {
        var app = TestApplication<Program>
            .Create();

        app.AddInMemoryMongo();

        app.Start();
    }
}