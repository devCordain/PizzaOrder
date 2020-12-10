using System.Collections.Generic;
using System.Linq;

namespace PizzaOrder {
    public class Order {
        public enum OrderStatus { Created, Cancelled, Confirmed, Completed }
        public int Id { get; }
        public OrderStatus Status { get; set; }
        public double TotalPrice { get { return new OrderablePriceDecorator().GetPrice(Items); } }
        public List<IOrderable> Items { get;}

        public Order(int id, List<IOrderable> items) {
            Id = id;
            Items = items;
        }
    }
}