using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Shared.Dto
{
    public class DepositDto
    {
        public int Id { get; set; }
        public UserDto User { get; set; } = new();


        public ICollection<CoinDto>? Coins { get; set; }
    }
}
