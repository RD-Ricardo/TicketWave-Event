using Domain.Entities;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

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
            _commands = [];
            
            _configuration = configuration;
            _mongoClient = new MongoClient(_configuration["MongoSettings:Connection"]);

            _database = _mongoClient.GetDatabase(_configuration["MongoSettings:DatabaseName"]);

            _session = (IClientSessionHandle)null!;

            Entities();
        }

        public void Entities()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(BaseEntity)))
            {
                BsonClassMap.RegisterClassMap<BaseEntity>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdMember(c => c.Id)
                        .SetSerializer(new GuidSerializer(GuidRepresentation.Standard));
                });
            }

            if (!BsonClassMap.IsClassMapRegistered(typeof(Event)))
            {
                BsonClassMap.RegisterClassMap<Event>(cm =>
                {
                    cm.AutoMap();
                });
            }
        }

        public async Task<int> SaveChanges()
        {
            var commandTasks = _commands.Select(c => c());

            try
            {
                await Task.WhenAll(commandTasks);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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
