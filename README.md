# PizzaOrder

Andreas Zachrisson
Tommy Karlsson

# Factory Pattern (OrderableFactory.cs)
För att skapa instanser av Pizzor och Drycker, utan att behöva veta typen. 
Det skapar också alla varianter av Pizzor och Drycker som ska finnas och därmed abstraherar vi bort nödvändigheten att veta tex alla ingredienser av en Margarita. 

# Visitor Pattern (OrderableVisitor.cs, PizzaVisitor.cs, DrinkVisitor.cs)
Genom att implementera Visitor Pattern så säkerställer vi att vi inte har logik i objekten Pizza och Drink. 
Istället implementerar vi logiken för att tex summera totalpris för objekten i en Visitor.

# Decorator Pattern (OrderablePriceDecorator.cs)
Genom att implementera en Decorator för vår OrderableVisitor så möjliggör vi att ställa logik som inte hör till någon av våra Visitors på ett ställe. Tex att summera priset av Pizzor och Drink tillsammans, eller i framtiden att kunna ändra lagersaldo beroende på ingredienser mm.


