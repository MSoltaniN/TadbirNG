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

export class ProjectApi {

    // projects
    public static EnvironmentProjects = environment.BaseUrl + "/projects";

    // projects/lookup
    public static EnvironmentProjectsLookup = environment.BaseUrl + "/projects/lookup";

    // projects/ledger
    public static EnvironmentProjectsLedger = environment.BaseUrl + "/projects/ledger";

    // projects/{projectId:min(1)}
    public static Project = environment.BaseUrl + "/projects/{0}";

    // projects/{projectId:min(1)}/children
    public static ProjectChildren = environment.BaseUrl + "/projects/{0}/children";

    // projects/{projectId:int}/children/new
    public static EnvironmentNewChildProject = environment.BaseUrl + "/projects/{0}/children/new";

    // projects/fullcode/{parentId}
    public static ProjectFullCode = environment.BaseUrl + "/projects/fullcode/{0}";
}