using AutoServiceLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoServiceLib.Types
{
    public class Car
    {
        public string Model { get; set; }
        public string VIN { get; set; }
        public string[] Sections { get; set; }
    }
}
