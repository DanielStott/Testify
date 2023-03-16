using Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace SqlLite;

public static class SqlLiteExtensions
{
    public static (Action<IHostBuilder> ConfigureHost, SqlLite) AddInMemorySqlLite<T>(this TestApplication<T> testApplication) where T : class
        => (testApplication.ConfigureHost, testApplication.AddSqlLite());

    public static (Action<IHostBuilder> hostAction, SqlLite) AddContext<T>(this (Action<IHostBuilder> hostAction, SqlLite sqlLite) builders) where T : DbContext
    {
        builders.hostAction += builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll<T>();
                services.AddDbContext<T>(options => options.UseSqlite(builders.sqlLite.Connection));
            });
        };
        return builders;
    }

    private static SqlLite AddSqlLite<T>(this TestApplication<T> testApplication) where T : class
    {
        var sqlLite = new SqlLite();
        testApplication.Dbs.Add(sqlLite);
        return sqlLite;
    }
}