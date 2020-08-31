using AutoServiceLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoServiceLib.Types
{
    public class AutoService : IAutoService
    {
        public string Name { get; set; }
        public IDiscount Discount { get; private set; }
        public List<IOperation> Operations { get; set; }

        public AutoService(string name, IDiscount discount, List<IOperation> operations)
        {
            Name = name;
            Discount = discount;
            Operations = operations;
        }

        public decimal GetTotalPrice(OrderForm orderForm)
        {
            var result = 0m;

            if (GetExpertiseAreaConfirmation(orderForm))
            {
                var matchedOperations = GetMatchedOperations(orderForm);

                result = matchedOperations.Sum(o => o.Price);
            }

            return result - Discount.GetCalculatedDiscount(result, orderForm.Customer);
        }

        private bool GetExpertiseAreaConfirmation(OrderForm orderForm)
        {
            var result = false;

            for (var i = 0; i < orderForm.Operations.Length; i++)
            {
                if (Operations.Any(p => p.Section.Equals(orderForm.Car.Sections[i], StringComparison.InvariantCultureIgnoreCase)))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        private List<IOperation> GetMatchedOperations(OrderForm orderForm)
        {
            var result = new List<IOperation> { };

            foreach (IOperation operation in Operations)
            {
                for (var i = 0; i < orderForm.Operations.Length; i++)
                {
                    if (operation.Name.Equals(orderForm.Operations[i], StringComparison.InvariantCultureIgnoreCase))
                    {
                        result.Add(operation);
                    }
                }
            }

            return result;
        }
    }
}
