using BankingSolution.Domain;
using Microsoft.EntityFrameworkCore;

namespace BankingSolution.Repository
{
    public class BankingContext : DbContext
    {
        public BankingContext(DbContextOptions<BankingContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "Banking");
        }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<BankAccountOwner> AccountOwners { get; set; }

        public DbSet<BankTransaction> Transactions { get; set; }
    }
}
