using System.ComponentModel.DataAnnotations;

namespace VendingMachine.Shared.Validation
{
    public class IntGreaterThanZero : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return value != null && int.TryParse(value.ToString(), out int i) && i > 0;
        }
    }
}
