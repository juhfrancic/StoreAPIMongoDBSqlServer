using System;
using System.Collections.Generic;
using System.Text;

namespace TomadaStore.Models.Models
{
    public class Sales
    {
        public string Id { get; private set; }
        public Customer Customer { get; private set; }
        public List<Products> Products { get; private set; }
        public DateTime SaleDate { get; private set; }
        public decimal TotalPrice { get; private set; }

        public Sales(Customer customer, List<Products> products, decimal totalPrice)
        {
            Customer = customer;
            Products = products;
            SaleDate = DateTime.UtcNow;
            TotalPrice = totalPrice;
        }
    }
}
