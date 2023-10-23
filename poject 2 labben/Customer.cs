using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poject_2_labben
{
    internal class Customer
    {
        public enum CustomerLevel
        {
            GoldCustomer,
            SilverCustomer,
            BronzeCustomer,

        }

        public string Name { get; private set; }
        public string Password { get; private set; }  
        public CustomerLevel Level { get; private set;}
        public List<Product> Cart { get; private set; } = new List<Product>();
        public CultureInfo  CurrencyCulture { get;  set; } = new CultureInfo ("sv-SE");

        public Customer (string name, string password, CustomerLevel level)
        {
            Name = name;
            Password = PasswordHelper.HashPassword(password);
            Level = level;
            Cart = new List<Product>();
        }
        public override string ToString()
        {
            return $"{Name} - Level:{Level} - Products in shopping Cart: {Cart.Count}";
        }


        public bool ConfirmPassword(string inputpassword)
        {
            return Password == PasswordHelper.HashPassword(inputpassword) ; 
        }

        public void AddToCart(Product product)
        {
            Cart.Add(product);
        }
        public void RemoveFromCart(Product product)
        {
            Cart.Remove(product);
        }

        public void ViewCart()
        {
            Console.WriteLine($"Products in {Name}'s cart:");
            foreach( var product in Cart)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine($"Total (with discount):{CalculateTotalWithDiscount().ToString("c",new CultureInfo("sv-SE"))}");
        }



        public double CalculateTotalWithDiscount()
        {
            double total = Cart.Sum(p => p.Price);
            total *= (1 - GetDiscount());
            return total;
        }
        public virtual double GetDiscount()
        {
            return 0;
        }
        public void CheckOut()
        {
            if (!Cart.Any())
            {
                Console.WriteLine("Your cart is empty. Please add items before checking out.");
                return;
            }

            double total = 0;
            Console.WriteLine("Checking out items:");
            foreach (var item in Cart)
            {
                Console.WriteLine($"{item.Name} - {item.Price.ToString("C", CurrencyCulture)}");
                total += item.Price;
            }

            Console.WriteLine($"Total to be paid: {total.ToString("C", CurrencyCulture)}");
            Cart.Clear();
            Console.WriteLine("Thank you for shopping with us!");
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey();
        }
    }
}
