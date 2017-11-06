using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SPPC.Tadbir.DataAccess
{
    public partial class AccountDBContext : DbContext, IDisposable
    {
        public virtual DbSet<AccountViewModel> AccountViewModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["TadbirConnectionString"].ConnectionString;

                    optionsBuilder.UseSqlServer(connectionString);
                }
                catch
                {
                    ////TODO: Log error exception
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountViewModel>().ToTable("Account", "Finance");
        }
    }
}
