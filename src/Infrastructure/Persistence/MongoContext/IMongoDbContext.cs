using MongoDB.Driver;

namespace Infrastructure.Persistence.MongoContext
{
    public interface IMongoDbContext :IDisposable
    {
        Task<int> SaveChanges();
        Task AddCommand(Func<Task> func);
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
