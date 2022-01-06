using AlatTipMyself.Api.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.DTO
{
    public class WalletCreationDto
    {
        public bool TipStatus { get; set; } = false;

        public string TipPercent { get; set; }
    }
}
