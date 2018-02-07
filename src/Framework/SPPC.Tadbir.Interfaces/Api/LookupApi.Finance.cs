using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    public partial class LookupApi
    {
        #region Finance Subsystem Resources

        /// <summary>
        /// API client URL for lookup collection of all accounts in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchAccounts = "lookup/accounts/fp/{0}/branch/{1}";

        /// <summary>
        /// API server route URL for lookup collection of all accounts in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchAccountsUrl =
            "lookup/accounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}";

        /// <summary>
        /// API server route URL for lookup collection of all accounts in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchAccountsSyncUrl =
            "lookup/accounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}/sync";

        /// <summary>
        /// API client URL for lookup collection of all detail accounts in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchDetailAccounts = "lookup/faccounts/fp/{0}/branch/{1}";

        /// <summary>
        /// API server route URL for lookup collection of all detail accounts in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchDetailAccountsUrl =
            "lookup/faccounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}";

        /// <summary>
        /// API server route URL for lookup collection of all detail accounts in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchDetailAccountsSyncUrl =
            "lookup/faccounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}/sync";

        /// <summary>
        /// API client URL for lookup collection of all cost centers in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchCostCenters = "lookup/costcenters/fp/{0}/branch/{1}";

        /// <summary>
        /// API server route URL for lookup collection of all cost centers in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchCostCentersUrl =
            "lookup/costcenters/fp/{fpId:min(1)}/branch/{branchId:min(1)}";

        /// <summary>
        /// API server route URL for lookup collection of all cost centers in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchCostCentersSyncUrl =
            "lookup/costcenters/fp/{fpId:min(1)}/branch/{branchId:min(1)}/sync";

        /// <summary>
        /// API client URL for lookup collection of all projects in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchProjects = "lookup/projects/fp/{0}/branch/{1}";

        /// <summary>
        /// API server route URL for lookup collection of all projects in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchProjectsUrl =
            "lookup/projects/fp/{fpId:min(1)}/branch/{branchId:min(1)}";

        /// <summary>
        /// API server route URL for lookup collection of all projects in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchProjectsSyncUrl =
            "lookup/projects/fp/{fpId:min(1)}/branch/{branchId:min(1)}/sync";

        /// <summary>
        /// API client URL for lookup collection of all fiscal periods of a company
        /// </summary>
        public const string CompanyFiscalPeriods = "lookup/fps/company/{0}";

        /// <summary>
        /// API server route URL for lookup collection of all fiscal periods of a company
        /// </summary>
        public const string CompanyFiscalPeriodsUrl = "lookup/fps/company/{companyId:min(1)}";

        /// <summary>
        /// API server route URL for lookup collection of all fiscal periods of a company
        /// </summary>
        public const string CompanyFiscalPeriodsSyncUrl = "lookup/fps/company/{companyId:min(1)}/sync";

        /// <summary>
        /// API client URL for lookup collection of all branches of a company
        /// </summary>
        public const string CompanyBranches = "lookup/branches/company/{0}";

        /// <summary>
        /// API server route URL for lookup collection of all branches of a company
        /// </summary>
        public const string CompanyBranchesUrl = "lookup/branches/company/{companyId:min(1)}";

        /// <summary>
        /// API client URL for lookup collection of all currencies
        /// </summary>
        public const string Currencies = "lookup/currencies";

        /// <summary>
        /// API server route URL for lookup collection of all currencies
        /// </summary>
        public const string CurrenciesUrl = "lookup/currencies";

        /// <summary>
        /// API server route URL for lookup collection of all currencies
        /// </summary>
        public const string CurrenciesSyncUrl = "lookup/currencies/sync";

        #endregion
    }
}
