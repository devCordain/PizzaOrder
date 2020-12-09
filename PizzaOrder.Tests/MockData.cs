using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaOrder.Tests {
    public class MockData {
        public Dictionary<string, IOrderable> Orderables = new Dictionary<string, IOrderable> {
            { "Margarita", new Pizza("Margarita", 85, new string[] { "Ost", "Tomatsås" }) },
            { "Hawaii", new Pizza("Hawaii", 95, new string[] { "Ost", "Tomatsås", "Skinka", "Ananas" }) },
            { "Kebabpizza", new Pizza("Kebabpizza", 105, new string[] { "Ost", "Tomatsås", "Kebab", "Champinjoner", "Lök", "Feferoni", "Isbergssallad", "Tomat", "Kebabsås"}) },
            { "Quatro Stagioni", new Pizza("Quatro Stagioni", 115, new string[] { "Ost", "Tomatsås", "Skinka", "Räkor", "Musslor", "Champinjoner", "Kronärtskocka"}) },
            { "Coca cola", new Drink("Coca cola", 20) },
            { "Fanta", new Drink("Fanta", 20) },
            { "Sprite", new Drink("Sprite", 25) }
        };
        public MockData() { }
    }
}
