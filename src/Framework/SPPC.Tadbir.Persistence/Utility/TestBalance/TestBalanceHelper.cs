using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.Persistence.Utility
{
    public class TestBalanceHelper : ITestBalanceHelper
    {
        public TestBalanceHelper(IConfigRepository config)
        {
            _config = config;
        }

        public int GetSourceList(TestBalanceFormat format, string itemTypeName)
        {
            string enumStringTemplate = "{0}Balance{1}Column";
            string itemType = (itemTypeName == "Account")
                ? "Test"
                : itemTypeName;
            string formatString = format.ToString();
            string enumValue = null;
            if (formatString.StartsWith("Two"))
            {
                enumValue = String.Format(enumStringTemplate, itemType, 2);
            }
            else if (formatString.StartsWith("Four"))
            {
                enumValue = String.Format(enumStringTemplate, itemType, 4);
            }
            else if (formatString.StartsWith("Six"))
            {
                enumValue = String.Format(enumStringTemplate, itemType, 6);
            }
            else if (formatString.StartsWith("Eight"))
            {
                enumValue = String.Format(enumStringTemplate, itemType, 8);
            }
            else
            {
                enumValue = String.Format(enumStringTemplate, itemType, 10);
            }

            return (int)Enum.Parse(typeof(SourceListId), enumValue);
        }

        public async Task<IEnumerable<TestBalanceModeInfo>> GetLevelBalanceTypesAsync(int viewId)
        {
            var lookup = new List<TestBalanceModeInfo>();
            var fullConfig = await _config.GetViewTreeConfigByViewAsync(viewId);
            var usedLevels = fullConfig.Current
                .Levels
                .Where(level => level.IsEnabled && level.IsUsed)
                .ToList();
            int typeId = 0;
            for (int index = 0; index < usedLevels.Count; index++)
            {
                lookup.Add(new TestBalanceModeInfo()
                {
                    Id = typeId++,
                    Name = usedLevels[index].Name,
                    Level = usedLevels[index].No,
                    IsDetail = false
                });
            }

            return lookup;
        }

        public async Task<IEnumerable<TestBalanceModeInfo>> GetChildBalanceTypesAsync(int viewId)
        {
            var lookup = new List<TestBalanceModeInfo>();
            var fullConfig = await _config.GetViewTreeConfigByViewAsync(viewId);
            var usedLevels = fullConfig.Current
                .Levels
                .Where(level => level.IsEnabled && level.IsUsed)
                .ToList();
            int typeId = usedLevels.Count;
            for (int index = 0; index < usedLevels.Count - 1; index++)
            {
                lookup.Add(new TestBalanceModeInfo()
                {
                    Id = typeId++,
                    Name = usedLevels[index].Name,
                    Level = usedLevels[index].No + 1,
                    IsDetail = true
                });
            }

            return lookup;
        }

        private readonly IConfigRepository _config;
    }
}
