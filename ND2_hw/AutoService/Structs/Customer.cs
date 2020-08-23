using System;
using System.Collections.Generic;
using System.Text;

namespace AutoServices.Types
{
    public struct Customer
    {
        public string Name { get; private set; }
        public double Bonus { get; private set; }

        public Customer(string name, double bonus)
        {
            Name = name;
            Bonus = bonus;
        }
    }
}
