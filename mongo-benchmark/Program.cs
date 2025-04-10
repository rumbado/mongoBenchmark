using MongoDB.Driver;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("benchdb");
        var collection = database.GetCollection<TestDoc>("test");

        await collection.InsertOneAsync(new TestDoc { Name = "Hello", CreatedAt = DateTime.UtcNow });

        var count = await collection.CountDocumentsAsync(FilterDefinition<TestDoc>.Empty);
        Console.WriteLine($"Documents in collection: {count}");
    }
}

class TestDoc
{
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
}
