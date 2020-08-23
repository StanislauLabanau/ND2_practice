using AutoServices.Types;
using System;

namespace TestAutoServices
{
    class Program
    {
        static void Main(string[] args)
        {
            BodyRepairStation bodyRepairStation = new BodyRepairStation("PaintAA", ShowOnConsole);
            UnitsRepairStation unitRepairStation = new UnitsRepairStation("PSA", ShowOnConsole);
            TireFittingStation tireFittingStation = new TireFittingStation("Grip", ShowOnConsole);


            OrderForm orderFormSmith = new OrderForm(new Customer("George Smith", 0.95), new Car("Renault", "Petrol"), new string[] { "Engine repair", "Chassis repair", "Wash" });
            OrderForm orderFormBlack = new OrderForm(new Customer("Joe Black", 1), new Car("Renault", "Electro"), new string[] { "Engine repair",  "4 wheel balansing"});

            bodyRepairStation.ShowTotalPrice(orderFormSmith);
            unitRepairStation.ShowTotalPrice(orderFormSmith);

            tireFittingStation.ShowTotalPrice(orderFormBlack);
            unitRepairStation.ShowTotalPrice(orderFormBlack);

            Console.Read();
        }

        static void ShowOnConsole(string mess)
        {
            Console.WriteLine(mess);
        }
    }
}
