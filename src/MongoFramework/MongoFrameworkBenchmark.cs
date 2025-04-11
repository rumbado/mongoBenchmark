using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using mongobenchmark.services;  
using MongoFramework;
using MongoFramework.Linq;

namespace mongobenchmark.MongoFramework;

public class MongoFrameworkBenchmark: MongoBenchmarkBase
{
    private static MyDbContext? _dbContext;

    [GlobalSetup]
    public async Task Setup()
    {
        if (_dbContext == null)
        {
            // Setup connection to MongoDB
            var connection = MongoDbConnection.FromConnectionString("mongodb://localhost:27017/benchdb");
            _dbContext = new MyDbContext(connection);
        }

        await SetupBase();
    }

    [Benchmark()]
    public async Task GetByObjectId()
    {
        await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == _existingId);
    }
}

public class MyDbContext(IMongoDbConnection connection) : MongoDbContext(connection)
{
    public MongoDbSet<User>? Users { get; set; }
}