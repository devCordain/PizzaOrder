using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace PizzaOrder.Controllers {
    public class OrderableController : Controller {
        private IOrderableFactory orderableFactory = new IOrderableFactory();

        [HttpGet]
        public IOrderable GetOrderable(string type) {
            return orderableFactory.Create(type);
        }

        public IEnumerable<IOrderable> GetOrderables() {
            return orderableFactory.CreateAll();
        }
    }
}
