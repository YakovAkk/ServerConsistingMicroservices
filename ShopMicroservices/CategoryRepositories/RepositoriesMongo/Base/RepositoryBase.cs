using CategoryData.Attributes;
using CategoryData.Data.DatabaseNoSql;
using CategoryData.Data.Models.Base;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CategoryRepositories.RepositoriesMongo.Base
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : IModel
    {
        abstract protected IMongoCollection<T> Collection { get; set; }
        public RepositoryBase(MongoDatabase<T> mongoDatabase)
        {
            Collection  = mongoDatabase.GetCollection();
        }
        
        public async virtual Task DeleteAsync(string id)
        {
            await Collection.DeleteOneAsync(i => i.Id == id);
        }
        public async virtual Task<List<T>> GetAllAsync()
        {
            var collection = await Collection.Find(_ => true).ToListAsync();
            if (collection == null)
            {
                return new List<T>();
            }
            return collection;
        } 
        public async virtual Task<T> GetByNameAsync(string name)
        {
            var allItems = await GetAllAsync();

            return allItems.FirstOrDefault(i => i.Name == name);
        }
        public abstract Task<T> GetByIDAsync(string id);
        public abstract Task<T> AddAsync(T item);
        public abstract Task<T> UpdateAsync(T item);
    }
}
