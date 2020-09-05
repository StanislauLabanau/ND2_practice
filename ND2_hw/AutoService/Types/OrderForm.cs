using AutoServiceLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoServiceLib.Types
{
    public class OrderForm
    {
        public Customer Customer { get; set; }
        public Car Car { get; set; }
        public string[] Operations { get; set; }
    }
}
