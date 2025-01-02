using MongoDB.Driver;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence.MongoContext;

namespace Infrastructure.Persistence
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly IMongoDbContext _context;

        private readonly IMongoCollection<TEntity> _collection;

        public Repository(IMongoDbContext context)
        {
            _context = context;

            var nameCollection = typeof(TEntity).Name.ToLower();
            _collection = _context.GetCollection<TEntity>(nameCollection);
        }

        public async Task Add(TEntity entity)
        {
            await _context.AddCommand(async () =>
               await _collection.InsertOneAsync(entity));
        }

        public async Task Delete(TEntity entity)
        {
            await _context.AddCommand(async () =>
                await _collection.DeleteOneAsync(x => x.Id == entity.Id));
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var datas = await _collection.FindAsync(Builders<TEntity>.Filter.Empty);
            return datas.ToList();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            var data = await _collection.FindAsync(entity => entity.Id == id);
            return data.SingleOrDefault();
        }

        public async Task Update(TEntity entity)
        {
            entity.SetUpdatedAt();
            await _context.AddCommand(async () => await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity));
        }
    }
}
