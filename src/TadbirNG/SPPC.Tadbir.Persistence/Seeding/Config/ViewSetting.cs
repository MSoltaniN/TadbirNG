using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Config;

namespace SPPC.Tadbir.Persistence.Seeding
{

    internal class ViewSettingConfiguration : IEntityTypeConfiguration<ViewSetting>
    {
        public void Configure(EntityTypeBuilder<ViewSetting> builder)
        {

            builder.HasData(new ViewSetting
            {
                Id = 1,
                SettingId = 5,
                ViewId = 1,
                ModelType = "ViewTreeConfig",
                Values = @"{""viewId"":1,""maxDepth"":3,""levels"":[{""no"":1,""name"":""LevelGeneral"",""codeLength"":3,""isEnabled"": true,""isUsed"":true},{""no"":2,""name"":""LevelAuxiliary"",""codeLength"":3,""isEnabled"": true,""isUsed"":true},{""no"":3,""name"":""LevelDetail"",""codeLength"":4,""isEnabled"": true,""isUsed"":true},{""no"":4,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":5,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":6,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":7,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":8,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":9,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":10,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":11,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":12,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":13,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":14,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":15,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":16,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false}]}",
                DefaultValues = @"{""viewId"":1,""maxDepth"":3,""levels"":[{""no"":1,""name"":""LevelGeneral"",""codeLength"":3,""isEnabled"": true,""isUsed"":true},{""no"":2,""name"":""LevelAuxiliary"",""codeLength"":3,""isEnabled"": true,""isUsed"":true},{""no"":3,""name"":""LevelDetail"",""codeLength"":4,""isEnabled"": true,""isUsed"":true},{""no"":4,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":5,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":6,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":7,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":8,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":9,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":10,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":11,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":12,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":13,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":14,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":15,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":16,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false}]}",
            });

            builder.HasData(new ViewSetting
            {
                Id = 2,
                SettingId = 5,
                ViewId = 6,
                ModelType = "ViewTreeConfig",
                Values = @"{""viewId"":6,""maxDepth"":4,""levels"":[{""no"":1,""name"":""LevelX"",""codeLength"":4,""isEnabled"":true,""isUsed"":false},{""no"":2,""name"":""LevelX"",""codeLength"":4,""isEnabled"":true,""isUsed"":false},{""no"":3,""name"":""LevelX"",""codeLength"":4,""isEnabled"":true,""isUsed"":false},{""no"":4,""name"":""LevelX"",""codeLength"":4,""isEnabled"":true,""isUsed"":false},{""no"":5,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":6,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":7,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":8,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":9,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":10,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":11,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":12,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":13,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":14,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":15,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":16,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false}]}",
                DefaultValues = @"{""viewId"":6,""maxDepth"":4,""levels"":[{""no"":1,""name"":""LevelX"",""codeLength"":4,""isEnabled"":true,""isUsed"":false},{""no"":2,""name"":""LevelX"",""codeLength"":4,""isEnabled"":true,""isUsed"":false},{""no"":3,""name"":""LevelX"",""codeLength"":4,""isEnabled"":true,""isUsed"":false},{""no"":4,""name"":""LevelX"",""codeLength"":4,""isEnabled"":true,""isUsed"":false},{""no"":5,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":6,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":7,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":8,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":9,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":10,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":11,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":12,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":13,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":14,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":15,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":16,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false}]}",
            });

            builder.HasData(new ViewSetting
            {
                Id = 3,
                SettingId = 5,
                ViewId = 7,
                ModelType = "ViewTreeConfig",
                Values = @"{""viewId"":7,""maxDepth"":4,""levels"":[{""no"":1,""name"":""LevelX"",""codeLength"":4,""isEnabled"":true,""isUsed"":false},{""no"":2,""name"":""LevelX"",""codeLength"":4,""isEnabled"":true,""isUsed"":false},{""no"":3,""name"":""LevelX"",""codeLength"":4,""isEnabled"":true,""isUsed"":false},{""no"":4,""name"":""LevelX"",""codeLength"":4,""isEnabled"":true,""isUsed"":false},{""no"":5,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":6,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":7,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":8,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":9,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":10,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":11,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":12,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":13,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":14,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":15,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":16,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false}]}",
                DefaultValues = @"{""viewId"":7,""maxDepth"":4,""levels"":[{""no"":1,""name"":""LevelX"",""codeLength"":4,""isEnabled"":true,""isUsed"":false},{""no"":2,""name"":""LevelX"",""codeLength"":4,""isEnabled"":true,""isUsed"":false},{""no"":3,""name"":""LevelX"",""codeLength"":4,""isEnabled"":true,""isUsed"":false},{""no"":4,""name"":""LevelX"",""codeLength"":4,""isEnabled"":true,""isUsed"":false},{""no"":5,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":6,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":7,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":8,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":9,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":10,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":11,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":12,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":13,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":14,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":15,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":16,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false}]}",
            });

            builder.HasData(new ViewSetting
            {
                Id = 4,
                SettingId = 5,
                ViewId = 8,
                ModelType = "ViewTreeConfig",
                Values = @"{""viewId"":8,""maxDepth"":4,""levels"":[{""no"":1,""name"":""LevelX"",""codeLength"":4,""isEnabled"":true,""isUsed"":false},{""no"":2,""name"":""LevelX"",""codeLength"":4,""isEnabled"":true,""isUsed"":false},{""no"":3,""name"":""LevelX"",""codeLength"":4,""isEnabled"":true,""isUsed"":false},{""no"":4,""name"":""LevelX"",""codeLength"":4,""isEnabled"":true,""isUsed"":false},{""no"":5,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":6,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":7,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":8,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":9,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":10,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":11,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":12,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":13,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":14,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":15,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":16,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false}]}",
                DefaultValues = @"{""viewId"":8,""maxDepth"":4,""levels"":[{""no"":1,""name"":""LevelX"",""codeLength"":4,""isEnabled"":true,""isUsed"":false},{""no"":2,""name"":""LevelX"",""codeLength"":4,""isEnabled"":true,""isUsed"":false},{""no"":3,""name"":""LevelX"",""codeLength"":4,""isEnabled"":true,""isUsed"":false},{""no"":4,""name"":""LevelX"",""codeLength"":4,""isEnabled"":true,""isUsed"":false},{""no"":5,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":6,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":7,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":8,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":9,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":10,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":11,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":12,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":13,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":14,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":15,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false},{""no"":16,""name"":""LevelX"",""codeLength"":4,""isEnabled"":false,""isUsed"":false}]}",
            });
        }

    }
}