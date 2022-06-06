﻿using LegoData.Data.DatabaseMongo;
using LegoData.Data.Models.Base;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LegoRepository.RepositoriesMongo.Base
{
    public abstract class BaseRepository<T> : IRepository<T> where T : IModel
    {
        abstract protected IMongoCollection<T> Collection { get; set; }
        public BaseRepository(MongoDatabase<T> mongoDatabase)
        {
            Collection = mongoDatabase.GetCollection();
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
        public async virtual Task<T> GetByIDAsync(string id)
        {
            var item = await Collection.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
            if (item == null)
            {
                return default(T);
            }
            return item;
        }
        public abstract Task<T> DeleteAsync(string id);
        public abstract Task<T> AddAsync(T item);
        public abstract Task<T> UpdateAsync(T item);
    }
}