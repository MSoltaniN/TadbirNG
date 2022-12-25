using System;

namespace SPPC.Tadbir.Persistence.DbUpgrade
{
    internal static class DbUpgradeConstants
    {
        internal const string SysDbUpgradeScript = "TadbirSys_UpdateDbObjects.sql";

        internal const string DbUpgradeScript = "Tadbir_UpdateDbObjects.sql";

        internal const string ScriptBlockRegex = @"-- (\d{1,}).(\d{1,}).(\d{1,})";
    }
}
