using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace poject_2_labben
{
    internal class Program
    {
        private static List<Customer> customers = new List<Customer>();
        private static List<Product> clothingStock = new List<Product>();

        static Program()
        {
            customers.Add(new GoldCustomer("Knatte", "123"));
            customers.Add(new SilverCustomer("Fnatte", "321"));
            customers.Add(new BronzeCustomer("Tjatte", "213"));

            clothingStock.AddRange(new List<Product>
            {
                new Product("T-shirt", 150),
                new Product("Jeans", 500),
                new Product("Keps", 100),
                new Product("Jacka", 800),
                new Product("Sneakers", 700)
            });
        }

        static void Main(string[] args)
        {
            string choice;
            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to our online clothing shop!");
                Console.WriteLine("1 . Create a new customer.");
                Console.WriteLine("2 . Log in if you are already a customer.");
                Console.WriteLine("3 . Exit the program");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        CreateNewCustomer();
                        break;
                    case "2":
                        Login();
                        break;
                    case "3":
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again!");
                        break;
                }
            }
            while (choice != "3");
        }

        private static void CreateNewCustomer()
        {
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();

            if (customers.Any(c => c.Name == name))
            {
                Console.WriteLine("A customer with this name already exists!");
                return;
            }

            Console.WriteLine("Enter a password.");
            string password = Console.ReadLine();
            var newCustomer = new BronzeCustomer(name, password);
            customers.Add(newCustomer);
            Console.WriteLine($"Customer {name} added. Congratulation now you are a bronze customer.");
            ShoppingMenu(newCustomer);
        }

        private static void Login()
        {
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();

            var customer = customers.FirstOrDefault(c => c.Name == name);
            if (customer == null)
            {
                Console.WriteLine("No customer with this name exists!");
                return;
            }

            int maxAttempts = 3;
            bool successfulLogin = false;
            while (maxAttempts > 0 && !successfulLogin)
            {
                Console.WriteLine("Enter your password:");
                string password = Console.ReadLine();

                if (customer.ConfirmPassword(password))
                {
                    Console.WriteLine($"Welcome back, {name}!");
                    successfulLogin = true;
                    ShoppingMenu(customer);
                }
                else
                {
                    maxAttempts--;
                    if (maxAttempts > 0)
                    {
                        Console.WriteLine($"Incorrect password. You have {maxAttempts} attempts left.");
                    }
                    else
                    {
                        Console.WriteLine("Too many incorrect attempts. Please try again later. Thank you....");
                    }
                }
            }
        }

        private static void ShoppingMenu(Customer customer)
        {
            bool shopping = true;
            while (shopping)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. View products.");
                Console.WriteLine("2. Add product to cart.");
                Console.WriteLine("3. View cart.");
                Console.WriteLine("4. Remove product from cart.");
                Console.WriteLine("5. Checkout.");
                Console.WriteLine("6. Exit");

                Console.WriteLine("Enter your choice:");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("\nAvailable products:");
                        for (int i = 0; i < clothingStock.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {clothingStock[i].Name} - {clothingStock[i].Price}kr");
                        }
                        break;
                    case "2":
                        Console.Write("Enter product number to add to your cart: ");
                        if (int.TryParse(Console.ReadLine(), out int choiceAdd) && choiceAdd >= 1 && choiceAdd <= clothingStock.Count)
                        {
                            customer.AddToCart(clothingStock[choiceAdd - 1]);
                            Console.WriteLine($"{clothingStock[choiceAdd - 1].Name} added to your cart!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice, try again.....");
                        }
                        break;
                    case "3":
                        customer.ViewCart();
                        break;
                    case "4":
                        Console.Write("Enter the number of the product you want to remove from your cart: ");
                        customer.ViewCart();
                        if (int.TryParse(Console.ReadLine(), out int choiceRemove))
                        {
                            customer.RemoveFromCart(choiceRemove - 1);
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice. Try again...");
                        }
                        break;
                    case "5":
                        customer.Checkout();
                        break;
                    case "6":
                        shopping = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice, try again....");
                        break;
                }
            }
        }

        private static void CustomerMenu(Customer customer)
        {
            string choice;
            do
            {
                Console.Clear();
                Console.WriteLine($"Welcome {customer.Name}!");
                Console.WriteLine("1. View available clothes and add to cart");
                Console.WriteLine("2. View cart and checkout.");
                Console.WriteLine("3. Log out.");
                Console.WriteLine("4. Switch currency");
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayAvailableClothes(customer);
                        break;
                    case "2":
                        ViewCart(customer);
                        break;
                    case "3":
                        break;
                    case "4":
                        SwitchCurrency(customer);
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again!");
                        break;
                }
            } while (choice != "3");
        }

        private static void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the online clothing store!");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register new customer");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Login();
                        break;
                    case "2":
                        RegisterNewCustomer();
                        break;
                    case "3":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option! Please try again.");
                        break;
                }
            }
        }

        private static void RegisterNewCustomer()
        {
            Console.Clear();
            Console.WriteLine("Register New Customer:");

            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            Console.WriteLine("Choose your customer type:");
            Console.WriteLine("1. Gold");
            Console.WriteLine("2. Silver");
            Console.WriteLine("3. Bronze");
            string customerTypeChoice = Console.ReadLine();

            Customer newCustomer;
            switch (customerTypeChoice)
            {
                case "1":
                    newCustomer = new GoldCustomer(name, password);
                    break;
                case "2":
                    newCustomer = new SilverCustomer(name, password);
                    break;
                case "3":
                    newCustomer = new BronzeCustomer(name, password);
                    break;
                default:
                    Console.WriteLine("Invalid choice! Defaulting to Bronze.");
                    newCustomer = new BronzeCustomer(name, password);
                    break;
            }

            // Add the new customer to the customers list
            customers.Add(newCustomer);

            Console.WriteLine($"Thank you {name}! You have been registered as a {newCustomer.GetType().Name} customer.");
            Console.WriteLine("Press any key to go back to the main menu.");
            Console.ReadKey();
        }


        private static void DisplayAvailableClothes(Customer customer)
        {
            Console.WriteLine("Available clothes:");
            for (int i = 0; i < clothingStock.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {clothingStock[i].Name} - {clothingStock[i].Price}kr");
            }
            Console.WriteLine("Enter the number of the item you want to add to your cart, or 0 to go back.");
            int choice;
            if (int.TryParse(Console.ReadLine(), out choice) && choice > 0 && choice <= clothingStock.Count)
            {
                customer.AddToCart(clothingStock[choice - 1]);
                Console.WriteLine($"{clothingStock[choice - 1].Name} has been added to your cart!");
            }
            else if (choice == 0)
            {
                Console.WriteLine("Going back to the previous menu...");
            }
            else
            {
                Console.WriteLine("Invalid choice! Please select a valid product number.");
            }
        }

        private static void ViewCart(Customer customer)
        {
            if (customer.Cart.Count == 0)
            {
                Console.WriteLine("Your cart is empty!");
                return;
            }
            else
            {
                Console.WriteLine("Items in your cart:");
                double totalCost = 0;
                foreach (var item in customer.Cart)
                {
                    Console.WriteLine($"{item.Name} - {item.Price}kr");
                    totalCost += item.Price;
                }
                Console.WriteLine($"Total: {totalCost}kr");
                Console.WriteLine("Press 'c' to checkout, or any other key to go back.");
                char choice = Console.ReadKey().KeyChar;
                if (choice == 'c' || choice == 'C')
                {
                    Console.WriteLine("Checking out...");
                    customer.Cart.Clear();
                    Console.WriteLine("Your items have been purchased! Thank you for shopping with us.");
                }
            }
        }

        private static void SwitchCurrency(Customer customer)
        {
            Console.WriteLine("Available currencies:");
            Console.WriteLine("1. SEK (Default)");
            Console.WriteLine("2. USD");
            Console.WriteLine("3. EUR");
            Console.WriteLine("Enter your choice:");
            string choice = Console.ReadLine();
            Currency selectedCurrency;

            switch (choice)
            {
                case "1":
                    selectedCurrency = Currency.SEK;
                    break;
                case "2":
                    selectedCurrency = Currency.USD;
                    break;
                case "3":
                    selectedCurrency = Currency.EUR;
                    break;
                default:
                    Console.WriteLine("Invalid option! Using default currency (SEK).");
                    selectedCurrency = Currency.SEK;
                    break;
            }
            customer.SelectedCurrency = selectedCurrency;
            Console.WriteLine($"Currency set to {selectedCurrency}.");
        }

        enum Currency
        {
            SEK,
            USD,
            EUR
        }

        abstract class Customer
        {
            public string Name { get; }
            private string Password { get; }
            public List<Product> Cart { get; private set; }
            public Currency SelectedCurrency { get; set; }

            public Customer(string name, string password)
            {
                Name = name;
                Password = password;
                Cart = new List<Product>();
                SelectedCurrency = Currency.SEK;
            }

            public bool ConfirmPassword(string input)
            {
                return Password == input;
            }

            public void AddToCart(Product product)
            {
                Cart.Add(product);
            }

            public void ViewCart()
            {
                if (Cart.Count == 0)
                {
                    Console.WriteLine("Your cart is empty!");
                    return;
                }

                Console.WriteLine("\nItems in your cart:");
                double totalCost = 0;
                for (int i = 0; i < Cart.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {Cart[i].Name} - {Cart[i].Price} {SelectedCurrency}");
                    totalCost += Cart[i].Price;
                }
                Console.WriteLine($"Total: {totalCost} {SelectedCurrency}");
            }

            public void RemoveFromCart(int index)
            {
                if (index >= 0 && index < Cart.Count)
                {
                    Console.WriteLine($"{Cart[index].Name} has been removed from your cart!");
                    Cart.RemoveAt(index);
                }
                else
                {
                    Console.WriteLine("Invalid index!");
                }
            }

            public abstract void Checkout();
        }

        class GoldCustomer : Customer
        {
            public GoldCustomer(string name, string password) : base(name, password) { }

            public override void Checkout()
            {
                if (Cart.Count == 0)
                {
                    Console.WriteLine("Your cart is empty! Cannot checkout.");
                    return;
                }

                double discount = 0.20;
                double totalCost = 0;

                Console.WriteLine("\nItems in your cart:");
                foreach (var item in Cart)
                {
                    Console.WriteLine($"{item.Name} - {item.Price} {SelectedCurrency}");
                    totalCost += item.Price;
                }

                double discountAmount = totalCost * discount;
                double finalAmount = totalCost - discountAmount;

                Console.WriteLine($"Total: {totalCost} {SelectedCurrency}");
                Console.WriteLine($"Discount (20%): -{discountAmount} {SelectedCurrency}");
                Console.WriteLine($"Amount to pay: {finalAmount} {SelectedCurrency}");

                Console.WriteLine("Press 'c' to confirm purchase, or any other key to cancel.");
                char choice = Console.ReadKey().KeyChar;

                if (choice == 'c' || choice == 'C')
                {
                    Cart.Clear();
                    Console.WriteLine("\nYour items have been purchased! Thank you for shopping with us.");
                }
                else
                {
                    Console.WriteLine("\nPurchase cancelled.");
                }
            }
        }

        class SilverCustomer : Customer
        {
            public SilverCustomer(string name, string password) : base(name, password) { }

            public override void Checkout()
            {
                if (Cart.Count == 0)
                {
                    Console.WriteLine("Your cart is empty! Cannot checkout.");
                    return;
                }

                double discount = 0.10;
                double totalCost = 0;

                Console.WriteLine("\nItems in your cart:");
                foreach (var item in Cart)
                {
                    Console.WriteLine($"{item.Name} - {item.Price} {SelectedCurrency}");
                    totalCost += item.Price;
                }

                double discountAmount = totalCost * discount;
                double finalAmount = totalCost - discountAmount;

                Console.WriteLine($"Total: {totalCost} {SelectedCurrency}");
                Console.WriteLine($"Discount (10%): -{discountAmount} {SelectedCurrency}");
                Console.WriteLine($"Amount to pay: {finalAmount} {SelectedCurrency}");

                Console.WriteLine("Press 'c' to confirm purchase, or any other key to cancel.");
                char choice = Console.ReadKey().KeyChar;

                if (choice == 'c' || choice == 'C')
                {
                    Cart.Clear();
                    Console.WriteLine("\nYour items have been purchased! Thank you for shopping with us.");
                }
                else
                {
                    Console.WriteLine("\nPurchase cancelled.");
                }
            }
        }

        class BronzeCustomer : Customer
        {
            public BronzeCustomer(string name, string password) : base(name, password) { }

            public override void Checkout()
            {
                if (Cart.Count == 0)
                {
                    Console.WriteLine("Your cart is empty! Cannot checkout.");
                    return;
                }

                double totalCost = 0;

                Console.WriteLine("\nItems in your cart:");
                foreach (var item in Cart)
                {
                    Console.WriteLine($"{item.Name} - {item.Price} {SelectedCurrency}");
                    totalCost += item.Price;
                }

                Console.WriteLine($"Amount to pay: {totalCost} {SelectedCurrency}");

                Console.WriteLine("Press 'c' to confirm purchase, or any other key to cancel.");
                char choice = Console.ReadKey().KeyChar;

                if (choice == 'c' || choice == 'C')
                {
                    Cart.Clear();
                    Console.WriteLine("\nYour items have been purchased! Thank you for shopping with us.");
                }
                else
                {
                    Console.WriteLine("\nPurchase cancelled.");
                }
            }
        }

        
    }
}
