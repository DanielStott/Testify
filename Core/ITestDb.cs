namespace Core;

public interface ITestDb : IDisposable
{
    public string ConnectionString { get; }
    public string DatabaseName { get; }
}