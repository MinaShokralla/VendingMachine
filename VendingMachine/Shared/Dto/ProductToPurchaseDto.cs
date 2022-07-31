using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Shared.Validation;

namespace VendingMachine.Shared.Dto
{
    public class ProductToPurchaseDto
    {
        public int productId { get; set; }

        [IntGreaterThanZero(ErrorMessage = "Amount of the product have to be > 0")]
        public int amountOfProduct { get; set; }
    }
}
