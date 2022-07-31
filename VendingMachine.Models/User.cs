using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Shared.Validation;

namespace VendingMachine.Models
{
    public class User
    {
        public int Id { get; set; }
        [UsernameValidation, StringLength(50)]
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = default!;
        public byte[] PasswordSalt { get; set; } = default!;
        public DateTime DateCreated { get; set; } = DateTime.Now;


        public Deposit? Deposit { get; set; }

        [Required, StringLength(50)]
        public string Role { get; set; } = string.Empty;


        public virtual ICollection<CustomerProduct>? CustomerProducts { get; set; }
        public virtual ICollection<Product>? Products { get; set; }



        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
