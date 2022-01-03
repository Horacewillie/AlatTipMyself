using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.Models
{
    [Table("Transactions")]
    public class TransactionHistory
    {
        [Key]
        public int Id { get; set; }
        public string TransactionUniqueReference { get; set; }
        public decimal TransactionAmount { get; set; }
        public TranStatus TransactionStatus { get; set; }
        public bool isSuccessful => TransactionStatus.Equals(TranStatus.Success);
        public string TransactionSourceAccount { get; set; }
        public string TransactionDestinationAccount { get; set; }
        public DateTime TransactionDate { get; set; }

        public TransactionHistory()
        {
            TransactionUniqueReference = Guid.NewGuid().ToString();
        }
    }

    public enum TranStatus
    {
        Failed,
        Success,
        Error
    }
}
