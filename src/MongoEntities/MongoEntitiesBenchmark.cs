using System;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using mongobenchmark.services;
using MongoDB.Entities;

namespace mongobenchmark.MongoEntities;

public class MongoEntitiesBenchmark: MongoBenchmarkBase
{
    [GlobalSetup]
    public async Task Setup()
    {
        // Setup connection to MongoDB
        await DB.InitAsync("benchdb", "localhost");

        await SetupBase();
    }

    [Benchmark()]
    public async Task GetByObjectId()
    {
        await DB.Find<User>().OneAsync(_existingId.ToString());
    }
}