using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.Models
{
    public class UserDetail
    {
        [Key]
        [MaxLength(10)]
        public string AcctNumber { get; set; }
        [Required]
        [MaxLength(50)]
        public string AcctName { get; set; }
        [Required]
        public string Email { get; set; }
        public decimal AcctBalance { get; set; }
    }
}
