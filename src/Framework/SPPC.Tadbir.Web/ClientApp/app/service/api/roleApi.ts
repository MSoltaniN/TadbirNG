// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.286
//     Template Version: 1.0
//     Generation Date: 05/10/2018 11:38:33 ق.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

import { Environment } from "../../enviroment";

export class RoleApi {

    // roles
    public static Roles = Environment.BaseUrl + "/roles";

    // roles/{roleId:min(1)}
    public static Role = Environment.BaseUrl + "/roles/{0}";

    // roles/{roleId:min(1)}/details
    public static RoleDetails = Environment.BaseUrl + "/roles/{0}/details";

    // roles/{roleId:min(1)}/branches
    public static RoleBranches = Environment.BaseUrl + "/roles/{0}/branches";

    // roles/{roleId:min(1)}/users
    public static RoleUsers = Environment.BaseUrl + "/roles/{0}/users";

    // roles/new
    public static NewRole = Environment.BaseUrl + "/roles/new";

    // roles/metadata
    public static RoleMetadata = Environment.BaseUrl + "/roles/metadata";
}