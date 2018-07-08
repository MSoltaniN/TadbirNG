using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    /// <summary>
    /// Defines API server routes and API client operation URLs for working with account vector relationships.
    /// </summary>
    public sealed class AccountRelationApi
    {
        private AccountRelationApi()
        {
        }

        /// <summary>
        /// API client URL for accounts in a specific fiscal period and branch that can be related to other components
        /// </summary>
        public const string FiscalPeriodBranchAccounts = "relations/accounts/fp/{0}/branch/{1}";

        /// <summary>
        /// API server route URL for accounts in a specific fiscal period and branch that can be related to other components
        /// </summary>
        public const string FiscalPeriodBranchAccountsUrl = "relations/accounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}";

        /// <summary>
        /// API client URL for detail accounts in a specific fiscal period and branch that can be related to other components
        /// </summary>
        public const string FiscalPeriodBranchDetailAccounts = "relations/faccounts/fp/{0}/branch/{1}";

        /// <summary>
        /// API server route URL for detail accounts in a specific fiscal period and branch that can be related to other components
        /// </summary>
        public const string FiscalPeriodBranchDetailAccountsUrl = "relations/faccounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}";

        /// <summary>
        /// API client URL for cost centers in a specific fiscal period and branch that can be related to other components
        /// </summary>
        public const string FiscalPeriodBranchCostCenters = "relations/ccenters/fp/{0}/branch/{1}";

        /// <summary>
        /// API server route URL for cost centers in a specific fiscal period and branch that can be related to other components
        /// </summary>
        public const string FiscalPeriodBranchCostCentersUrl = "relations/ccenters/fp/{fpId:min(1)}/branch/{branchId:min(1)}";

        /// <summary>
        /// API client URL for projects in a specific fiscal period and branch that can be related to other components
        /// </summary>
        public const string FiscalPeriodBranchProjects = "relations/projects/fp/{0}/branch/{1}";

        /// <summary>
        /// API server route URL for projects in a specific fiscal period and branch that can be related to other components
        /// </summary>
        public const string FiscalPeriodBranchProjectsUrl = "relations/projects/fp/{fpId:min(1)}/branch/{branchId:min(1)}";

        /// <summary>
        /// API client URL for all detail accounts related to an account specified by unique identifier
        /// </summary>
        public const string DetailAccountsRelatedToAccount = "relations/account/{0}/faccounts";

        /// <summary>
        /// API server route URL for all detail accounts related to an account specified by unique identifier
        /// </summary>
        public const string DetailAccountsRelatedToAccountUrl = "relations/account/{accountId:min(1)}/faccounts";

        /// <summary>
        /// API client URL for all cost centers related to an account specified by unique identifier
        /// </summary>
        public const string CostCentersRelatedToAccount = "relations/account/{0}/ccenters";

        /// <summary>
        /// API server route URL for all cost centers related to an account specified by unique identifier
        /// </summary>
        public const string CostCentersRelatedToAccountUrl = "relations/account/{accountId:min(1)}/ccenters";

        /// <summary>
        /// API client URL for all projects related to an account specified by unique identifier
        /// </summary>
        public const string ProjectsRelatedToAccount = "relations/account/{0}/projects";

        /// <summary>
        /// API server route URL for all projects related to an account specified by unique identifier
        /// </summary>
        public const string ProjectsRelatedToAccountUrl = "relations/account/{accountId:min(1)}/projects";

        /// <summary>
        /// API client URL for all accounts related to a detail account specified by unique identifier
        /// </summary>
        public const string AccountsRelatedToDetailAccount = "relations/faccount/{0}/accounts";

        /// <summary>
        /// API server route URL for all accounts related to a detail account specified by unique identifier
        /// </summary>
        public const string AccountsRelatedToDetailAccountUrl = "relations/faccount/{faccountId:min(1)}/accounts";

        /// <summary>
        /// API client URL for all accounts related to a cost center specified by unique identifier
        /// </summary>
        public const string AccountsRelatedToCostCenter = "relations/ccenter/{0}/accounts";

        /// <summary>
        /// API server route URL for all accounts related to a cost center specified by unique identifier
        /// </summary>
        public const string AccountsRelatedToCostCenterUrl = "relations/ccenter/{ccenterId:min(1)}/accounts";

        /// <summary>
        /// API client URL for all accounts related to a project specified by unique identifier
        /// </summary>
        public const string AccountsRelatedToProject = "relations/project/{0}/accounts";

        /// <summary>
        /// API server route URL for all accounts related to a project specified by unique identifier
        /// </summary>
        public const string AccountsRelatedToProjectUrl = "relations/project/{projectId:min(1)}/accounts";

        /// <summary>
        /// API client URL for all detail accounts not related to an account specified by unique identifier
        /// </summary>
        public const string DetailAccountsNotRelatedToAccount = "api/relations/free/accounts/{0}/faccounts";

        /// <summary>
        /// API server route URL for all detail accounts not related to an account specified by unique identifier
        /// </summary>
        public const string DetailAccountsNotRelatedToAccountUrl = "api/relations/free/accounts/{accountId:min(1)}/faccounts";

        /// <summary>
        /// API client URL for all cost centers not related to an account specified by unique identifier
        /// </summary>
        public const string CostCentersNotRelatedToAccount = "api/relations/free/accounts/{0}/ccenters";

        /// <summary>
        /// API server route URL for all cost centers not related to an account specified by unique identifier
        /// </summary>
        public const string CostCentersNotRelatedToAccountUrl = "api/relations/free/accounts/{accountId:min(1)}/ccenters";

        /// <summary>
        /// API client URL for all projects not related to an account specified by unique identifier
        /// </summary>
        public const string ProjectsNotRelatedToAccount = "api/relations/free/accounts/{0}/projects";

        /// <summary>
        /// API server route URL for all projects not related to an account specified by unique identifier
        /// </summary>
        public const string ProjectsNotRelatedToAccountUrl = "api/relations/free/accounts/{accountId:min(1)}/projects";

        /// <summary>
        /// API client URL for all accounts not related to a detail account specified by unique identifier
        /// </summary>
        public const string AccountsNotRelatedToDetailAccount = "api/relations/free/faccounts/{0}/accounts";

        /// <summary>
        /// API server route URL for all accounts not related to a detail account specified by unique identifier
        /// </summary>
        public const string AccountsNotRelatedToDetailAccountUrl = "api/relations/free/faccounts/{faccountId:min(1)}/accounts";

        /// <summary>
        /// API client URL for all accounts not related to a cost center specified by unique identifier
        /// </summary>
        public const string AccountsNotRelatedToCostCenter = "api/relations/free/ccenters/{0}/accounts";

        /// <summary>
        /// API server route URL for all accounts not related to a cost center specified by unique identifier
        /// </summary>
        public const string AccountsNotRelatedToCostCenterUrl = "api/relations/free/ccenters/{ccenterId:min(1)}/accounts";

        /// <summary>
        /// API client URL for all accounts not related to a project specified by unique identifier
        /// </summary>
        public const string AccountsNotRelatedToProject = "api/relations/free/projects/{0}/accounts";

        /// <summary>
        /// API server route URL for all accounts not related to a project specified by unique identifier
        /// </summary>
        public const string AccountsNotRelatedToProjectUrl = "api/relations/free/projects/{projectId:min(1)}/accounts";
    }
}
