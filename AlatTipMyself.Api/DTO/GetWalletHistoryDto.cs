using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.DTO
{
    public class GetWalletHistoryDto
    {
        public decimal TransactionAmount { get; set; }
        public int TipPercent { get; set; }
        public decimal TipAmount { get; set; }
        public DateTime Date { get; set; }
    }
}
