using NUnit.Framework;

namespace Core;

public class BaseTest
{
    protected HttpClient Api { get; private set; }

    [SetUp]
    public virtual Task Setup()
    {
        Api = Test.Instance?.GetClient() ??
              throw new InvalidOperationException("Create a new instance of TestApplication<T> before calling BaseTest<T>");
        return Task.CompletedTask;
    }

    [TearDown]
    public virtual void TearDown() => Api.Dispose();
}