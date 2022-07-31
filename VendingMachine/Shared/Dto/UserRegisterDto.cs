using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Shared.Validation;

namespace VendingMachine.Shared.Dto
{
    public class UserRegisterDto
    {
        [UsernameValidation, StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [PasswordComplexityValidation]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Compare(nameof(Password), ErrorMessage = "The passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;
        [EnumDataType(typeof(RoleEnum))]
        public RoleEnum Role { get; set; }
    }
}
