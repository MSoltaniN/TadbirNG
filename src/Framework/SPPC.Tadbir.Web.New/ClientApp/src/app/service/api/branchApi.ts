// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.323
//     Template Version: 1.0
//     Generation Date: 06/12/2018 09:51:56 ق.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

import { environment } from "../../../environments/environment";

export class BranchApi {

    // branches/company/{companyId:min(1)}
    public static CompanyBranches = environment.BaseUrl + "/branches/company/{0}";

    // branches
    public static Branches = environment.BaseUrl + "/branches";

    // branches/{branchId:min(1)}
    public static Branch = environment.BaseUrl + "/branches/{0}";

    // branches/metadata
    public static BranchMetadata = environment.BaseUrl + "/branches/metadata";

    // branches/{branchId:min(1)}/roles
    public static BranchRoles = environment.BaseUrl + "/branches/{0}/roles";
}