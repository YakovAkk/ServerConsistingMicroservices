using CategoryData.Attributes;
using CategoryData.Data.DatabaseNoSql;
using CategoryData.Data.Models.Base;
using MongoDB.Bson;
using MongoDB.Driver;


namespace CategoryRepositories.RepositoriesMongo.Base
{
    public abstract class MongoDbBase<T> : IMongoDB<T> where T : IModel
    {
        private readonly IMongoDatabase _db;
        abstract protected IMongoCollection<T> Collection { get; set; }
        public MongoDbBase()
        {
            _db = new MongoDatabase().GetConnectionToDB();
            Collection = _db.GetCollection<T>(GetNameAtributes() == "" ? typeof(T).Name : GetNameAtributes());
        }
        public string GetNameAtributes()
        {
            var type = typeof(T);

            var atributes = type.GetCustomAttributes(typeof(NameCollectionAttribute), false);

            foreach (NameCollectionAttribute atribute in atributes)
            {
                return atribute.CollectionName;
            }
            return "";
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
        public async virtual Task<T> GetByIDAsync(string id)
        {
            var item = await Collection.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
            if (item == null)
            {
                return default(T);
            }
            return item;
        }
        public async virtual Task<T> GetByNameAsync(string name)
        {
            var allItems = await GetAllAsync();

            return allItems.FirstOrDefault(i => i.Name == name);
        }
        public abstract Task<T> AddAsync(T item);
        public abstract Task<T> UpdateAsync(T item);
    }
}
