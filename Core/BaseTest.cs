using NUnit.Framework;

namespace Core;

public class BaseTest<T> where T : class
{
    protected HttpClient Api { get; private set; }

    [SetUp]
    public virtual Task Setup()
    {
        Api = TestApplication<T>.Instance?.GetClient() ??
              throw new InvalidOperationException("Create a new instance of TestApplication<T> before calling using BaseTest<T>");
        return Task.CompletedTask;
    }

    [TearDown]
    public virtual void TearDown() => Api.Dispose();
}