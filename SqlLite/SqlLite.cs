using Core;
using Microsoft.Data.Sqlite;

namespace SqlLite;

public sealed class SqlLite : ITestDb
{
    public string ConnectionString => Connection.ConnectionString;
    public string DatabaseName => Connection.Database;
    public SqliteConnection Connection { get; }

    public SqlLite()
    {
        Connection = new SqliteConnection("DataSource=:memory:");
        Connection.Open();
    }

    public void Dispose()
        => Connection.Dispose();
}