using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrder {
    public class OrderablePriceDecorator : OrderableVisitor {
        public override double GetPrice(IEnumerable<IOrderable> items) {
            if (items.Count() == 0) {
                return 0;
            }
            var pizzaPrice = new PizzaVisitor().GetPrice(items);
            var drinksPrice = new DrinkVisitor().GetPrice(items);
            return pizzaPrice + drinksPrice;
        }
    }
}
