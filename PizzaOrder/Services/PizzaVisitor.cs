using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrder {
    public class PizzaVisitor : OrderableVisitor {
        public override double GetPrice(IEnumerable<IOrderable> items) {
            double totalPrice = 0;
            var pizzas = items.Where(x => x is Pizza);
            if (!pizzas.Any()) {
                return totalPrice;
            }
            foreach (Pizza pizza in pizzas) {
                totalPrice += CalculateTotalPizzaPrice(pizza);
            }
            return totalPrice;
        }

        private double CalculateTotalPizzaPrice(Pizza pizza) {
            return pizza.Addables.Count == 0 ? pizza.Price : pizza.Price + pizza.Addables.Sum(x => x.Price);
        }
        
    }
}
