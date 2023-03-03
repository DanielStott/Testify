using Core;
using Microsoft.AspNetCore.Hosting;

namespace Mongo;

public static class MongoExtensions
{
    public static (IWebHostBuilder Builder, Mongo) AddInMemoryMongo<T>(this TestApplication<T> testApplication) where T : class
        => (testApplication.Builder, testApplication.AddMongo());

    private static Mongo AddMongo<T>(this TestApplication<T> testApplication) where T : class
    {
        var mongo = new Mongo();
        testApplication.Dbs.Add(mongo);
        return mongo;
    }
}