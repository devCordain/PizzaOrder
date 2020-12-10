using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrder {
    public abstract class OrderableVisitor {
        public abstract double GetPrice(IEnumerable<IOrderable> items);
    }
}
