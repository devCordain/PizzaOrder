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
 * Lista alla beställningsbara produker -> (IOrderable)
 * Skapa Order -> Pizza/Läsk (IOrderable) (Objekt instansieras på Order) Basklass (Factory för Pizza) (Factory för Läsk)
 * Ta bort på Order -> Pizza/Läsk (IOrderable) (Objekt tas bort på Order)
 * Lägga till på Order -> Pizza/Läsk (IOrderable) (Objekt läggs till på Order) (PizzaBuilder -> (Factory för Pizza(IOrderable)).With(Topping(IAddable))
 * Lägga till på Pizza -> Tillbehör (IAddable) (Objekt instansieras på Pizza) (Extra ingredienser)
 * Ta bort på Pizza -> Tillbehör (IAddable) (Objekt tas bort på Pizza) 
 * Bekräfta Order -> Returnerar lista på ingredienser, produkter och pris (Decorator -> logga till pågående ordrar med kö) (ska returnera lista med ingredienser, alla produkter och totalt pris.)
 * Lista samtliga pågående Ordrar -> Skriver ut aktuell lista (Hämtar från Decorators pågående lista)
 * Avbryta Order -> 
 * Färdigställa Order ->
 *
 * Pizza.With(Cheese).With(Ham)With(Tomato).Build();
 */
