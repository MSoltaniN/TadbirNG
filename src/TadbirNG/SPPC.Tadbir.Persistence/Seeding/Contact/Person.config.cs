using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Contact;

namespace SPPC.Tadbir.Persistence.Seeding
{

    internal class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {

            builder.HasData(new Person { Id = 1  ,UserId = 1 ,FullName = "admin" });

        }

    }
}