using CreditoTiendita.Domain.Models;
using CreditoTiendita.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoTiendita.Domain.Persistance.Context
{
    public class AppDbContext:DbContext 
    {
        
        public DbSet<Client> Clients { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountStatus> AccountStatuses { get; set; }
        public DbSet<AditionalCost> AditionalCosts { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<FeeType> FeeTypes { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Client
            builder.Entity<Client>().ToTable("Clients");
            builder.Entity<Client>().HasKey(p => p.Dni);
            builder.Entity<Client>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Client>().Property(p => p.LastName).IsRequired().HasMaxLength(50);
            builder.Entity<Client>().Property(p => p.Dni).IsRequired().HasMaxLength(8);
            builder.Entity<Client>().Property(p => p.PhoneNumber).IsRequired().HasMaxLength(13);
            builder.Entity<Client>().Property(p => p.Address).IsRequired().HasMaxLength(100);
            builder.Entity<Client>().Property(p => p.Birthdate).IsRequired();
            builder.Entity<Client>().Property(p => p.Mail).IsRequired().HasMaxLength(60);
            builder.Entity<Client>().Property(p => p.Password).IsRequired();
            builder.Entity<Client>()
                .HasOne(c => c.Account)
                .WithOne(a => a.Client)
                .HasForeignKey<Account>(a => a.ClientId);

            //Accounts
            builder.Entity<Account>().ToTable("Accounts");
            builder.Entity<Account>().HasKey(a => a.Id);
            builder.Entity<Account>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Account>().Property(a => a.AvailableCredit).IsRequired();
            builder.Entity<Account>()
                .HasMany(a => a.AccountStatuses)
                .WithOne(e => e.Account)
                .HasForeignKey(e => e.AccountId);
            builder.Entity<Account>()
                .HasMany(a => a.Transactions)
                .WithOne(t => t.Account)
                .HasForeignKey(t => t.AccountId);
            builder.Entity<Account>()
                .HasMany(a => a.AditionalCosts)
                .WithOne(ad => ad.Account)
                .HasForeignKey(ad => ad.AccountId);
            builder.Entity<Account>()
                .HasOne(a => a.Currency)
                .WithOne(c => c.Account)
                .HasForeignKey<Currency>(c => c.AccountId);
            builder.Entity<Account>()
                .HasOne(a => a.Fee)
                .WithOne(f => f.Account)
                .HasForeignKey<Fee>(f => f.AccountId);

            //Account Status
            builder.Entity<AccountStatus>().ToTable("AccountStatus");
            builder.Entity<AccountStatus>().HasKey(ac => ac.Id);
            builder.Entity<AccountStatus>().Property(ac => ac.Id).IsRequired().ValueGeneratedOnAdd();


            //Periods
            builder.Entity<Period>().ToTable("Periods");
            builder.Entity<Period>().HasKey(ac => ac.Id);
            builder.Entity<Period>().Property(ac => ac.Id).IsRequired().ValueGeneratedOnAdd();

            //Currency
            builder.Entity<Currency>().ToTable("Currencies");
            builder.Entity<Currency>().HasKey(ac => ac.Id);
            builder.Entity<Currency>().Property(ac => ac.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Currency>().Property(ac => ac.Code).IsConcurrencyToken();

            //Transactions
            builder.Entity<Transaction>().ToTable("Transactions");
            builder.Entity<Transaction>().HasKey(t => t.Id);
            builder.Entity<Transaction>().Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Transaction>().Property(t => t.Date).ValueGeneratedOnAdd();
            builder.Entity<Transaction>().Property(t => t.Description).IsRequired().HasMaxLength(100);
            builder.Entity<Transaction>().Property(t => t.Amount).IsRequired();

            //Transaction type
            builder.Entity<TransactionType>().ToTable("TransactionTypes");
            builder.Entity<TransactionType>().HasKey(t => t.Id);
            builder.Entity<TransactionType>().Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<TransactionType>().Property(t => t.Name).IsRequired().HasMaxLength(50);
            builder.Entity<TransactionType>().Property(t => t.Description).IsRequired().HasMaxLength(50);
            builder.Entity<TransactionType>()
                .HasMany(tt => tt.Transactions)
                .WithOne(t => t.TransactionType)
                .HasForeignKey(t => t.TransactionTypeId);

            //Fee
            builder.Entity<Fee>().ToTable("Fees");
            builder.Entity<Fee>().HasKey(t => t.Id);
            builder.Entity<Fee>().Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Fee>().Property(t => t.Percentage).IsRequired();
            builder.Entity<Fee>().Property(t => t.Date).ValueGeneratedOnAdd();

            //FeeType
            builder.Entity<FeeType>().ToTable("FeeTypes");
            builder.Entity<FeeType>().HasKey(t => t.Id);
            builder.Entity<FeeType>().Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<FeeType>().Property(t => t.Name).IsRequired().HasMaxLength(15);
            builder.Entity<FeeType>()
                .HasMany(ft => ft.Fees)
                .WithOne(f => f.FeeType)
                .HasForeignKey(f => f.FeeTypeId);

            //Period
            builder.Entity<Period>().ToTable("Periods");
            builder.Entity<Period>().HasKey(t => t.Id);
            builder.Entity<Period>().Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Period>().Property(t => t.Type).IsRequired().HasMaxLength(15);
            builder.Entity<Period>()
                .HasMany(ft => ft.Accounts)
                .WithOne(f => f.Period)
                .HasForeignKey(f => f.PeriodId);

            builder.ApplySnakeCaseNamingConvention();
        }
    }
}
