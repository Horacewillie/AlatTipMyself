using AlatTipMyself.Api.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.Models
{
    public class Wallet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WalletId { get; set; }

        
        public UserDetail UserDetail { get; set; }
        [Required]
        public string AcctNumber { get; set; }

        public decimal WalletBalance { get; set; } = 0;
        public bool TipStatus { get; set; } = false;

        public string TipPercent { get; set; }
    }
}
