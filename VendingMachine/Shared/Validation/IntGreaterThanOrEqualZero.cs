using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Shared.Validation
{
    public class IntGreaterThanOrEqualZero : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return value != null && int.TryParse(value.ToString(), out int i) && i >= 0;
        }
    }
}
