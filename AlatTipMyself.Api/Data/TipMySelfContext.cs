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

        public TipMySelfContext(DbContextOptions<TipMySelfContext> options) : base(options)
        {

        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<UserDetail>()
        //    .HasOne(x => x.Wallet)
        //    .WithOne(y => y.UserDetail)
        //    .HasPrincipalKey<UserDetail>(x => x.AcctNumber)
        //    .HasForeignKey<Wallet>(x => x.AcctNumber);

        //    modelBuilder.Entity<WalletHistory>()
        //    .HasOne(x => x.UserDetail)
        //    .WithMany(x => x.WalletHistory)
        //    .HasForeignKey(x => x.AcctNumber)
        //    .HasPrincipalKey(x => x.AcctNumber);
        //}
        public DbSet<UserDetail> UserDetails { get; set; }

        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletHistory> WalletHistories { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }


    }
}
