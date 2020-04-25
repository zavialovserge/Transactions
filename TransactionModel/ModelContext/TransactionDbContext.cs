using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionModel.ModelContext
{
    public class TransactionDbContext:DbContext
    {
        public TransactionDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                        .Property(p => p.Amount).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Currency>()
                       .HasKey(u => new { u.CurrencyName, u.CurrencyDate });

            modelBuilder.Entity<Currency>()
                       .Property(p => p.Amount).HasColumnType("decimal(9,2)");

        }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
    }
}
