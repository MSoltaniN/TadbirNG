// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.327
//     Template Version: 1.0
//     Generation Date: 06/24/2018 03:00:07 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

import { Environment } from "../../enviroment";

export class AccountRelationApi {

    // relations/accounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}
    public static FiscalPeriodBranchAccounts = Environment.BaseUrl + "/relations/accounts/fp/{0}/branch/{1}";

    // relations/faccounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}
    public static FiscalPeriodBranchDetailAccounts = Environment.BaseUrl + "/relations/faccounts/fp/{0}/branch/{1}";

    // relations/ccenters/fp/{fpId:min(1)}/branch/{branchId:min(1)}
    public static FiscalPeriodBranchCostCenters = Environment.BaseUrl + "/relations/ccenters/fp/{0}/branch/{1}";

    // relations/projects/fp/{fpId:min(1)}/branch/{branchId:min(1)}
    public static FiscalPeriodBranchProjects = Environment.BaseUrl + "/relations/projects/fp/{0}/branch/{1}";

    // relations/account/{accountId:min(1)}/faccounts
    public static DetailAccountsRelatedToAccount = Environment.BaseUrl + "/relations/account/{0}/faccounts";

    // relations/account/{accountId:min(1)}/ccenters
    public static CostCentersRelatedToAccount = Environment.BaseUrl + "/relations/account/{0}/ccenters";

    // relations/account/{accountId:min(1)}/projects
    public static ProjectsRelatedToAccount = Environment.BaseUrl + "/relations/account/{0}/projects";

    // relations/faccount/{faccountId:min(1)}/accounts
    public static AccountsRelatedToDetailAccount = Environment.BaseUrl + "/relations/faccount/{0}/accounts";

    // relations/ccenter/{ccenterId:min(1)}/accounts
    public static AccountsRelatedToCostCenter = Environment.BaseUrl + "/relations/ccenter/{0}/accounts";

    // relations/project/{projectId:min(1)}/accounts
    public static AccountsRelatedToProject = Environment.BaseUrl + "/relations/project/{0}/accounts";
}