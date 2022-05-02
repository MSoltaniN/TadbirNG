using System;

namespace SPPC.Tadbir.Persistence.DbUpgrade
{
    internal static class DbUpgradeQuery
    {
        internal const string FetchDbVersion = "SELECT Number FROM [Core].[Version]";

        internal const string UpdateDbVersion = "UPDATE [Core].[Version] SET Number = '{0}'";

        internal const string ActiveCompanies = @"
SELECT DbName, Server, UserName, Password
FROM [Config].[CompanyDb]
WHERE IsActive = 1";
    }
}
