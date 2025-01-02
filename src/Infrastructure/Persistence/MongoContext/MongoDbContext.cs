using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.MongoContext
{
    internal class MongoDbContext : IMongoDbContext
    {
        private IClientSessionHandle? _session;
        
        private readonly IMongoDatabase _database;
        
        private readonly MongoClient _mongoClient;
        
        private readonly List<Func<Task>> _commands;

        private readonly IConfiguration _configuration;

        public MongoDbContext(IConfiguration configuration)
        {
            _configuration = configuration;

            _commands = [];

            _mongoClient = new MongoClient(_configuration["MongoSettings:Connection"]);

            _database = _mongoClient.GetDatabase(_configuration["MongoSettings:DatabaseName"]);

            _session = (IClientSessionHandle)null!;
        }

        public async Task<int> SaveChanges()
        {
            using (_session = await _mongoClient.StartSessionAsync())
            {
                var commandTasks = _commands.Select(c => c());

                try
                {
                    await Task.WhenAll(commandTasks);

                    await _session.CommitTransactionAsync(new CancellationToken());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return _commands.Count;
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }

        public void Dispose()
        {
            _session?.Dispose();
            GC.SuppressFinalize(this);
        }

        public Task AddCommand(Func<Task> func)
        {
            _commands.Add(func);
            return Task.CompletedTask;
        }

        public async Task<bool> Commit()
        {
            var changeAmount = await SaveChanges();

            return changeAmount > 0;
        }
    }
}
