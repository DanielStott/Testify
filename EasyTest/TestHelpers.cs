using EasyTest.DBs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite.Infrastructure.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EasyTest;

public static class TestHelpers
{
    public static (IWebHostBuilder Builder, SqlLite) AddInMemorySqlLite<T>(this TestApplication<T> testApplication) where T : class
        => (testApplication.Builder, testApplication.AddSqlLite());
    
    public static (IWebHostBuilder Builder, Mongo) AddInMemoryMongo<T>(this TestApplication<T> testApplication) where T : class
        => (testApplication.Builder, testApplication.AddMongo());

    public static (IWebHostBuilder, SqlLite) AddContext<T>(this (IWebHostBuilder, SqlLite) builders) where T : DbContext
    {
        builders.Item1.ConfigureServices(services =>
        {
            services.RemoveAll<T>();
            services.AddDbContext<T>(options => options.UseSqlite(builders.Item2.Connection));
        });
        return builders;
    }
}