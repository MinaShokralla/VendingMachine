using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Shared.Validation;

namespace VendingMachine.Shared.Dto
{
    public class UserForProductDto
    {
        public int Id { get; set; }
        [UsernameValidation]
        public string Username { get; set; } = string.Empty;
    }
}
