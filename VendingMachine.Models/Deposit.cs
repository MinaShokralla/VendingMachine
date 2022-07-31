using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Models
{
    public class Deposit
    {
        public int Id { get; set; }

        public User User { get; set; } = new();
        public int UserId { get; set; }


        public ICollection<Coin>? Coins { get; set; }



        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
