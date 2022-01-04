using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlatTipMyself.Api
{
    public class SendMoneyParameter
    {
        public string ToAccount { get; set; }
        public decimal Amount { get; set; }
    }
}
