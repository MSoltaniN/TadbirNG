using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Metadata;

namespace SPPC.Tadbir.Persistence.Seeding
{

    internal class SysOperationConfiguration : IEntityTypeConfiguration<Operation>
    {
        public void Configure(EntityTypeBuilder<Operation> builder)
        {

            builder.HasData(new Operation { Id = 1  ,Name =  "View",  Description = "" });
            builder.HasData(new Operation { Id = 2  ,Name =  "Create",  Description = "" });
            builder.HasData(new Operation { Id = 3  ,Name =  "Edit",  Description = "" });
            builder.HasData(new Operation { Id = 4  ,Name =  "Delete",  Description = "" });
            builder.HasData(new Operation { Id = 5  ,Name =  "Filter",  Description = "" });
            builder.HasData(new Operation { Id = 6  ,Name =  "Print",  Description = "" });
            builder.HasData(new Operation { Id = 7  ,Name =  "Save",  Description = "" });
            builder.HasData(new Operation { Id = 8  ,Name =  "Archive",  Description = "" });
            builder.HasData(new Operation { Id = 10 ,Name = "Design",  Description = "" });
            builder.HasData(new Operation { Id = 21 ,Name = "GroupDelete",  Description = "" });
            builder.HasData(new Operation { Id = 22 ,Name = "FailedLogin",  Description = "" });
            builder.HasData(new Operation { Id = 23 ,Name = "CompanyLogin",  Description = "" });
            builder.HasData(new Operation { Id = 24 ,Name = "SwitchFiscalPeriod",  Description = "" });
            builder.HasData(new Operation { Id = 25 ,Name = "SwitchBranch",  Description = "" });
            builder.HasData(new Operation { Id = 26 ,Name = "AssignRole",  Description = "" });
            builder.HasData(new Operation { Id = 27 ,Name = "AssignUser",  Description = "" });
            builder.HasData(new Operation { Id = 28 ,Name = "BranchAccess",  Description = "" });
            builder.HasData(new Operation { Id = 29 ,Name = "FiscalPeriodAccess",  Description = "" });
            builder.HasData(new Operation { Id = 30 ,Name = "ViewArchive",  Description = "" });
            builder.HasData(new Operation { Id = 35 ,Name = "RoleAccess",  Description = "" });
            builder.HasData(new Operation { Id = 54 ,Name = "Export",  Description = "" });
            builder.HasData(new Operation { Id = 57 ,Name = "CompanyAccess",  Description = "" });
            builder.HasData(new Operation { Id = 58 ,Name = "PrintPreview",  Description = "" });
            
        }

    }
}