using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using mongobenchmark.entities;
using mongobenchmark.services;
using System.Threading;
using DnsClient.Protocol;
using BenchmarkDotNet.Running;

class Program
{
    static async Task Main(string[] args)
    {
        // Set up the console application
        Console.WriteLine("MongoDB Benchmark Tool");
        Console.WriteLine("======================");
        Console.WriteLine("Available actions:");
        Console.WriteLine("1. GenerateData numOfUsers. E.g.: dotnet run GenerateData 1000");
        Console.WriteLine("2. BenchmarkByObjectId. E.g.: dotnet run BenchmarkByObjectId -c Release");
        Console.WriteLine();

        // Configure DI container
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IMongoClient>(sp => new MongoClient("mongodb://localhost:27017"))
            .AddSingleton<DummyDataGenerator>()
            .AddSingleton<MongoBench>()
            .BuildServiceProvider();



        if (args.Length == 0)
        {
            Console.WriteLine("No input provided.");
            return;
        }

        var action = args[0].ToLowerInvariant();

        switch (action)
        {
            case "generatedata":
                // Get number of users from input
                var numOfUsers = int.Parse(args[1]);

                // Resolve dependencies
                var dataGenerator = serviceProvider.GetRequiredService<DummyDataGenerator>();

                // Generate data
                await dataGenerator.GenerateDummyUsers(numOfUsers, CancellationToken.None);

                Console.WriteLine($"Items created: {numOfUsers}");
                break;

            case "benchmarkbyobjectid":
                // MongoDB.Entities
                await MongoDB.Entities.DB.InitAsync("benchdb", "localhost");

                // Run benchmark
                BenchmarkRunner.Run<MongoBench>();

                break;

            default:
                Console.WriteLine("Invalid action.");
                break;
        }

    }
}

