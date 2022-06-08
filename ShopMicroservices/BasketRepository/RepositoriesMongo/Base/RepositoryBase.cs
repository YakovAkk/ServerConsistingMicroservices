using BasketData.Data.DatabaseMongo;
using BasketData.Data.Models.Base;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BasketRepository.RepositoriesMongo.Base
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : IModel
    {
        abstract protected IMongoCollection<T> Collection { get; set; }
        public RepositoryBase(MongoDatabase<T> mongoDatabase)
        {
            Collection = mongoDatabase.GetCollection();
        }

        public abstract Task<T> DeleteAsync(string id);
        public async virtual Task<List<T>> GetAllAsync()
        {
            var collection = await Collection.Find(_ => true).ToListAsync();
            if (collection == null)
            {
                return new List<T>();
            }
            return collection;
        }
        public abstract Task<T> GetByIDAsync(string id);
        public abstract Task<T> AddAsync(T item);
        public abstract Task<T> UpdateAsync(T item);
    }
}
