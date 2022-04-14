using System;

namespace SPPC.Tadbir.Persistence.DbUpgrade
{
    internal static class DbUpgradeConstants
    {
        internal const string SysLoginName = "NgTadbirUser";

        internal const string SysLoginPassword = "Demo1234";

        internal const string SysDbName = "NGTadbirSys";

        internal const string SysDbUpgradeScript = "TadbirSys_UpdateDbObjects.sql";

        internal const string DbUpgradeScript = "Tadbir_UpdateDbObjects.sql";

        internal const string ScriptBlockRegex = @"-- (\d{1,}).(\d{1,}).(\d{1,})";
    }
}
