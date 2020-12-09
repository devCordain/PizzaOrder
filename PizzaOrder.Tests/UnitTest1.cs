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

        [DataTestMethod]
        [DataRow("Margarita")]
        [DataRow("Hawaii")]
        [DataRow("Kebabpizza")]
        [DataRow("Quatro Stagioni")]
        [TestMethod]
        public void GetOrderable_Should_return_a_correct_list_of_toppings(string input) {
            var mockData = new MockData();
            var expected = (mockData.Orderables.Where(x => x.Key == input).FirstOrDefault().Value as Pizza).Toppings;
            var controller = new OrderableController();
            var actual = (controller.GetOrderable(input) as Pizza).Toppings;
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length - 1; i++) {
                Assert.AreEqual(expected[i], actual[i]);
            }
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
            var actual = controller.Get(1);
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
            var actual = controller.Get(1);
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
            var actual = controller.Get(1);
            Assert.AreEqual(0, (actual.Items[2] as Pizza).Addables.Count);
            controller.AddAddable(1, 2, inputAddable);
            Assert.AreEqual(1, (actual.Items[2] as Pizza).Addables.Count);
            Assert.AreEqual(240, actual.TotalPrice);
        }

        [TestMethod]
        public void Remove_Addables_to_pizza_should_succeed() {
            var inputNames = new List<string>() { "Hawaii", "Fanta", "Kebabpizza" };
            var controller = new OrdersController();
            controller.Create(inputNames);
            var inputAddable = "Kebab";
            var actual = controller.Get(1);
            Assert.AreEqual(0, (actual.Items[2] as Pizza).Addables.Count);
            controller.AddAddable(1, 2, inputAddable);
            Assert.AreEqual(1, (actual.Items[2] as Pizza).Addables.Count);
            controller.RemoveAddable(1, 2, inputAddable);
            Assert.AreEqual(0, (actual.Items[2] as Pizza).Addables.Count);
            Assert.AreEqual(220, actual.TotalPrice);
        }

        [TestMethod]
        public void Confirm_order_should_return_list_of_ingrediens_and_products_and_price() {
            var inputNames = new List<string>() { "Hawaii", "Fanta", "Kebabpizza" };
            var controller = new OrdersController();
            controller.Create(inputNames);
            var inputAddable = "Kebab";
            controller.AddAddable(1, 2, inputAddable);
            var actual = controller.Confirm(1);
            Assert.AreEqual(Order.OrderStatus.Confirmed, actual.Status);
        }

        [TestMethod]
        public void Confirm_order_should_throw_expected_exception_if_status_is_wrong() {
            var inputNames = new List<string>() { "Hawaii", "Fanta", "Kebabpizza" };
            var controller = new OrdersController();
            controller.Create(inputNames);
            controller.Cancel(1);
            Assert.ThrowsException<InvalidOperationException>(() => controller.Confirm(1));
        }

        [TestMethod]
        public void Get_ongoing_Should_return_all_active_orders() {
            var inputData = new List<List<string>>() {
                new List<string>() { "Margarita", "Fanta", "Margarita" },
                new List<string>() { "Kebabpizza", "Sprite", "Kebabpizza" },
                new List<string>() { "Hawaii", "Coca cola", "Kebabpizza" },
                new List<string>() { "Margarita", "Fanta", "Kebabpizza" },
                new List<string>() { "Hawaii", "Sprite", "Kebabpizza" },
                new List<string>() { "Kebabpizza", "Fanta", "Kebabpizza" },
                new List<string>() { "Hawaii", "Sprite", "Kebabpizza" },
                new List<string>() { "Margarita", "Fanta", "Kebabpizza" }
            };
            var ordersController = new OrdersController();
            foreach (var inputNames in inputData) {
                ordersController.Create(inputNames);
            }
            for (int i = 1; i < inputData.Count; i+=2) {
                ordersController.Confirm(i);
            }
            var actual = ordersController.GetActive();
            Assert.AreEqual(4, actual.Count());
        }

        [TestMethod]
        public void Cancel_order_should_succeed() {
            var inputNames = new List<string>() { "Hawaii", "Fanta", "Kebabpizza" };
            var controller = new OrdersController();
            controller.Create(inputNames);
            controller.Cancel(1);
            var actual = controller.Get(1);
            Assert.AreEqual(Order.OrderStatus.Cancelled, actual.Status);
        }

        [TestMethod]
        public void Cancel_order_should_throw_expected_exception_if_status_is_wrong() {
            var inputNames = new List<string>() { "Hawaii", "Fanta", "Kebabpizza" };
            var controller = new OrdersController();
            controller.Create(inputNames);
            controller.Confirm(1);
            controller.Complete(1);
            Assert.ThrowsException<InvalidOperationException>(() => controller.Cancel(1));
        }

        [TestMethod]
        public void Complete_should_succeed() {
            var inputNames = new List<string>() { "Hawaii", "Fanta", "Kebabpizza" };
            var controller = new OrdersController();
            controller.Create(inputNames);
            controller.Confirm(1);
            controller.Complete(1);
            var actual = controller.Get(1);
            Assert.AreEqual(Order.OrderStatus.Completed, actual.Status);
        }
        
        [TestMethod]
        public void Complete_order_should_throw_expected_exception_if_status_is_wrong() {
            var inputNames = new List<string>() { "Hawaii", "Fanta", "Kebabpizza" };
            var controller = new OrdersController();
            controller.Create(inputNames);
            Assert.ThrowsException<InvalidOperationException>(() => controller.Complete(1));
        }

    }
}
/* 
 * X - Lista alla best�llningsbara produker -> (IOrderable) 
 * X - Skapa Order -> Pizza/L�sk (IOrderable) (Objekt instansieras p� Order) Basklass (Factory f�r Pizza) (Factory f�r L�sk)
 * X - Ta bort p� Order -> Pizza/L�sk (IOrderable) (Objekt tas bort p� Order)
 * X - L�gga till p� Order -> Pizza/L�sk (IOrderable) (Objekt l�ggs till p� Order) (PizzaBuilder -> (Factory f�r Pizza(IOrderable)).With(Topping(IAddable))
 * X - L�gga till p� Pizza -> Tillbeh�r (IAddable) (Objekt instansieras p� Pizza) (Extra ingredienser)
 * X - Ta bort p� Pizza -> Tillbeh�r (IAddable) (Objekt tas bort p� Pizza) 
 * X - Bekr�fta Order -> Returnerar lista p� ingredienser, produkter och pris.
 * X - Lista samtliga p�g�ende Ordrar -> Skriver ut aktuell lista (H�mtar fr�n Decorators p�g�ende lista)
 * X - Avbryta Order -> 
 * X - F�rdigst�lla Order ->
 * X Testa exceptions
 * Pizza.With(Cheese).With(Ham)With(Tomato).Build();
 */
