using MongoDB.Driver;
using System;
using Bogus;
using System.Threading;
using System.Threading.Tasks;
using mongobenchmark.MongoDriver;

namespace mongobenchmark.services;
public class DummyDataGenerator
{
    private readonly IMongoDatabase _mongoDatabase;

    public DummyDataGenerator()
    {
        // Setup connection to MongoDB
        var client = new MongoClient("mongodb://localhost:27017");
        _mongoDatabase = client.GetDatabase("benchdb");
    }

    public async Task GenerateDummyUsers(int count, CancellationToken cancellationToken)
    {
        var collection = _mongoDatabase.GetCollection<User>("users");

        for (int i = 0; i < count; i++)
        {
            await collection.InsertOneAsync(CreateRandomUser());
            Console.WriteLine($"Inserted user {i + 1} of {count}");

            if (cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Operation cancelled.");
                break;
            }
        }
    }

    private static User CreateRandomUser()
    {
        // User Bogus to create a random user
        var faker = new Faker<User>();
        faker.RuleFor(u => u.Name, f => f.Name.FullName());
        faker.RuleFor(u => u.Email, f => f.Internet.Email());
        faker.RuleFor(u => u.Address, f => f.Address.FullAddress());
        faker.RuleFor(u => u.Phone, f => f.Phone.PhoneNumber());
        faker.RuleFor(u => u.BirthDate, f => f.Date.Past(30));

        return faker.Generate();
    }
}