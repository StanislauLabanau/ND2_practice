using AutoServiceLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoServiceLib.Types
{
    public class Operation : IOperation
    {
        public string Section { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
