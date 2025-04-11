using System;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using mongobenchmark.services;
using MongoDB.Entities;

namespace mongobenchmark.MongoEntities;

public class MongoEntitiesBenchmark: MongoBenchmarkBase
{
    [GlobalSetup]
    public void Setup()
    {
        // Setup connection to MongoDB
        DB.InitAsync("benchdb", "localhost").GetAwaiter().GetResult();

        SetupBase().GetAwaiter().GetResult();
    }

    [Benchmark()]
    public async Task GetByObjectId()
    {
        await DB.Find<User>().OneAsync(_existingId.ToString());
    }

    [Benchmark()]
    public async Task GetByText()
    {
        var user = await DB.Find<User>().Match(x => x.Eq(u => u.Name, _existingName)).ExecuteFirstAsync();

        if (user == null || user.Name != _existingName)
        {
            throw new Exception("User not found");
        }
    }
}