namespace Mongo;

public record MongoConnection(string Connection) : IMongoConnection;