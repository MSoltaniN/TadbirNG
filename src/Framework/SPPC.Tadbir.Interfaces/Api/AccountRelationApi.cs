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
        /// API client URL for accounts in current environment that can be related to other components
        /// </summary>
        public const string EnvironmentAccounts = "relations/accounts";

        /// <summary>
        /// API server route URL for accounts in current environment that can be related to other components
        /// </summary>
        public const string EnvironmentAccountsUrl = "relations/accounts";

        /// <summary>
        /// API client URL for detail accounts in current environment that can be related to other components
        /// </summary>
        public const string EnvironmentDetailAccounts = "relations/faccounts";

        /// <summary>
        /// API server route URL for detail accounts in current environment that can be related to other components
        /// </summary>
        public const string EnvironmentDetailAccountsUrl = "relations/faccounts";

        /// <summary>
        /// API client URL for cost centers in current environment that can be related to other components
        /// </summary>
        public const string EnvironmentCostCenters = "relations/ccenters";

        /// <summary>
        /// API server route URL for cost centers in current environment that can be related to other components
        /// </summary>
        public const string EnvironmentCostCentersUrl = "relations/ccenters";

        /// <summary>
        /// API client URL for projects in current environment that can be related to other components
        /// </summary>
        public const string EnvironmentProjects = "relations/projects";

        /// <summary>
        /// API server route URL for projects in current environment that can be related to other components
        /// </summary>
        public const string EnvironmentProjectsUrl = "relations/projects";

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
        public const string DetailAccountsNotRelatedToAccount = "relations/free/accounts/{0}/faccounts";

        /// <summary>
        /// API server route URL for all detail accounts not related to an account specified by unique identifier
        /// </summary>
        public const string DetailAccountsNotRelatedToAccountUrl = "relations/free/accounts/{accountId:min(1)}/faccounts";

        /// <summary>
        /// API client URL for all cost centers not related to an account specified by unique identifier
        /// </summary>
        public const string CostCentersNotRelatedToAccount = "relations/free/accounts/{0}/ccenters";

        /// <summary>
        /// API server route URL for all cost centers not related to an account specified by unique identifier
        /// </summary>
        public const string CostCentersNotRelatedToAccountUrl = "relations/free/accounts/{accountId:min(1)}/ccenters";

        /// <summary>
        /// API client URL for all projects not related to an account specified by unique identifier
        /// </summary>
        public const string ProjectsNotRelatedToAccount = "relations/free/accounts/{0}/projects";

        /// <summary>
        /// API server route URL for all projects not related to an account specified by unique identifier
        /// </summary>
        public const string ProjectsNotRelatedToAccountUrl = "relations/free/accounts/{accountId:min(1)}/projects";

        /// <summary>
        /// API client URL for all accounts not related to a detail account specified by unique identifier
        /// </summary>
        public const string AccountsNotRelatedToDetailAccount = "relations/free/faccounts/{0}/accounts";

        /// <summary>
        /// API server route URL for all accounts not related to a detail account specified by unique identifier
        /// </summary>
        public const string AccountsNotRelatedToDetailAccountUrl = "relations/free/faccounts/{faccountId:min(1)}/accounts";

        /// <summary>
        /// API client URL for all accounts not related to a cost center specified by unique identifier
        /// </summary>
        public const string AccountsNotRelatedToCostCenter = "relations/free/ccenters/{0}/accounts";

        /// <summary>
        /// API server route URL for all accounts not related to a cost center specified by unique identifier
        /// </summary>
        public const string AccountsNotRelatedToCostCenterUrl = "relations/free/ccenters/{ccenterId:min(1)}/accounts";

        /// <summary>
        /// API client URL for all accounts not related to a project specified by unique identifier
        /// </summary>
        public const string AccountsNotRelatedToProject = "relations/free/projects/{0}/accounts";

        /// <summary>
        /// API server route URL for all accounts not related to a project specified by unique identifier
        /// </summary>
        public const string AccountsNotRelatedToProjectUrl = "relations/free/projects/{projectId:min(1)}/accounts";
    }
}
