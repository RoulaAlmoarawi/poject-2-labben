using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poject_2_labben
{
    internal class GoldCustomer : Customer
    {
        public GoldCustomer(string name, string password) : base(name, password,CustomerLevel.GoldCustomer)
        {
        }

        public override double GetDiscount()
        {
            return 0.15;
        }
    }
    
}
