using AutoServices.Interfaces;
using AutoServices.Structs;
using System;
using System.Collections.Generic;

namespace AutoServices.Types
{
    public class TireFittingStation : Station
    {
        public TireFittingStation(string name, Action<string> write) : base(name, write)
        {
            Category = "Tire";
            Operations = new List<IOperation> { };
            Operations.Add(new Operation("Wheel balansing", 10.0));
            Operations.Add(new Operation("4 wheel balansing", 35.0));
            Operations.Add(new Operation("Puncture repair", 20.0));
            Operations.Add(new Operation("Change tire", 15.0));
            Operations.Add(new Operation("4 tire change", 50.0));
        }
    }
}
