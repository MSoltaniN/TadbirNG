// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.638
//     Template Version: 1.0
//     Generation Date: 6/25/2019 8:49:32 AM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

import { environment } from "../../../environments/environment";

export class CostCenterApi {

    // ccenters
    public static EnvironmentCostCenters = environment.BaseUrl + "/ccenters";

    // ccenters/lookup
    public static EnvironmentCostCentersLookup = environment.BaseUrl + "/ccenters/lookup";

    // ccenters/ledger
    public static EnvironmentCostCentersLedger = environment.BaseUrl + "/ccenters/ledger";

    // ccenters/{ccenterId:min(1)}
    public static CostCenter = environment.BaseUrl + "/ccenters/{0}";

    // ccenters/{ccenterId:min(1)}/children
    public static CostCenterChildren = environment.BaseUrl + "/ccenters/{0}/children";

    // ccenters/{ccenterId:int}/children/new
    public static EnvironmentNewChildCostCenter = environment.BaseUrl + "/ccenters/{0}/children/new";

    // ccenters/fullcode/{parentId}
    public static CostCenterFullCode = environment.BaseUrl + "/ccenters/fullcode/{0}";
}