using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using SPPC.Tadbir.Model.Auth;

namespace SPPC.Tadbir.Persistence.Seeding
{

    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasData(new User { Id = 1  , UserName = "admin", PasswordHash  = "b22f213ec710f0b0e86297d10279d69171f50f01a04edf40f472a563e7ad8576" , LastLoginDate = DateTime.Parse("10/16/2023 8:57:46.003 AM"),IsEnabled = true});

        }

    }
}