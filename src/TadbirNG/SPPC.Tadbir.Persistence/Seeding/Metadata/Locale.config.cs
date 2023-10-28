using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Metadata;

namespace SPPC.Tadbir.Persistence.Seeding
{

    internal class LocaleConfiguration : IEntityTypeConfiguration<Locale>
    {
        public void Configure(EntityTypeBuilder<Locale> builder)
        {

            builder.HasData(new Locale { Id = 1  ,Name =  "English" ,LocalName =   "English" ,Code = "en" });
            builder.HasData(new Locale { Id = 2  ,Name =  "Persian" , LocalName = "فارسی", Code =  "fa"});
            builder.HasData(new Locale { Id = 3  ,Name =  "Arabic"  , LocalName = "العربیه", Code =  "ar" });
            builder.HasData(new Locale { Id = 4  ,Name =  "French"  , LocalName = "Français", Code =  "fr" } );

        }

    }
}