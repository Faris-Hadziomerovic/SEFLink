using SEFLink.UI.HCI.Models;
using System.Collections.Generic;

namespace SEFLink.UI.HCI.Data
{
    public static class DataProvider
    {
        public static class Food
        {
            public static OrderItem Hamburger = new OrderItem
            {
                Image = "Hamburger",
                Name = "Hamburger",
                Price = 4.99M,
                Description = "Beef hamburger with lettuce, tomatoes and cheddar cheese."
            };
        
            public static OrderItem ChickenTenders = new OrderItem
            {
                Image = "Chicken",
                Name = "Chicken tenders",
                Price = 5.49M,
                Description = "Deep fried chicken tenders with gold sauce."
            };
            
            public static OrderItem FrenchFries = new OrderItem
            {
                Image = "Fries",
                Name = "French Fries",
                Price = 1.50M,
                Description = "French fries with mayo and ketchup."
            };
        
            public static OrderItem PepperoniPizza = new OrderItem
            {
                Image = "Pizza1",
                Name = "Pepperoni pizza",
                Price = 8.99M,
                Description = "Thin crust pizza with pepperoni and gauda cheese topping..."
            };

            public static OrderItem NapolitanPizza = new OrderItem
            {
                Image = "Pizza2",
                Name = "Napolitan pizza",
                Price = 11.49M,
                Description = "Thick crust pizza with mozzarella cheese, mushrooms and tomatoe topping..."
            };
        
            public static OrderItem MexicanPizza = new OrderItem
            {
                Image = "Pizza3",
                Name = "Mexican pizza",
                Price = 10.49M,
                Description = "Pizza with gauda cheese, sausages, corn and pepper topping..."
            };

        }
        
        public static class Drinks
        {
            public static OrderItem CafeLatte = new OrderItem
            {
                Image = "CafeLatte",
                Name = "Cafe Latte",
                Price = 2.00M,
                Description = "Coffee with milk."
            };
        
            public static OrderItem Cappuccino = new OrderItem
            {
                Image = "Cappuccino",
                Name = "Cappuccino",
                Price = 3.00M,
                Description = "Classic cappuccino with a foamy top."
            };
            
            public static OrderItem HotChocolate = new OrderItem
            {
                Image = "HotChocolate",
                Name = "Hot Chocolate",
                Price = 3.50M,
                Description = "Hot chocolate drink with whipped cream on top."
            };
        
            public static OrderItem Cola = new OrderItem
            {
                Image = "Cola",
                Name = "Coca Cola 0.33l",
                Price = 2.49M,
                Description = "Refreshing fizzy drink."
            };

            public static OrderItem Sprite = new OrderItem
            {
                Image = "Sprite",
                Name = "Sprite 0.33l",
                Price = 2.49M,
                Description = "Refreshing lemony fizzy drink."
            };
        
            public static OrderItem Fanta = new OrderItem
            {
                Image = "Fanta",
                Name = "Fanta 0.33l",
                Price = 2.49M,
                Description = "Refreshing orange fizzy drink."
            };
        }

        public static List<OrderItem> FoodItems = new List<OrderItem>
        {
            Food.Hamburger,
            Food.ChickenTenders,
            Food.FrenchFries,
            Food.PepperoniPizza,
            Food.NapolitanPizza,
            Food.MexicanPizza,
        };
        
        public static List<OrderItem> DrinkItems = new List<OrderItem>
        {
            Drinks.CafeLatte,
            Drinks.Cappuccino,
            Drinks.HotChocolate,
            Drinks.Cola,
            Drinks.Sprite,
            Drinks.Fanta,
        };
    }
}
