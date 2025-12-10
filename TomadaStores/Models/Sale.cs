using System;
using System.Collections.Generic;
using System.Text;

namespace TomadaStore.Models.Models
{
    public class Sale
    {
        public string SaleId { get; private set; }
        public string ProductId { get; private set; }
        public decimal TotalPrice { get; private set; }
        public string Situation { get; private set; } = string.Empty;

        public Sale(string saleId, string productId, decimal totalPrice, string situation)
        {
            SaleId = saleId;
            ProductId = productId;
            TotalPrice = totalPrice;
            Situation = situation;
        }
    }
}
