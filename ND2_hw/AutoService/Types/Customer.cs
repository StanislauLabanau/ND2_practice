using System;
using System.Collections.Generic;
using System.Text;

namespace AutoServiceLib.Types
{
    public class Customer
    {
        public string FirstName { get; private set; }
        public string SecondName { get; private set; }

        public Customer(string firstName, string secondName)
        {
            FirstName = firstName;
            SecondName = secondName;
        }
    }
}
