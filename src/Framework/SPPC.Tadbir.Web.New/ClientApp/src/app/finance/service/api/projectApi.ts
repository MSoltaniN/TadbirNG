// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.1013
//     Template Version: 1.0
//     Generation Date: 2020-10-29 11:02:55 AM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

import { environment } from "@sppc/env/environment";

export class ProjectApi {

    // projects
    public static EnvironmentProjects = environment.BaseUrl + "/projects";

    // projects/root
    public static RootProjects = environment.BaseUrl + "/projects/root";

    // projects/{projectId:min(1)}
    public static Project = environment.BaseUrl + "/projects/{0}";

    // projects/{projectId:min(1)}/children
    public static ProjectChildren = environment.BaseUrl + "/projects/{0}/children";

    // projects/{projectId:int}/children/new
    public static NewChildProject = environment.BaseUrl + "/projects/{0}/children/new";

    // projects/{projectId:int}/fullcode
    public static ProjectFullCode = environment.BaseUrl + "/projects/{0}/fullcode";
}