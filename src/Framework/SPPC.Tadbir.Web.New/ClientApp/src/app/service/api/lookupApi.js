"use strict";
// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.429
//     Template Version: 1.0
//     Generation Date: 10/20/2018 09:36:52 ق.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
Object.defineProperty(exports, "__esModule", { value: true });
var environment_1 = require("../../../environments/environment");
var LookupApi = /** @class */ (function () {
    function LookupApi() {
    }
    // lookup/partners
    LookupApi.Partners = environment_1.environment.BaseUrl + "/lookup/partners";
    // lookup/units
    LookupApi.Units = environment_1.environment.BaseUrl + "/lookup/units";
    // lookup/roles
    LookupApi.Roles = environment_1.environment.BaseUrl + "/lookup/roles";
    // lookup/views
    LookupApi.EntityViews = environment_1.environment.BaseUrl + "/lookup/views";
    // lookup/views/tree
    LookupApi.TreeViews = environment_1.environment.BaseUrl + "/lookup/views/tree";
    // lookup/accounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}
    LookupApi.FiscalPeriodBranchAccounts = environment_1.environment.BaseUrl + "/lookup/accounts/fp/{0}/branch/{1}";
    // lookup/faccounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}
    LookupApi.FiscalPeriodBranchDetailAccounts = environment_1.environment.BaseUrl + "/lookup/faccounts/fp/{0}/branch/{1}";
    // lookup/ccenters/fp/{fpId:min(1)}/branch/{branchId:min(1)}
    LookupApi.FiscalPeriodBranchCostCenters = environment_1.environment.BaseUrl + "/lookup/ccenters/fp/{0}/branch/{1}";
    // lookup/projects/fp/{fpId:min(1)}/branch/{branchId:min(1)}
    LookupApi.FiscalPeriodBranchProjects = environment_1.environment.BaseUrl + "/lookup/projects/fp/{0}/branch/{1}";
    // lookup/vouchers/fp/{fpId:min(1)}/branch/{branchId:min(1)}
    LookupApi.FiscalPeriodBranchVouchers = environment_1.environment.BaseUrl + "/lookup/vouchers/fp/{0}/branch/{1}";
    // lookup/vouchers/lines/fp/{fpId:min(1)}/branch/{branchId:min(1)}
    LookupApi.FiscalPeriodBranchVoucherLines = environment_1.environment.BaseUrl + "/lookup/vouchers/lines/fp/{0}/branch/{1}";
    // lookup/companies/user/{userId:min(1)}
    LookupApi.UserAccessibleCompanies = environment_1.environment.BaseUrl + "/lookup/companies/user/{0}";
    // lookup/fps/company/{companyId:min(1)}/user/{userId:min(1)}
    LookupApi.UserAccessibleCompanyFiscalPeriods = environment_1.environment.BaseUrl + "/lookup/fps/company/{0}/user/{1}";
    // lookup/branches/company/{companyId:min(1)}/user/{userId:min(1)}
    LookupApi.UserAccessibleCompanyBranches = environment_1.environment.BaseUrl + "/lookup/branches/company/{0}/user/{1}";
    // lookup/currencies
    LookupApi.Currencies = environment_1.environment.BaseUrl + "/lookup/currencies";
    // lookup/accgroup/categories
    LookupApi.AccountGroupCategories = environment_1.environment.BaseUrl + "/lookup/accgroup/categories";
    // lookup/accgroups
    LookupApi.AccountGroups = environment_1.environment.BaseUrl + "/lookup/accgroups";
    // lookup/accturnovermodes
    LookupApi.AccountTurnovers = environment_1.environment.BaseUrl + "/lookup/accturnovermodes";
    return LookupApi;
}());
exports.LookupApi = LookupApi;
//# sourceMappingURL=lookupApi.js.map