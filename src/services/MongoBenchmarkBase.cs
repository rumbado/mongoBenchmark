using System;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using mongobenchmark.MongoDriver;
using MongoDB.Driver;

namespace mongobenchmark.services;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.Method, MethodOrderPolicy.Declared)]
public abstract class MongoBenchmarkBase
{
    protected string _existingId = string.Empty;
    protected string _existingName = string.Empty;
 
    protected virtual async Task SetupBase()
    {
        // Setup connection to MongoDB
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("benchdb");
        
        // Get a random user from the database
        var users = await database.GetCollection<User>("users").Find(_ => true).ToListAsync();
        var randomUser = users[new Random().Next(users.Count)];
        _existingId = randomUser.Id.ToString() ?? string.Empty;
        _existingName = randomUser.Name ?? string.Empty;
    }
}