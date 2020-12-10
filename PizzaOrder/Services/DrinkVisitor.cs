using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrder {
    public class DrinkVisitor : OrderableVisitor {
        public override double GetPrice(IEnumerable<IOrderable> items) {
            double totalPrice = 0;
            var drinks = items.Where(x => x is Drink);
            if (!drinks.Any()) {
                return totalPrice;
            }
            foreach (Drink drink in drinks) {
                totalPrice += CalculateTotalDrinkPrice(drink);
            }
            return totalPrice;
        }

        private double CalculateTotalDrinkPrice(Drink drink) {
            return drink.Price;
        }
        
    }
}
