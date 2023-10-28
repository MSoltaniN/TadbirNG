using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Config;

namespace SPPC.Tadbir.Persistence.Seeding
{

    internal class SysSettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {

            builder.HasData(new Setting { Id = 4, ParentId = null, Subsystem = null, TitleKey = "ListFormViewSettings", Type = 3, ScopeType = 2, ModelType = "ListFormViewConfig", IsStandalone = true, Values = @"{""pageSize"": 10, ""columnViews"": []}", DefaultValues = @"{""pageSize"": 10, ""columnViews"": []}" ,DescriptionKey = "ListFormViewSettingsDescription" });
            
            builder.HasData(new Setting { Id = 7, ParentId = null, Subsystem = null, TitleKey = "QuickReportSettings", Type = 3, ScopeType = 2, ModelType = "QuickReportConfig", IsStandalone = true, Values = @"{}", DefaultValues = @"{}" , DescriptionKey = "QuickReportSettingsDescription" });

        }

    }
}