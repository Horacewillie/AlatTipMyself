using AlatTipMyself.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.Data
{
    public class TipMySelfContext : DbContext
    {
        public TipMySelfContext()
        {
        }
        public static bool isMigration = true;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {           
            if (isMigration)
            {
                optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = AlatTipMySelfDb2; Initial Catalog = AlatTipMySelf2; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            }
        }
        public TipMySelfContext(DbContextOptions<TipMySelfContext> options) : base(options)
        {

        }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletHistory> WalletHistories { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }


    }
}
