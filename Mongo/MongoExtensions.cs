using Core;
using Microsoft.Extensions.DependencyInjection;

namespace Mongo;

public static class MongoExtensions
{
    public static (ITestApplication Application, Mongo) AddInMemoryMongo(this ITestApplication testApplication)
    {
        var mongo = testApplication.AddMongo();

        testApplication.Configuration += (builder, _) =>
        {
            builder.ConfigureServices((_, services) =>
            {
                services.AddScoped<IMongoConnection>(_ => new MongoConnection(mongo.ConnectionString));
            });
        };
        return (testApplication, mongo);
    }

    private static Mongo AddMongo(this ITestApplication testApplication)
    {
        var mongo = new Mongo();
        testApplication.Dbs.Add(mongo);
        return mongo;
    }
}