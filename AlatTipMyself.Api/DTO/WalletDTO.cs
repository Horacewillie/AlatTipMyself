using AlatTipMyself.Api.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.DTO
{
    public class WalletDto
    {
        public int WalletId { get; set; }

        public string AcctNumber { get; set; }

        public decimal WalletBalance { get; set; } = 0;
        public bool TipStatus { get; set; } = false;

        public string TipPercent { get; set; }
    }
}
