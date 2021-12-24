﻿using AlatTipMyself.Api.Models;
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
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletHistory> WalletHistories { get; set; }


    }
}
