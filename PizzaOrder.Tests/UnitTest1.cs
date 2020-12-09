using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PizzaOrder.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PizzaOrder.Tests {
    [TestClass]
    public class UnitTest1 {
        [DataTestMethod]
        [DataRow("Margarita")]
        [DataRow("Hawaii")]
        [DataRow("Kebabpizza")]
        [DataRow("Quatro Stagioni")]
        [DataRow("Coca cola")]
        [DataRow("Fanta")]
        [DataRow("Sprite")]

        public void GetOrderable_Should_return_a_correct_name_and_price(string input) {
            var mockData = new MockData();
            var expected = mockData.Orderables.Where(x => x.Key == input).FirstOrDefault().Value;
            var controller = new OrderableController();
            Assert.AreEqual(expected.Name, controller.GetOrderable(input).Name);
            Assert.AreEqual(expected.Price, controller.GetOrderable(input).Price);
        }

        [TestMethod]
        public void GetOrderables_Should_return_a_correct_name_and_price_for_all_orderables() {
            var mockData = new MockData();
            var expected = mockData.Orderables;
            var controller = new OrderableController();
            var actual = controller.GetOrderables();
            foreach (var expectedItem in expected) {
                var actualItem = controller.GetOrderables().Where(x => x.Name == expectedItem.Value.Name).FirstOrDefault();
                Assert.AreEqual(expectedItem.Value.Name, actualItem.Name);
                Assert.AreEqual(expectedItem.Value.Price, actualItem.Price);
            }
        }

        [TestMethod]
        public void Create_Order_should_return_correct_values() {
            var inputNames = new List<string>() { "Hawaii", "Fanta", "Kebabpizza" };
            var controller = new OrdersController();
            var actual = controller.Create(inputNames);
            Assert.AreEqual(1, actual.Id);
            Assert.AreEqual(Order.OrderStatus.Created, actual.Status);
            foreach (var item in actual.Items) {
                var expectedItem = new MockData().Orderables.Where(x => x.Key == item.Name).FirstOrDefault();
                Assert.AreEqual(expectedItem.Value.Name, item.Name);
                Assert.AreEqual(expectedItem.Value.Price, item.Price);
            }
            Assert.AreEqual(220, actual.TotalPrice);
        }

        [TestMethod]
        public void Remove_from_Order_should_succeed() {
            var inputNames = new List<string>() { "Fanta" };
            var controller = new OrdersController();
            controller.Create(inputNames);
            controller.RemoveItemFromOrder(1, "Fanta");
            var actual = controller.GetOrder(1);
            Assert.AreEqual(1, actual.Id);
            Assert.AreEqual(0, actual.Items.Count);
            Assert.AreEqual(0, actual.TotalPrice);
        }

        [TestMethod]
        public void Add_to_Order_should_succeed() {
            var inputNames = new List<string>() { "Fanta" };
            var controller = new OrdersController();
            controller.Create(inputNames);
            controller.AddItemToOrder(1, "Coca cola");
            var actual = controller.GetOrder(1);
            Assert.AreEqual(1, actual.Id);
            Assert.AreEqual(2, actual.Items.Count);
            Assert.AreEqual(40, actual.TotalPrice);
        }

        [TestMethod]
        public void Add_Addables_to_pizza_should_succeed() {
            var inputNames = new List<string>() { "Hawaii", "Fanta", "Kebabpizza" };
            var controller = new OrdersController();
            controller.Create(inputNames);
            var inputAddable = "Kebab";
            var actual = controller.GetOrder(1);
            Assert.AreEqual(0, (actual.Items[2] as Pizza).Addables.Count);
            controller.AddAddable(1, 2, inputAddable);
            Assert.AreEqual(1, (actual.Items[2] as Pizza).Addables.Count);
            Assert.AreEqual(240, actual.TotalPrice);
        }
    }
}
/* 
 * X - Lista alla beställningsbara produker -> (IOrderable) 
 * X - Skapa Order -> Pizza/Läsk (IOrderable) (Objekt instansieras på Order) Basklass (Factory för Pizza) (Factory för Läsk)
 * X - Ta bort på Order -> Pizza/Läsk (IOrderable) (Objekt tas bort på Order)
 * X - Lägga till på Order -> Pizza/Läsk (IOrderable) (Objekt läggs till på Order) (PizzaBuilder -> (Factory för Pizza(IOrderable)).With(Topping(IAddable))
 * X - Lägga till på Pizza -> Tillbehör (IAddable) (Objekt instansieras på Pizza) (Extra ingredienser)
 * Ta bort på Pizza -> Tillbehör (IAddable) (Objekt tas bort på Pizza) 
 * Bekräfta Order -> Returnerar lista på ingredienser, produkter och pris (Decorator -> logga till pågående ordrar med kö) (ska returnera lista med ingredienser, alla produkter och totalt pris.)
 * Lista samtliga pågående Ordrar -> Skriver ut aktuell lista (Hämtar från Decorators pågående lista)
 * Avbryta Order -> 
 * Färdigställa Order ->
 *
 * Pizza.With(Cheese).With(Ham)With(Tomato).Build();
 */
