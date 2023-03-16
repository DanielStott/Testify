using Core;

namespace Mongo;

public static class MongoExtensions
{
    public static (ITestApplication Application, Mongo) AddInMemoryMongo(this ITestApplication testApplication)
        => (testApplication, testApplication.AddMongo());

    private static Mongo AddMongo(this ITestApplication testApplication)
    {
        var mongo = new Mongo();
        testApplication.Dbs.Add(mongo);
        return mongo;
    }
}