using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrder {
    public interface IOrderable {
        public string Name { get;}
        public int Price { get;}
    }
}