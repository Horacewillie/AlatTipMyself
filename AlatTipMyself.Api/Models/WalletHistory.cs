using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.Models
{
    public class WalletHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WalletHistoryId { get; set; }

        [ForeignKey("AcctNumber")]
        public UserDetail UserDetail { get; set; }
        public string AcctNumber { get; set; }

        [ForeignKey("WalletId")]
        public Wallet Wallet { get; set; }
        public int WalletId { get; set; }

        public decimal TransactionAmount { get; set; }

        public int TipPercent { get; set; }
        public decimal TipAmount { get; set; }

        public DateTime Date { get; set; }
    }
}
