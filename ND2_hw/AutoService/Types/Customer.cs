using AutoServiceLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoServiceLib.Types
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public Membership Membership = new Membership { Title = "No membership", DiscountValue = 0m };
    }
}
