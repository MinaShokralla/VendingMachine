using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Shared.Dto
{
    public class BuyerUserResultDto
    {
        public DepositDto User { get; set; } = new();

        public int Coin_005cent { get; set; }
        public int Coin_010cent { get; set; }
        public int Coin_020cent { get; set; }
        public int Coin_050cent { get; set; }
        public int Coin_100cent { get; set; }
    }
}
