using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Config;

namespace SPPC.Tadbir.Persistence.Seeding
{

    internal class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {

            builder.HasData(new Setting
            {
                Id = 1,
                ParentId = null,
                Subsystem = null,
                TitleKey = "AccountRelationsSettings",
                Type = 2,
                ScopeType = 1,
                ModelType = "RelationsConfig",
                IsStandalone = true,
                Values = @"{""useLeafDetails"": true, ""useLeafCostCenters"": true,""useLeafProjects"": true}",
                DefaultValues = @"{""useLeafDetails"": true, ""useLeafCostCenters"": true,""useLeafProjects"": true}",
                DescriptionKey = "AccountRelationsSettingsDescription"
            });

            builder.HasData(new Setting
            {
                Id = 2,
                ParentId = null,
                Subsystem = null,
                TitleKey = "DateRangeFilterSettings",
                Type = 2,
                ScopeType = 0,
                ModelType = "DateRangeConfig",
                IsStandalone = true,
                Values = @"{""defaultDateRange"": ""FiscalStartToFiscalEnd""}",
                DefaultValues = @"{""defaultDateRange"": ""FiscalStartToFiscalEnd""}",
                DescriptionKey = "QuickReportSettingsDescription"
            });

            builder.HasData(new Setting
            {
                Id = 3,
                ParentId = null,
                Subsystem = null,
                TitleKey = "NumberCurrencySettings",
                Type = 2,
                ScopeType = 0,
                ModelType = "NumberDisplayConfig",
                IsStandalone = true,
                Values = @"{""useSeparator"": true, ""separatorMode"": ""UseCustom"", ""separatorSymbol"": "","" , ""decimalPrecision"": 0, ""maxPrecision"": 8}",
                DefaultValues = @"{""useSeparator"": true, ""separatorMode"": ""UseCustom"", ""separatorSymbol"": "", "", ""decimalPrecision"": 0, ""maxPrecision"": 8}",
                DescriptionKey = "NumberCurrencySettingsDescription"
            });

            builder.HasData(new Setting
            {
                Id = 5,
                ParentId = null,
                Subsystem = null,
                TitleKey = "ViewTreeSettings",
                Type = 2,
                ScopeType = 2,
                ModelType = "ViewTreeConfig",
                IsStandalone = false,
                Values = @"{}",
                DefaultValues = @"{}",
                DescriptionKey = "ViewTreeSettingsDescription"
            });

            builder.HasData(new Setting
            {
                Id = 6,
                ParentId = null,
                Subsystem = null,
                TitleKey = "QuickSearchSettings",
                Type = 3,
                ScopeType = 2,
                ModelType = "QuickSearchConfig",
                IsStandalone = false,
                Values = @"{}",
                DefaultValues = @"{}",
                DescriptionKey = "QuickSearchSettingsDescription"
            });

            builder.HasData(new Setting
            {
                Id = 8,
                ParentId = null,
                Subsystem = null,
                TitleKey = "SystemConfigurationSettings",
                Type = 2,
                ScopeType = 1,
                ModelType = "SystemConfig",
                IsStandalone = true,
                Values = @"{""defaultCurrencyNameKey"":""CUnit_IranianRial"",""defaultDecimalCount"":0,""defaultCalendar"":0,""defaultCalendars"": [{""language"":""fa"", ""calendar"":0}, {""language"":""en"", ""calendar"":1}],""usesDefaultCoding"":true,""inventoryMode"": 1}",
                DefaultValues = @"{""defaultCurrencyNameKey"":""CUnit_IranianRial"",""defaultDecimalCount"":0,""defaultCalendar"":0,""defaultCalendars"": [{""language"":""fa"", ""calendar"":0}, {""language"":""en"", ""calendar"":1}],""usesDefaultCoding"":true,""inventoryMode"": 1}",
                DescriptionKey = "SystemConfigurationDescription"
            });

            builder.HasData(new Setting
            {
                Id = 9,
                ParentId = null,
                Subsystem = null,
                TitleKey = "FinanceReportSettings",
                Type = 2,
                ScopeType = 1,
                ModelType = "FinanceReportConfig",
                IsStandalone = true,
                Values = @"{""openingAsFirstVoucher"":false,""startTurnoverAsInitBalance"":false}",
                DefaultValues = @"{""openingAsFirstVoucher"":false,""startTurnoverAsInitBalance"":false}",
                DescriptionKey = "FinanceReportSettingsDescription"
            });

            builder.HasData(new Setting
            {
                Id = 10,
                ParentId = null,
                Subsystem = null,
                TitleKey = "FormLabelSettings",
                Type = 2,
                ScopeType = 3,
                ModelType = "FormLabelConfig",
                IsStandalone = false,
                Values = @"{}",
                DefaultValues = @"{}",
                DescriptionKey = null
            });

            builder.HasData(new Setting
            {
                Id = 11,
                ParentId = null,
                Subsystem = null,
                TitleKey = "UserProfileSettings",
                Type = 3,
                ScopeType = 1,
                ModelType = "UserProfileConfig",
                IsStandalone = false,
                Values = @"{}",
                DefaultValues = @"{}",
                DescriptionKey = "UserProfileSettingsDescription"
            });

            builder.HasData(new Setting
            {
                Id = 12,
                ParentId = null,
                Subsystem = null,
                TitleKey = "ReceiptSettings",
                Type = 2,
                ScopeType = 1,
                ModelType = "ReceiptConfig",
                IsStandalone = true,
                Values = @"{""registerFlowConfig"":{""confirmAfterSave"":true, ""approveAfterConfirm"": true, ""registerAfterApprove"": true},""registerConfig"":{""registerWithLastValidVoucher"": true, ""registerWithNewCreatedVoucher"": false, ""checkedVoucher"": false}}",
                DefaultValues = @"{""registerFlowConfig"":{""confirmAfterSave"":true, ""approveAfterConfirm"": true, ""registerAfterApprove"": true},""registerConfig"":{""registerOnLastValidVoucher"": true, ""registerOnCreatedVoucher"": false, ""checkedVoucher"": false}}",
                DescriptionKey = "ReceiptSettingsDescription"
            });

            builder.HasData(new Setting
            {
                Id = 13,
                ParentId = null,
                Subsystem = null,
                TitleKey = "PaymentSettings",
                Type = 2,
                ScopeType = 1,
                ModelType = "PaymentSettings",
                IsStandalone = true,
                Values = @"{""registerFlowConfig"":{""confirmAfterSave"":true, ""approveAfterConfirm"": true, ""registerAfterApprove"": true},""registerConfig"":{""registerWithLastValidVoucher"": true, ""registerWithNewCreatedVoucher"": false, ""checkedVoucher"": false}}",
                DefaultValues = @"{""registerFlowConfig"":{""confirmAfterSave"":true, ""approveAfterConfirm"": true, ""registerAfterApprove"": true},""registerConfig"":{""registerOnLastValidVoucher"": true, ""registerOnCreatedVoucher"": false, ""checkedVoucher"": false}}",
                DescriptionKey = "PaymentSettingsDescription"
            });

        }

    }
}