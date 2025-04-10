using System;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using mongobenchmark.entities;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoFramework;
using MongoDB.Driver.Linq;
using MongoDB.Entities;

namespace mongobenchmark.services;

[MemoryDiagnoser]
public class MongoBench()
{
    private IMongoDatabase? _database;
    private string _existingId;
    private MyDbContext? _dbContext;

    [GlobalSetup]
    public async Task Setup()
    {
        // MongoDB.Driver
        var client = new MongoClient("mongodb://localhost:27017");
        _database = client.GetDatabase("benchdb");

        // MongoFramework
        var connection = MongoFramework.MongoDbConnection.FromConnectionString("mongodb://localhost:27017/benchdb");
        _dbContext = new MyDbContext(connection);


        // Get a random user from the database
        var users = await _database.GetCollection<User>("users").Find(_ => true).ToListAsync();
        var randomUser = users[new Random().Next(users.Count)];
        _existingId = randomUser.ID;
    }

    [Benchmark(Baseline = true)]
    public async Task MongoDriver_GetByObjectId()
    {
        var filter = Builders<User>.Filter.Eq(u => u.ID, _existingId);

        await _database.GetCollection<User>("users")
            .Find(filter).SingleOrDefaultAsync();
    }

    [Benchmark()]
    public async Task MongoDBEntities_GetByObjectId()
    {
        await DB.Find<User>().OneAsync(_existingId.ToString());
    }

    [Benchmark()]
    public async Task MongoFramework_GetByObjectId()
    {
        await _dbContext.Users.SingleOrDefaultAsync(x => x.ID == _existingId);
    }
}

public class MyDbContext : MongoFramework.MongoDbContext
{
    public MyDbContext(MongoFramework.IMongoDbConnection connection) : base(connection) { }

    public MongoDbSet<User>? Users { get; set; }
}