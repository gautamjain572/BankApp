using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RepositoryLayer.DatabaseModels.BankSP;

namespace RepositoryLayer.DataContext 
{
    public class BankDataCotext : DbContext
    {
    public BankDataCotext(DbContextOptions<BankDataCotext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GetAllBankDetailsResult>().HasNoKey().ToView(null);

            modelBuilder.Entity<GetAllAccountHoldersResult>().HasNoKey().ToView(null);

        }
    }
}
