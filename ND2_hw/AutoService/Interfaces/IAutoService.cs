using AutoServices.Types;
using System;
using System.Collections.Generic;

namespace AutoServices.Interfaces
{
    interface IAutoService
    {
        public Action<string> Write { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public List<IOperation> Operations { get; set; }

        public void ShowTotalPrice(OrderForm orderForm, Action<string> write)
        {
        }
    }
}
