using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Shared.Validation;

namespace VendingMachine.Shared.Dto
{
    public class UserBuyerDto
    {
        [UsernameValidation, StringLength(50)]
        public string Username { get; set; } = string.Empty;
        [RequiredDepositGreaterThanZero]
        public DepositDto Deposit { get; set; }
    }
}
