using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using mongobenchmark.entities;
using Bogus;
using System.Threading;
using System.Threading.Tasks;

namespace mongobenchmark.services;
public class DummyDataGenerator(IMongoClient mongoClient)
{
    private readonly IMongoDatabase _mongoDatabase = mongoClient.GetDatabase("benchdb");

    public async Task GenerateDummyUsers(int count, CancellationToken cancellationToken)
    {
        var collection = _mongoDatabase.GetCollection<User>("users");

        for (int i = 0; i < count; i++)
        {
            await collection.InsertOneAsync(CreateRandomUser());

            if (cancellationToken.IsCancellationRequested)
            {
                break;
            }
        }
    }

    private User CreateRandomUser()
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