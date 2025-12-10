using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TomadaStore.Models.DTOs.Sale
{
    public class SaleRequestDTO
    {
        public string SaleId { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
    }
}
