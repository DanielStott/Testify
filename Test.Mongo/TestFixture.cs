using Core;
using Mongo;

namespace Test.Mongo;

[SetUpFixture]
public class TestFixture
{
    private TestApplication<Program>? _app;

    [OneTimeSetUp]
    public void Setup()
    {
        _app = TestApplication<Program>.Create((_, application) => application.AddInMemoryMongo());

        _app.Start();
    }

    [OneTimeTearDown]
    public void TearDown() => _app?.Dispose();
}