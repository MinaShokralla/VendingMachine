using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VendingMachine.Shared.Validation
{
    public class UsernameValidation : ValidationAttribute
    {



        public override bool IsValid(object? value)
        {
            if (value == null) return false;
            return Regex.IsMatch(value.ToString()!, 
                @"^(?=.{3,32}$)(?!.*[._-]{2})(?!.*[0-9]{5,})[a-zA-Z](?:[\w]*|[a-zA-Z\d\.]*|[a-zA-Z\d-]*)[a-zA-Z0-9]$");
        }

    }
}
