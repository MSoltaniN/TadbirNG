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

import { environment } from "../../../environments/environment";

export class LookupApi {

  // lookup/partners
  public static Partners = environment.BaseUrl + "/lookup/partners";

  // lookup/units
  public static Units = environment.BaseUrl + "/lookup/units";

  // lookup/roles
  public static Roles = environment.BaseUrl + "/lookup/roles";

  // lookup/views
  public static EntityViews = environment.BaseUrl + "/lookup/views";

  // lookup/views/tree
  public static TreeViews = environment.BaseUrl + "/lookup/views/tree";

  // lookup/accounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}
  public static FiscalPeriodBranchAccounts = environment.BaseUrl + "/lookup/accounts/fp/{0}/branch/{1}";

  // lookup/faccounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}
  public static FiscalPeriodBranchDetailAccounts = environment.BaseUrl + "/lookup/faccounts/fp/{0}/branch/{1}";

  // lookup/ccenters/fp/{fpId:min(1)}/branch/{branchId:min(1)}
  public static FiscalPeriodBranchCostCenters = environment.BaseUrl + "/lookup/ccenters/fp/{0}/branch/{1}";

  // lookup/projects/fp/{fpId:min(1)}/branch/{branchId:min(1)}
  public static FiscalPeriodBranchProjects = environment.BaseUrl + "/lookup/projects/fp/{0}/branch/{1}";

  // lookup/vouchers/fp/{fpId:min(1)}/branch/{branchId:min(1)}
  public static FiscalPeriodBranchVouchers = environment.BaseUrl + "/lookup/vouchers/fp/{0}/branch/{1}";

  // lookup/vouchers/lines/fp/{fpId:min(1)}/branch/{branchId:min(1)}
  public static FiscalPeriodBranchVoucherLines = environment.BaseUrl + "/lookup/vouchers/lines/fp/{0}/branch/{1}";

  // lookup/companies/user/{userId:min(1)}
  public static UserAccessibleCompanies = environment.BaseUrl + "/lookup/companies/user/{0}";

  // lookup/fps/company/{companyId:min(1)}/user/{userId:min(1)}
  public static UserAccessibleCompanyFiscalPeriods = environment.BaseUrl + "/lookup/fps/company/{0}/user/{1}";

  // lookup/branches/company/{companyId:min(1)}/user/{userId:min(1)}
  public static UserAccessibleCompanyBranches = environment.BaseUrl + "/lookup/branches/company/{0}/user/{1}";

  // lookup/currencies
  public static Currencies = environment.BaseUrl + "/lookup/currencies";

  // lookup/accgroup/categories
  public static AccountGroupCategories = environment.BaseUrl + "/lookup/accgroup/categories";

  // lookup/accgroups
  public static AccountGroups = environment.BaseUrl + "/lookup/accgroups";
}
