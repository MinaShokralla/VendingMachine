using System.ComponentModel.DataAnnotations;
using VendingMachine.Shared.Validation;

namespace VendingMachine.Models
{
    public class Product
    {       
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string ProductName { get; set; } = string.Empty;
        [IntGreaterThanOrEqualZero]
        public int AmountAvailable { get; set; }
        [DoubleGreaterThanOrEqualZero]
        public double Cost { get; set; }



        public User Seller { get; set; } = new();
        public int SellerId { get; set; }



        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}