using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Shared.Validation;

namespace VendingMachine.Shared.Dto
{
    public class UserChangePasswordDto
    {
        [PasswordComplexityValidation]
        [StringLength(40, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;

        [PasswordComplexityValidation]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
