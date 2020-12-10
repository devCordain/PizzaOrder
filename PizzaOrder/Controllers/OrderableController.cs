using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace PizzaOrder.Controllers {
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class OrderableController : Controller {
        private OrderableFactory orderableFactory = new OrderableFactory();

        [HttpGet("/GetOrderable")]
        public IOrderable GetOrderable(string type) {
            return orderableFactory.Create(type);
        }

        [HttpGet("/GetOrderables")]
        public IEnumerable<IOrderable> GetOrderables() {
            return orderableFactory.CreateAll();
        }
    }
}
