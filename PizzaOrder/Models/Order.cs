using System.Collections.Generic;
using System.Linq;

namespace PizzaOrder {
    public class Order {
        public enum OrderStatus { Created, Cancelled, Confirmed, Completed }
        public int Id { get; }
        public OrderStatus Status { get; set; }
        public int TotalPrice { get { return Items.Count == 0 ? 0 : Items.Sum(x => x.Price); } }
        public List<IOrderable> Items { get;}

        public Order(int id, List<IOrderable> items) {
            Id = id;
            Items = items;
        }

        public void Add(IOrderable item) {
            Items.Add(item);
        }

        public void Remove(IOrderable item) {
            Items.Remove(item);
        }
    }
}