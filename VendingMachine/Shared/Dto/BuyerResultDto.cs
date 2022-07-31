using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Shared.Dto
{
    public class BuyerResultDto
    {
        public int AmountSpend { get; set; }
        public UserDto User { get; set; } = new();
        public ProductPurchasedDto productPurchased { get; set; } = new();

        public bool Succes { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}
