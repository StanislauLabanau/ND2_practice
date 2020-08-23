using AutoServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoServices.Types
{
    public class OrderForm
    {
        public Customer Customer { get; private set; }
        public Car Car { get; private set; }
        public string[] Operations { get; set; }

        public OrderForm(Customer customer, Car car, string[] operations)
        {
            Customer = customer;
            Car = car;
            Operations = operations;
        }
    }
}
