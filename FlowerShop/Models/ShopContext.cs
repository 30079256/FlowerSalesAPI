using MongoDB.Driver;

namespace FlowerSales.API.Models
{
    public class ShopContext
    {
        private readonly IMongoDatabase _database;

        public ShopContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Product> Products => _database.GetCollection<Product>("products");
    }
}