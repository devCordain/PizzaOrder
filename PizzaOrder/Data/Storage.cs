using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrder {
    public class Storage {
        public List<Order> data { get; set; }
        public Storage() {
            data = new List<Order>();
        }
    }
}
