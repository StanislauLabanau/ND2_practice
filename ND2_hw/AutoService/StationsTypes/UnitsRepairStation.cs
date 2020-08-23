using AutoServices.Interfaces;
using AutoServices.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoServices.Types
{
    public class UnitsRepairStation : Station
    {
        const string OutOfExperticeAreaMessage = "Unfortunately we don't work whith such engine type or car brand";
        public string[] EngineTypes { get; set; }
        public string[] CarBrands { get; set; }

        public UnitsRepairStation(string name, Action<string> write) : base(name, write)
        {
            Category = "Units";

            Operations = new List<IOperation> { };
            Operations.Add(new Operation("Change motor oil", 100.0));
            Operations.Add(new Operation("Change air filter", 30.0));
            Operations.Add(new Operation("Engine repair", 1000.0));
            Operations.Add(new Operation("Gearbox repair", 500.0));
            Operations.Add(new Operation("Chassis repair", 200.0));

            EngineTypes = new string[]
            {
                "Petrol",
                "Diesel"
            };

            CarBrands = new string[]
            {
                "Citroen",
                "Peugeot",
                "Renault"
            };
        }

        private bool CheckExperticeArea(OrderForm orderForm)
        {
            bool flagEngine = default;
            bool flagBrand = default;

            for (int i = 0; i < EngineTypes.Length; i++)
            {
                if (EngineTypes[i].Equals(orderForm.Car.EngineType, StringComparison.InvariantCultureIgnoreCase))
                {
                    flagEngine = true;
                }
            }

            for (int i = 0; i < CarBrands.Length; i++)
            {
                if (CarBrands[i].Equals(orderForm.Car.Brand, StringComparison.InvariantCultureIgnoreCase))
                {
                    flagBrand = true;
                }
            }

            if (flagEngine && flagBrand)
            {
                return true;
            }
            else
                return false;
        }

        public override void ShowTotalPrice(OrderForm orderForm)
        {
            if (CheckExperticeArea(orderForm))
            {
                base.ShowTotalPrice(orderForm);
            }
            else
            {
                Write($"({Name}) {OutOfExperticeAreaMessage}");
            }
        }
    }
}
