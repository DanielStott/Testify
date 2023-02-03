using System.Reflection;
using Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SqlLite;

public static class SqlLiteExtensions
{
    public static (IWebHostBuilder Builder, SqlLite) AddInMemorySqlLite<T>(this TestApplication<T> testApplication) where T : class
        => (testApplication.Builder, testApplication.AddSqlLite());
    
    public static (IWebHostBuilder, SqlLite) AddContext<T>(this (IWebHostBuilder, SqlLite) builders) where T : DbContext
    {
        builders.Item1.ConfigureServices(services =>
        {
            services.RemoveAll<T>();
            services.AddDbContext<T>(options => options.UseSqlite(builders.Item2.Connection));
        });
        return builders;
    }
    
    // public static (IWebHostBuilder, SqlLite) ScanForDbContexts(this (IWebHostBuilder, SqlLite) builders, Assembly[] assemblies)
    // {
    //     builders.Item1.ConfigureServices(services =>
    //     {
    //         foreach (var assembly in assemblies)
    //         {
    //             assembly.GetTypes()
    //                 .Where(t => t.IsSubclassOf(typeof(DbContext)))
    //                 .ToList()
    //                 .ForEach(t =>
    //                 {
    //                     var test = new DbContextOptionsBuilder();
    //                     services.RemoveAll(t);
    //                     // services.AddDbContext<>(options => options.UseSqlite(builders.Item2.Connection));
    //                 });
    //         }
    //     });
    //     return builders;
    // } 
    
    private static SqlLite AddSqlLite<T>(this TestApplication<T> testApplication) where T : class
    {
        var sqlLite = new SqlLite();
        testApplication.Dbs.Add(sqlLite);
        return sqlLite;
    }
}