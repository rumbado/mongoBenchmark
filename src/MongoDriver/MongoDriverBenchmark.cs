using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using mongobenchmark.services;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace mongobenchmark.MongoDriver;

public class MongoDriverBenchmark: MongoBenchmarkBase
{
    private IMongoDatabase? _database;

    [GlobalSetup]
    public async Task Setup()
    {
        // Setup connection to MongoDB
        var client = new MongoClient("mongodb://localhost:27017");
        _database = client.GetDatabase("benchdb");

        await SetupBase();
    }

    [Benchmark(Baseline = true)]
    public async Task GetByObjectId()
    {
        var objectId = ObjectId.Parse(_existingId);
        var filter = Builders<User>.Filter.Eq(u => u.Id, objectId);

        await _database.GetCollection<User>("users")
            .Find(filter).SingleOrDefaultAsync();
    }
}