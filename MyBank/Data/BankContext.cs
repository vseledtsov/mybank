using Microsoft.EntityFrameworkCore;
using MyBank.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBank.Data
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options)
       : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transfer>()
                .HasOne(x => x.Recipient)
                .WithMany(x => x.Incoming)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Transfer>()
             .HasOne(x => x.Sender)
             .WithMany(x => x.Outgoing)
             .OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
    }
}
