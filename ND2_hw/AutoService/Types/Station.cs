using AutoServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoServices.Types
{
    public class Station : IAutoService
    {
        const string MismatchMessage = "Unfortunately we can't provide operation";
        const string TotalPriceMessage = "The total price is ";

        public Action<string> Write { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public List<IOperation> Operations { get; set; }

        public Station(string name, Action<string> write)
        {
            Name = name;
            Write = write;
        }

        public virtual void ShowTotalPrice(OrderForm orderForm)
        {
            double result = default;
            bool flag;

            for (int i = 0; i < orderForm.Operations.Length; i++)
            {
                flag = false;

                foreach (IOperation operationStation in Operations)
                {
                    if (orderForm.Operations[i].Equals(operationStation.Title, StringComparison.InvariantCultureIgnoreCase))
                    {
                        flag = true;
                        result += operationStation.Price * orderForm.Customer.Bonus;
                    }
                }

                if (!flag)
                {
                    Write($"({Name}) {MismatchMessage} \"{orderForm.Operations[i]}\"");
                }
            }

            if (result != 0)
            {
                Write($"({Name}) {TotalPriceMessage} {result}");
            }
        }
    }
}
