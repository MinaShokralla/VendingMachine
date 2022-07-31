using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VendingMachine.Shared.Validation
{
    public class PasswordComplexityValidation : ValidationAttribute
    {


        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null && Regex.IsMatch(value.ToString()!, @"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{8,40})"))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Password should meet compelixty min.chars 8 and to contain one digit, lowercase letter, uppercase letter & special character");
        }

    }
}
