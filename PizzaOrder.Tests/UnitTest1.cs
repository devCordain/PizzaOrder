using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PizzaOrder.Tests {
    [TestClass]
    public class UnitTest1 {
        [DataTestMethod]
        [DataRow("Margerita")]
        [DataRow("Hawaii")]
        [DataRow("Kebabpizza")]
        [DataRow("Quatro Stagioni")]
        [DataRow("Coca cola")]
        [DataRow("Fanta")]
        [DataRow("Sprite")]

        public void GetOrderable_Should_return_a_JSON_with_the_orderable_item(string input) {
            var expected = JObject.Parse(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "/orderableItems.json"))[input].First;
            var controller = new OrdersController();
            Assert.AreEqual(expected, controller.GetOrderable(input));
        }

        [TestMethod]
        public void GetOrderables_Should_return_a_JSON_with_all_orderables() {
        }

        [TestMethod]
        public void GetOrder_Should_return_a_JSON_the_order() {
        }

        [TestMethod]
        public void GetOrders_Should_return_a_JSON_with_all_orders() {
        }
    }
}
/* 
 * Lista alla best�llningsbara produker -> (IOrderable)
 * Skapa Order -> Pizza/L�sk (IOrderable) (Objekt instansieras p� Order) Basklass (Factory f�r Pizza) (Factory f�r L�sk)
 * Ta bort p� Order -> Pizza/L�sk (IOrderable) (Objekt tas bort p� Order)
 * L�gga till p� Order -> Pizza/L�sk (IOrderable) (Objekt l�ggs till p� Order) (PizzaBuilder -> (Factory f�r Pizza(IOrderable)).With(Topping(IAddable))
 * L�gga till p� Pizza -> Tillbeh�r (IAddable) (Objekt instansieras p� Pizza) (Extra ingredienser)
 * Ta bort p� Pizza -> Tillbeh�r (IAddable) (Objekt tas bort p� Pizza) 
 * Bekr�fta Order -> Returnerar lista p� ingredienser, produkter och pris (Decorator -> logga till p�g�ende ordrar med k�) (ska returnera lista med ingredienser, alla produkter och totalt pris.)
 * Lista samtliga p�g�ende Ordrar -> Skriver ut aktuell lista (H�mtar fr�n Decorators p�g�ende lista)
 * Avbryta Order -> 
 * F�rdigst�lla Order ->
 *
 * Pizza.With(Cheese).With(Ham)With(Tomato).Build();
 */
