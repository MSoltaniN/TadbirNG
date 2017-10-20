
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.DataAccess
{
    public partial class AccountDBContext : DbContext,IDisposable
    {
        public virtual DbSet<AccountViewModel> AccountViewModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Data Source=ASUS-PC;Initial Catalog=TadbirDemo;Persist Security Info=True;User ID=sa;Password=123456aA;Enlist=False;Network Library=dbmssocn;Application Name=Pafco MIS Systemdemo1;Connect Timeout=120;MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountViewModel>().ToTable("Account","Finance");

           
        }
    }
}
