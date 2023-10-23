using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poject_2_labben
{
    internal class BronzeCustomer : Customer 
    {
        public BronzeCustomer(string name, string password) : base(name, password, CustomerLevel.BronzeCustomer )
        {
        }

        public override double GetDiscount()
        {
            return 0.05;
        }
    }
}
