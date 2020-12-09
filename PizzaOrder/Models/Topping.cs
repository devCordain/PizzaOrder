using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrder {
    public class Topping : IAddable {
        public string Name { get; }
        public int Price { get; }

        public Topping(string name, int price) {
            Name = name;
            Price = price;
        }
    }
}
