using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Shared.Validation;

namespace VendingMachine.Models
{
    public class CustomerProduct
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        [DoubleGreaterThanOrEqualZero]
        public double Cost { get; set; }

        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int AmountOfProduct { get; set; }
        public DateTime BoughtOn { get; set; } = DateTime.Now;

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
