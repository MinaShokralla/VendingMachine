using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Shared.Validation
{
    public class ActualDepositCent : ValidationAttribute
    {
        private static List<int> LstOfCoinsAvailable = new List<int> { 5, 10, 20, 50, 100 };


        public override bool IsValid(object? value)
        {
            int i;
            return value != null &&
                   int.TryParse(value.ToString(), out i) &&
                   LstOfCoinsAvailable.Any(c => c == i);
        }
    }
}
