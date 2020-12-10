using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrder {
    public class OrderableFactory {

        private Dictionary<string, Func<IOrderable>> createOrderableInitializer = new Dictionary<string, Func<IOrderable>> {
            { "Margarita", () => new Pizza("Margarita", 85, new string[] { "Ost", "Tomatsås" }) },
            { "Hawaii", () => new Pizza("Hawaii", 95, new string[] { "Ost", "Tomatsås", "Skinka", "Ananas" }) },
            { "Kebabpizza", () => new Pizza("Kebabpizza", 105, new string[] { "Ost", "Tomatsås", "Kebab", "Champinjoner", "Lök", "Feferoni", "Isbergssallad", "Tomat", "Kebabsås"}) },
            { "Quatro Stagioni", () => new Pizza("Quatro Stagioni", 115, new string[] { "Ost", "Tomatsås", "Skinka", "Räkor", "Musslor", "Champinjoner", "Kronärtskocka"}) },
            { "Coca cola", () => new Drink("Coca cola", 20) },
            { "Fanta", () => new Drink("Fanta", 20) },
            { "Sprite", () => new Drink("Sprite", 25) }
        };

        public IOrderable Create(string type)
        {
            var initializer = createOrderableInitializer.Where(x => x.Key == type).FirstOrDefault();
            if (initializer.Value == null) {
                throw new ArgumentNullException("No such item type");
            }
            return initializer.Value.Invoke();
        }

        public IEnumerable<IOrderable> CreateAll() {
            List<IOrderable> orderables = new List<IOrderable>();
            foreach (var initializer in createOrderableInitializer) {
                orderables.Add(initializer.Value.Invoke());
            }
            return orderables;
        }
    }
}
