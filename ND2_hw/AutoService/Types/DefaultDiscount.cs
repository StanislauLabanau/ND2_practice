using AutoServiceLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoServiceLib.Types
{
    public class DefaultDiscount : IDiscount
    {
        public decimal GetCalculatedDiscount(decimal total, Customer customer)
        {
            var discount = customer.Membership.DiscountValue;

            if (total >= 300)
            {
                discount += 5;
            }

            return total * discount / 100;
        }
    }
}
