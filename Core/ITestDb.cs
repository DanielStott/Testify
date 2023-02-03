namespace Core;

public interface ITestDb
{
    public string ConnectionString { get; }
    public string DatabaseName { get; }
}