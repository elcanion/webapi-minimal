using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaStore
{
    public class Product
    {
        private int id;
        private string name;
        private string description;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Product(int id, string name, string description){
            Id = id;
            Name = name;
            Description = description;
        }

    }
}