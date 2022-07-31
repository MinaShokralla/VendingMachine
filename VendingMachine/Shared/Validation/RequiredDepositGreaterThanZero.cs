using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Shared.Validation
{
    public class RequiredDepositGreaterThanZero : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            double i;
            return value != null && double.TryParse(value.ToString(), out i) && i > 0;
        }
    }
}
