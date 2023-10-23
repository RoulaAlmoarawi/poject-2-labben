using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poject_2_labben
{
    internal class SilverCustomer : Customer
    {
        public SilverCustomer(string name, string password) : base(name, password, CustomerLevel.SilverCustomer )
        {
        }

        public override double GetDiscount()
        {
            return 0.10;

        }

    }
}
