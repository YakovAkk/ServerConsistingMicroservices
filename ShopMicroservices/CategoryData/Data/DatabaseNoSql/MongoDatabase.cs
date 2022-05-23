using MongoDB.Driver;

namespace CategoryData.Data.DatabaseNoSql
{
    public class MongoDatabase
    {
        private IMongoDatabase _db;
        public MongoDatabase()
        {
            string _connectionString = "mongodb://localhost:27017/Lego";
            MongoUrlBuilder _connection = new MongoUrlBuilder(_connectionString);
            MongoClient _client = new MongoClient(_connectionString);
            _db = _client.GetDatabase(_connection.DatabaseName);
        }

        public IMongoDatabase GetConnectionToDB()
        {
            return _db;
        }
    }
}
