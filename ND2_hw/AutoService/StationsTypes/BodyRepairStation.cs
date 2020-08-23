using AutoServices.Interfaces;
using AutoServices.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoServices.Types
{
    public class BodyRepairStation : Station
    {
        public BodyRepairStation(string name, Action<string> write) : base(name, write)
        {
            Category = "Body";

            Operations = new List<IOperation> { };
            Operations.Add(new Operation("Wash", 15.0));
            Operations.Add(new Operation("Polish", 35.0));
            Operations.Add(new Operation("Paintless dent removal", 20.0));
            Operations.Add(new Operation("Dent removal", 20.0));
            Operations.Add(new Operation("Paint element", 20.0));
        }
    }
}

