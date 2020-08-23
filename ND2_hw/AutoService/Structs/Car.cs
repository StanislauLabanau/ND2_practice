using System;
using System.Collections.Generic;
using System.Text;

namespace AutoServices.Types
{
    public struct Car
    {
        public string Brand { get; private set; }
        public string EngineType { get; private set; }

        public Car(string brand, string engine)
        {
            Brand = brand;
            EngineType = engine;
        }
    }
}
