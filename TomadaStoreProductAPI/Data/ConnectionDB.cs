using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TomadaStore.Models.Models;

namespace TomadaStoreProductAPI.Data
{
    public class ConnectionDB
    {
        public readonly IMongoCollection<Product> mongoCollection;
        public ConnectionDB(IOptions<MongoDBSettings> mongoDBSettings)
        {
           MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            mongoCollection = database.GetCollection<Product> (mongoDBSettings.Value.CollectionName);
        }

        public IMongoCollection<Product>  GetMongoConnection()
        {
            return mongoCollection;
        }
    }
}
