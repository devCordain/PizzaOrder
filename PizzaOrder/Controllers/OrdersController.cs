using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaOrder.Controllers {
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class OrdersController : Controller {
        private OrderableController orderableController = new OrderableController();
        private List<Order> orders = new List<Order>();
        private int orderIndex = 1;
        private List<IAddable> addables = new List<IAddable>() {
            new Topping("Skinka", 10),
            new Topping("Ananas", 10),
            new Topping("Champinjoner", 10),
            new Topping("Lök", 10),
            new Topping("Kebabsås", 10),
            new Topping("Räkor", 15),
            new Topping("Musslor", 15),
            new Topping("Kronärtskocka", 15),
            new Topping("Kebab", 20),
            new Topping("Koriander", 20)
        };

        [HttpPost]
        public Order Create(IEnumerable<string> itemNames) {
            var items = itemNames.Select(x => orderableController.GetOrderable(x)).ToList();
            var order = new Order(orderIndex, items);
            orders.Add(order);
            orderIndex++;
            return order;
        }

        [HttpPost]
        public void RemoveItemFromOrder(int orderId, string itemName) {
            var order = orders.Where(x => x.Id == orderId).FirstOrDefault();
            var item = order.Items.Where(x => x.Name == itemName).FirstOrDefault();
            if (item == null) throw new ArgumentNullException();
            order.Items.Remove(item);
        }

        [HttpGet]
        public Order GetOrder(int orderId) {
            return orders.Where(x => x.Id == orderId).FirstOrDefault();
        }

        [HttpPut]
        public void AddItemToOrder(int orderId, string itemName) {
            var order = orders.Where(x => x.Id == orderId).FirstOrDefault();
            var item = orderableController.GetOrderable(itemName);
            if (item == null) throw new ArgumentNullException();
            order.Items.Add(item);
        }

        [HttpPut]
        public void AddAddable(int orderId, int itemIndex, string addableName) {
            var order = orders.Where(x => x.Id == orderId).FirstOrDefault();
            var addable = addables.Where(x => x.Name == addableName).FirstOrDefault();
            if (addable == null) throw new ArgumentNullException();
            (order.Items[itemIndex] as Pizza).Addables.Add(addable);
        }
    }
}
