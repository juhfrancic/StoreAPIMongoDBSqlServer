using System;
using System.Collections.Generic;
using System.Text;

namespace TomadaStore.Models.Models
{
    public class Products
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string  Description{ get; private set; }
        public decimal Price { get; private set; }
        public Category Category { get; private set; }

        public Products(string name, string description, decimal price, Category category)
        {
            Name = name;
            Description = description;
            Price = price;
            Category = category;
        }
    }
}
