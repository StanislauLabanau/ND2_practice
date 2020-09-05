using AutoServiceLib.Types;
using System;
using System.Collections.Generic;

namespace AutoServiceLib.Interfaces
{
    interface IAutoService
    {
        public string Name { get; set; }
        public IDiscount Discount { get; set; }
        public List<IOperation> Operations { get; set; }

        public decimal GetTotalPrice(OrderForm orderForm);
    }
}
