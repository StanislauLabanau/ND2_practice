using AutoServiceLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoServiceLib.Types
{
    public class Car
    {
        public string Model { get; private set; }
        public string VIN { get; private set; }
        public string[] Sections { get; private set; }

        public Car(string model, string vin, string[] sections)
        {
            VIN = vin;
            Model = model;
            Sections = sections;
        }
    }
}
