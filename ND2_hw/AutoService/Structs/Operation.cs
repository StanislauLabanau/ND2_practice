using AutoServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoServices.Structs
{
    public struct Operation : IOperation
    {
        public string Title { get; set; }
        public double Price { get; set; }

        public Operation(string title, double price)
        {
            Title = title;
            Price = price;
        }
    }
}
