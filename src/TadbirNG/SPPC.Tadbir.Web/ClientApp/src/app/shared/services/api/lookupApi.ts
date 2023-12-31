// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.853
//     Template Version: 1.0
//     Generation Date: 01/16/1398 09:12:59 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

import { environment } from "@sppc/env/environment";

export class LookupApi {

    // lookup/roles
    public static Roles = environment.BaseUrl + "/lookup/roles";

    // lookup/views
    public static EntityViews = environment.BaseUrl + "/lookup/views";

    // lookup/views
    public static EntityView = environment.BaseUrl + "/lookup/view/{0}";

    // lookup/views/base
    public static BaseEntityViews = environment.BaseUrl + "/lookup/views/base";

    // lookup/views/tree
    public static TreeViews = environment.BaseUrl + "/lookup/views/tree";

    // lookup/entities
    public static EntityTypes = environment.BaseUrl + "/lookup/entities";

    // lookup/sys/entities
    public static SystemEntityTypes = environment.BaseUrl + "/lookup/sys/entities";

    // lookup/users
    public static Users = environment.BaseUrl + "/lookup/users";

    // lookup/accounts
    public static EnvironmentAccounts = environment.BaseUrl + "/lookup/accounts";

    // lookup/faccounts
    public static EnvironmentDetailAccounts = environment.BaseUrl + "/lookup/faccounts";

    // lookup/ccenters
    public static EnvironmentCostCenters = environment.BaseUrl + "/lookup/ccenters";

    // lookup/projects
    public static EnvironmentProjects = environment.BaseUrl + "/lookup/projects";

    // lookup/vouchers
    public static EnvironmentVouchers = environment.BaseUrl + "/lookup/vouchers";

    // lookup/vouchers/lines
    public static EnvironmentVoucherLines = environment.BaseUrl + "/lookup/vouchers/lines";

    // lookup/companies/user/{userId:min(1)}
    public static UserAccessibleCompanies = environment.BaseUrl + "/lookup/companies/user/{0}";

    // lookup/fps/company/{companyId:min(1)}/user/{userId:min(1)}
    public static UserAccessibleCompanyFiscalPeriods = environment.BaseUrl + "/lookup/fps/company/{0}/user/{1}";

    // lookup/branches/company/{companyId:min(1)}/user/{userId:min(1)}
    public static UserAccessibleCompanyBranches = environment.BaseUrl + "/lookup/branches/company/{0}/user/{1}";

    // lookup/currencies
    public static Currencies = environment.BaseUrl + "/lookup/currencies";

    // lookup/currencies/info
    public static CurrenciesInfo = environment.BaseUrl + "/lookup/currencies/info";

    // lookup/acc-group/categories
    public static AccountGroupCategories = environment.BaseUrl + "/lookup/acc-group/categories";

    // lookup/acc-groups
    public static AccountGroups = environment.BaseUrl + "/lookup/acc-groups";

    // lookup/acc-turnover-modes
    public static AccountTurnovers = environment.BaseUrl + "/lookup/acc-turnover-modes";

    // lookup/acc-parts
    public static FullAccountParts = environment.BaseUrl + "/lookup/acc-parts";

    // lookup/types/voucher
    public static VoucherSysTypes = environment.BaseUrl + "/lookup/types/voucher";

    // lookup/types/voucher-line
    public static VoucherLineTypes = environment.BaseUrl + "/lookup/types/voucher-line";

    // {date:DateTime}
    public static VouchersByDate = environment.BaseUrl + "/lookup/vouchers/by-date/{0}";

    // lookup/accbook/views/{viewId:min(1)}/levels
    public static AccountBookLevels = environment.BaseUrl + "/lookup/accbook/views/{0}/levels";

    // lookup/provinces
    public static Provinces = environment.BaseUrl + "/lookup/provinces";

    // lookup/cities/{provinceCode}
    public static Cities = environment.BaseUrl + "/lookup/cities/{0}";

    // lookup/rowaccess/views/{viewId:min(1)}
    public static ValidRowPermissions = environment.BaseUrl + "/lookup/rowaccess/views/{0}";

    // lookup/inv-acc
    public static InventoryAccounts = environment.BaseUrl + "/lookup/inv-acc"

    // lookup/vouchers/references
    public static VoucherReferences = environment.BaseUrl + "/lookup/vouchers/references";

    /*
    * API client URL for lookup collection of all source/application based on type {sourceAppType}
    * 0 for 'Sources', 1 for 'Applications', 2 for both
    */
    public static SourceApps = environment.BaseUrl + "/lookup/source-apps/types/{0}";
}
