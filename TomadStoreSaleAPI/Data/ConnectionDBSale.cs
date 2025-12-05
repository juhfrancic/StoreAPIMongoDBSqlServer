using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TomadaStore.Models.Models;
using TomadStoreSaleAPI.Data;

namespace TomadaStoreSaleAPI.Data
{
    public class ConnectionDBSale
    {
        public readonly IMongoCollection<Sale> mongoCollection;

        public ConnectionDBSale(IOptions<MongoDBSettings> mongoDbSettings)
        {
            MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            mongoCollection = database.GetCollection<Sale>(mongoDbSettings.Value.CollectionName);
        }

        public IMongoCollection<Sale> GetMongoCollection()
        {
            return mongoCollection;
        }
    }
}
