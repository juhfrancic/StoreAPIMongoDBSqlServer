using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Products;
using TomadaStore.Models.Models;

namespace TomadaStore.Models.DTOs.Sale
{
    public class SaleResponseDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; init; }
        [BsonElement("customer")]
        public CustomerResponseDTO Customer { get; init; }
        [BsonElement("products")]
        public List<ProductResponseDTO> Products { get; init; }
        [BsonElement("saleDate")]
        public DateTime SaleDate { get; init; }
        [BsonElement("totalPrice")]
        public decimal TotalPrice { get; init; }
    }
}
