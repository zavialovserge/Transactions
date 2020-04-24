using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionModel.ModelContext
{
    public class TransactionDbContext:DbContext
    {
        public TransactionDbContext(DbContextOptions options) : base(options) { }// Database.EnsureCreated(); }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                        .Property(p => p.Amount).HasColumnType("decimal(18,2)");

        }
        public virtual DbSet<Transaction> Transactions { get; set; }
    }
}
