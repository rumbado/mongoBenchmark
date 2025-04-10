using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using mongobenchmark.entities;
using mongobenchmark.services;
using System.Threading;

class Program
{
    static async Task Main()
    {

        // Configure DI container
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IMongoClient>(sp => new MongoClient("mongodb://localhost:27017"))
           .AddSingleton<DummyDataGenerator>()
            .BuildServiceProvider();

        // Resolve dependencies
        var dataGenerator = serviceProvider.GetRequiredService<DummyDataGenerator>();

        // Application logic
        await dataGenerator.GenerateDummyUsers(10, CancellationToken.None);

        Console.WriteLine($"Items created: {10}");
    }
}

