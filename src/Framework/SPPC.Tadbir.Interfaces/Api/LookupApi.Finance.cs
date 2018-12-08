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
        /// API client URL for lookup collection of all detail accounts in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchDetailAccounts = "lookup/faccounts/fp/{0}/branch/{1}";

        /// <summary>
        /// API server route URL for lookup collection of all detail accounts in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchDetailAccountsUrl =
            "lookup/faccounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}";

        /// <summary>
        /// API client URL for lookup collection of all cost centers in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchCostCenters = "lookup/ccenters/fp/{0}/branch/{1}";

        /// <summary>
        /// API server route URL for lookup collection of all cost centers in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchCostCentersUrl =
            "lookup/ccenters/fp/{fpId:min(1)}/branch/{branchId:min(1)}";

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
        /// API client URL for lookup collection of all vouchers in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchVouchers = "lookup/vouchers/fp/{0}/branch/{1}";

        /// <summary>
        /// API server route URL for lookup collection of all vouchers in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchVouchersUrl =
            "lookup/vouchers/fp/{fpId:min(1)}/branch/{branchId:min(1)}";

        /// <summary>
        /// API client URL for lookup collection of all voucher lines in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchVoucherLines = "lookup/vouchers/lines/fp/{0}/branch/{1}";

        /// <summary>
        /// API server route URL for lookup collection of all voucher lines in a fiscal period and a corporate branch
        /// </summary>
        public const string FiscalPeriodBranchVoucherLinesUrl =
            "lookup/vouchers/lines/fp/{fpId:min(1)}/branch/{branchId:min(1)}";

        /// <summary>
        /// API client URL for all companies accessible to a user specified by identifier.
        /// </summary>
        public const string UserAccessibleCompanies = "lookup/companies/user/{0}";

        /// <summary>
        /// API server route URL for all companies accessible to a user specified by identifier.
        /// </summary>
        public const string UserAccessibleCompaniesUrl = "lookup/companies/user/{userId:min(1)}";

        /// <summary>
        /// API client URL for lookup collection of all fiscal periods of a company
        /// </summary>
        public const string UserAccessibleCompanyFiscalPeriods = "lookup/fps/company/{0}/user/{1}";

        /// <summary>
        /// API server route URL for lookup collection of all fiscal periods of a company
        /// </summary>
        public const string UserAccessibleCompanyFiscalPeriodsUrl =
            "lookup/fps/company/{companyId:min(1)}/user/{userId:min(1)}";

        /// <summary>
        /// API client URL for lookup collection of all branches of a company
        /// </summary>
        public const string UserAccessibleCompanyBranches = "lookup/branches/company/{0}/user/{1}";

        /// <summary>
        /// API server route URL for lookup collection of all branches of a company
        /// </summary>
        public const string UserAccessibleCompanyBranchesUrl =
            "lookup/branches/company/{companyId:min(1)}/user/{userId:min(1)}";

        /// <summary>
        /// API client URL for lookup collection of all currencies
        /// </summary>
        public const string Currencies = "lookup/currencies";

        /// <summary>
        /// API server route URL for lookup collection of all currencies
        /// </summary>
        public const string CurrenciesUrl = "lookup/currencies";

        /// <summary>
        /// API client URL for lookup collection of all categories used in account groups
        /// </summary>
        public const string AccountGroupCategories = "lookup/accgroup/categories";

        /// <summary>
        /// API server route URL for lookup collection of all categories used in account groups
        /// </summary>
        public const string AccountGroupCategoriesUrl = "lookup/accgroup/categories";

        /// <summary>
        /// API client URL for lookup collection of all account groups
        /// </summary>
        public const string AccountGroups = "lookup/accgroups";

        /// <summary>
        /// API server route URL for lookup collection of all account groups
        /// </summary>
        public const string AccountGroupsUrl = "lookup/accgroups";

        #endregion
    }
}
