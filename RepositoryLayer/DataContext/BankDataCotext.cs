using Microsoft.EntityFrameworkCore;
using static RepositoryLayer.DatabaseModels.BankSP;

// database connection in RL & Mapping Stored Procedure Results
namespace RepositoryLayer.DataContext
{
    public class BankDataCotext : DbContext
    {
        public BankDataCotext(DbContextOptions<BankDataCotext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GetAllBankDetailsResult>().HasNoKey().ToView(null);
            modelBuilder.Entity<GetAllAccountHoldersResult>().HasNoKey().ToView(null);
        }
    }
}
