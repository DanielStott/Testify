using Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace SqlLite;

public static class SqlLiteExtensions
{
    public static (TestApplication<T> Application, SqlLite) AddInMemorySqlLite<T>(this TestApplication<T> testApplication) where T : class
        => (testApplication, testApplication.AddSqlLite());

    public static void AddContext<TContext, T>(this (TestApplication<T> Application, SqlLite sqlLite) builders) where TContext : DbContext where T : class
    {
        builders.Application.Configuration += (builder, _) =>
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll<TContext>();
                services.AddDbContext<TContext>(options => options.UseSqlite(builders.sqlLite.Connection));
            });
        };
    }

    private static SqlLite AddSqlLite<T>(this TestApplication<T> testApplication) where T : class
    {
        var sqlLite = new SqlLite();
        testApplication.Dbs.Add(sqlLite);
        return sqlLite;
    }
}