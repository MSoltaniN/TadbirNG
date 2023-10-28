using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Config;

namespace SPPC.Tadbir.Persistence.Seeding
{

    internal class CompanyDbConfiguration : IEntityTypeConfiguration<CompanyDb>
    {
        public void Configure(EntityTypeBuilder<CompanyDb> builder)
        {

            builder.HasData(new CompanyDb { Id = 1, Name = "‘—ò  ‰„Ê‰Â", DbName = "NGTadbirMG2", Server = "130.185.75.230,49878", UserName = "TadbirUser", Password = "$$$%%%", IsActive = true, Description = "" });

        }

    }
}