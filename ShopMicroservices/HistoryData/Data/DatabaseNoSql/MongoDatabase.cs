using HistoryData.Attributes;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HistoryData.Data.DatabaseNoSql
{
    public class MongoDatabase<T>
    {
        private IMongoDatabase _db;
        private IMongoCollection<T> Collection { get; set; }
        public MongoDatabase(IOptions<HistoryStoreDatabaseSettings> legoStoreDatabaseSettings)
        {
            string _connectionString = $"{legoStoreDatabaseSettings.Value.ConnectionString}/{legoStoreDatabaseSettings.Value.DatabaseName}";
            MongoUrlBuilder _connection = new MongoUrlBuilder(_connectionString);
            MongoClient _client = new MongoClient(_connectionString);
            _db = _client.GetDatabase(_connection.DatabaseName);
            Collection = _db.GetCollection<T>(GetNameAtributes() == "" ? typeof(T).Name : GetNameAtributes());
        }

        public IMongoCollection<T> GetCollection()
        {
            return Collection;
        }

        private string GetNameAtributes()
        {
            var type = typeof(T);

            var atributes = type.GetCustomAttributes(typeof(NameCollectionAttribute), false);

            foreach (NameCollectionAttribute atribute in atributes)
            {
                return atribute.CollectionName;
            }
            return "";
        }
    }
}
