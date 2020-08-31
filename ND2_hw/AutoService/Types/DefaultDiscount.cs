using AutoServiceLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoServiceLib.Types
{
    public class DefaultDiscount : IDiscount
    {
        public List<Customer> GoldenCustomers { get; set; }

        public DefaultDiscount(List<Customer> goldenCustomers)
        {
            GoldenCustomers = goldenCustomers;
        }

        public decimal GetCalculatedDiscount(decimal total, Customer customer)
        {
            var discount = 0m;

            if (total >= 300)
            {
                discount += 5;
            }

            foreach (Customer goldenCustomer in GoldenCustomers)
            {
                if (goldenCustomer.FirstName.Equals(customer.FirstName) && goldenCustomer.SecondName.Equals(customer.SecondName))
                {
                    discount += 10;
                }
            }

            return total * discount / 100;
        }
    }
}
