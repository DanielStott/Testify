using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Mongo2Go;
using MongoDB.Driver;

namespace EasyTest.DBs;

public class Mongo : ITestDb, IDisposable
{
    public string ConnectionString => Runner.ConnectionString; 
    public string DatabaseName { get; } 
    public MongoDbRunner Runner { get; }

    public Mongo(string name)
    {
        DatabaseName = name;
        Runner = MongoDbRunner.Start(logger: NullLogger.Instance);
    }
    
    public void Dispose()
        => Runner.Dispose();
}