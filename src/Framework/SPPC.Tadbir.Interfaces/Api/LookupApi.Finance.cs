using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    public partial class LookupApi
    {
        #region Finance Subsystem Resources

        /// <summary>
        /// API client URL for lookup collection of all accounts in current application environment
        /// </summary>
        public const string EnvironmentAccounts = "lookup/accounts";

        /// <summary>
        /// API server route URL for lookup collection of all accounts in current application environment
        /// </summary>
        public const string EnvironmentAccountsUrl = "lookup/accounts";

        /// <summary>
        /// API client URL for lookup collection of all detail accounts in current application environment
        /// </summary>
        public const string EnvironmentDetailAccounts = "lookup/faccounts";

        /// <summary>
        /// API server route URL for lookup collection of all detail accounts in current application environment
        /// </summary>
        public const string EnvironmentDetailAccountsUrl = "lookup/faccounts";

        /// <summary>
        /// API client URL for lookup collection of all cost centers in current application environment
        /// </summary>
        public const string EnvironmentCostCenters = "lookup/ccenters";

        /// <summary>
        /// API server route URL for lookup collection of all cost centers in current application environment
        /// </summary>
        public const string EnvironmentCostCentersUrl = "lookup/ccenters";

        /// <summary>
        /// API client URL for lookup collection of all projects in current application environment
        /// </summary>
        public const string EnvironmentProjects = "lookup/projects";

        /// <summary>
        /// API server route URL for lookup collection of all projects in current application environment
        /// </summary>
        public const string EnvironmentProjectsUrl = "lookup/projects";

        /// <summary>
        /// API client URL for lookup collection of all vouchers in current application environment
        /// </summary>
        public const string EnvironmentVouchers = "lookup/vouchers";

        /// <summary>
        /// API server route URL for lookup collection of all vouchers in current application environment
        /// </summary>
        public const string EnvironmentVouchersUrl = "lookup/vouchers";

        /// <summary>
        /// API client URL for lookup collection of all voucher lines in current application environment
        /// </summary>
        public const string EnvironmentVoucherLines = "lookup/vouchers/lines";

        /// <summary>
        /// API server route URL for lookup collection of all voucher lines in current application environment
        /// </summary>
        public const string EnvironmentVoucherLinesUrl = "lookup/vouchers/lines";

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
        /// API client URL for lookup collection of all currencies with last rates
        /// </summary>
        public const string CurrenciesInfo = "lookup/currencies/info";

        /// <summary>
        /// API server route URL for lookup collection of all currencies with last rates
        /// </summary>
        public const string CurrenciesInfoUrl = "lookup/currencies/info";

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

        /// <summary>
        /// API client URL for lookup collection of all account turnover modes
        /// </summary>
        public const string AccountTurnovers = "lookup/accturnovermodes";

        /// <summary>
        /// API server route URL for lookup collection of all account turnover modes
        /// </summary>
        public const string AccountTurnoversUrl = "lookup/accturnovermodes";

        /// <summary>
        /// API client URL for lookup collection of voucher system types
        /// </summary>
        public const string VoucherSysTypes = "lookup/types/voucher";

        /// <summary>
        /// API server route URL for lookup collection of voucher system types
        /// </summary>
        public const string VoucherSysTypesUrl = "lookup/types/voucher";

        /// <summary>
        /// API client URL for lookup collection of voucher line types
        /// </summary>
        public const string VoucherLineTypes = "lookup/types/voucher-line";

        /// <summary>
        /// API server route URL for lookup collection of voucher line types
        /// </summary>
        public const string VoucherLineTypesUrl = "lookup/types/voucher-line";

        /// <summary>
        /// API client URL for all applicable tree levels in Account Book report
        /// </summary>
        public const string AccountBookLevels = "lookup/accbook/views/{viewId:min(1)}/levels";

        /// <summary>
        /// API server route URL for all applicable tree levels in Account Book report
        /// </summary>
        public const string AccountBookLevelsUrl = "lookup/accbook/views/{viewId:min(1)}/levels";

        #endregion
    }
}
