using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Shared;
using VendingMachine.Shared.Validation;

namespace VendingMachine.Shared.Dto
{
    public class CoinDto
    {


        [EnumDataType(typeof(CoinTypeEnum))]
        public CoinTypeEnum CoinType { get; set; }

        [IntGreaterThanOrEqualZero]
        public int Count { get; set; }
    }
}
