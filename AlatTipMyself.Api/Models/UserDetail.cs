using AlatTipMyself.Api.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.Models
{
    public class UserDetail
    {
        public UserDetail()
        {
            AcctNumber = HelperMethods.GenerateAccountNumber().Result;
        }
        //[Key]
        //public int Id { get; set; }
        [Key]
        [MaxLength(10)]
        public string AcctNumber { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public decimal AcctBalance { get; set; } = 50000;
        //public Wallet Wallet { get; set; }
        //public List<WalletHistory> WalletHistory { get; set; }
        //public string TransactionPin { get; set; }
        //public string Password { get; set; }

        [JsonIgnore]
        public byte[] PinHash { get; set; }
        [JsonIgnore]
        public byte[] PinSalt { get; set; }
        [JsonIgnore]
        public byte[] PasswordHash { get; set; }
        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }
    }

    
}
