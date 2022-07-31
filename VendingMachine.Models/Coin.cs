using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Shared;
using VendingMachine.Shared.Validation;

namespace VendingMachine.Models
{
    public class Coin
    {
        public int Id { get; set; }

        public int DepositId { get; set; }

        [EnumDataType(typeof(CoinTypeEnum))]
        public CoinTypeEnum CoinType { get; set; }

        [IntGreaterThanOrEqualZero]
        public int Count { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
