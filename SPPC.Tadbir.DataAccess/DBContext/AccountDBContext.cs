using Microsoft.EntityFrameworkCore;
using SPPC.Tadbir.ViewModel.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.DataAccess
{
    public partial class AccountDBContext : DbContext
    {
        public virtual DbSet<AccountViewModel> AccountViewModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=ASUS-PC;Database=ContactDB;User Id=sa;Password=123456aA;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountViewModel>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(true);

                entity.Property(e => e.Description)
                    .HasMaxLength(256)
                    .IsUnicode(true);

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BranchId);                    

                entity.Property(e => e.FiscalPeriodId);
                    

            });
        }
    }
}
