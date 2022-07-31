using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Shared.Validation;

namespace VendingMachine.Shared.Dto
{
    public class ProductPurchasedDto
    {
        [Required, StringLength(100)]
        public string ProductName { get; set; } = string.Empty;

        [IntGreaterThanOrEqualZero]
        public int AmountAvailable { get; set; }

        [DoubleGreaterThanOrEqualZero]
        public double Cost { get; set; }
    }
}
