﻿using Core;
using Microsoft.Extensions.Logging.Abstractions;
using Mongo2Go;

namespace Mongo;

public sealed class Mongo : ITestDb
{
    public string ConnectionString => Runner.ConnectionString;
    public string DatabaseName { get; }
    public MongoDbRunner Runner { get; }

    public Mongo()
    {
        DatabaseName = Guid.NewGuid().ToString();
        Runner = MongoDbRunner.Start(logger: NullLogger.Instance);
    }

    public void Dispose()
        => Runner.Dispose();
}