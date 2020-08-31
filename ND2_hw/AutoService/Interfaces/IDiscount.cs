using AutoServiceLib.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoServiceLib.Interfaces
{
    public interface IDiscount
    {
        public List<Customer> GoldenCustomers { get; set; }
        public decimal GetCalculatedDiscount(decimal total, Customer customer);
    }
}
