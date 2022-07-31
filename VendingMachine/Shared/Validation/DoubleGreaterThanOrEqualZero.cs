using System.ComponentModel.DataAnnotations;

namespace VendingMachine.Shared.Validation
{
    public class DoubleGreaterThanOrEqualZero : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return value != null && double.TryParse(value.ToString(), out double d) && d >= 0;
        }
    }
}
