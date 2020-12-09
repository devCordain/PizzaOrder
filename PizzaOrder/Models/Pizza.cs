using System.Collections.Generic;
using System.Linq;

namespace PizzaOrder { 
    public class Pizza : IOrderable {
        public string Name { get; }
        public int Price { get { return Addables.Count == 0 ? basePrice : basePrice + Addables.Sum(x => x.Price); } }
        private int basePrice;
        public string[] Toppings { get; }
        public List<IAddable> Addables { get;}

        public Pizza(string name, int price, string[] toppings, List<IAddable> addables = null) {
            Name = name;
            basePrice = price;
            Toppings = toppings;
            Addables = addables ?? new List<IAddable>();
        }
    }
}