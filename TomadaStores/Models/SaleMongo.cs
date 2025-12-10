using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TomadaStore.Models.Models
{
    public class SaleMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement("saleId")]
        public string SaleId { get; set; } = string.Empty;
        [BsonElement("productId")]
        public string ProductId { get; set; } = string.Empty;
        [BsonElement("totalPrice")]
        public decimal TotalPrice { get; set; }
        [BsonElement("situacao")]
        public string Situacao { get; set; } = string.Empty;

        public SaleMongo() { }
    }
}
