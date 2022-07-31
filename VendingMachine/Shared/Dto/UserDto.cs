using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Shared.Validation;

namespace VendingMachine.Shared.Dto
{
    public class UserDto
    {
        [UsernameValidation, StringLength(50)]
        public string Username { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;


        public DepositDto? Deposit { get; set; }

        [Required, StringLength(50)]
        public string Role { get; set; } = string.Empty;
    }
}
