using System;
using System.Collections.Generic;
using System.Text;

namespace AutoServices.Interfaces
{
    public interface IOperation
    {
        public string Title { get; set; }
        public double Price { get; set; }
    }
}
