using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrder {
    public class OrderablePriceDecorator {
        public OrderablePriceDecorator() {

        }

        public double GetTotalOrderPrice(IEnumerable<IOrderable> orderables) {
            if (orderables.Count() == 0) {
                return 0;
            }
            var pizzaPrice = new PizzaVisitor().GetPrice(orderables);
            var drinksPrice = new DrinkVisitor().GetPrice(orderables);
            return pizzaPrice + drinksPrice;
        }
    }
}
