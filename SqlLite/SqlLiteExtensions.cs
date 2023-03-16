using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace SqlLite;

public static class SqlLiteExtensions
{
    public static (ITestApplication Application, SqlLite) AddInMemorySqlLite(this ITestApplication testApplication)
        => (testApplication, testApplication.AddSqlLite());

    public static (ITestApplication Application, SqlLite sqlLite) AddContext<TContext>(this (ITestApplication Application, SqlLite sqlLite) builders) where TContext : DbContext
    {
        builders.Application.Configuration += (builder, _) =>
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll<TContext>();
                services.AddDbContext<TContext>(options => options.UseSqlite(builders.sqlLite.Connection));
            });
        };
        return builders;
    }

    private static SqlLite AddSqlLite(this ITestApplication testApplication)
    {
        var sqlLite = new SqlLite();
        testApplication.Dbs.Add(sqlLite);
        return sqlLite;
    }
}