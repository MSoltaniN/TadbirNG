"use strict";
// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.349
//     Template Version: 1.0
//     Generation Date: 07/22/2018 12:37:13 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
Object.defineProperty(exports, "__esModule", { value: true });
var environment_1 = require("../../../environments/environment");
var RoleApi = /** @class */ (function () {
    function RoleApi() {
    }
    // roles
    RoleApi.Roles = environment_1.environment.BaseUrl + "/roles";
    // roles/{roleId:min(1)}
    RoleApi.Role = environment_1.environment.BaseUrl + "/roles/{0}";
    // roles/{roleId:min(1)}/details
    RoleApi.RoleDetails = environment_1.environment.BaseUrl + "/roles/{0}/details";
    // roles/{roleId:min(1)}/branches
    RoleApi.RoleBranches = environment_1.environment.BaseUrl + "/roles/{0}/branches";
    // roles/{roleId:min(1)}/users
    RoleApi.RoleUsers = environment_1.environment.BaseUrl + "/roles/{0}/users";
    // roles/{roleId:min(1)}/fperiods
    RoleApi.RoleFiscalPeriods = environment_1.environment.BaseUrl + "/roles/{0}/fperiods";
    // roles/new
    RoleApi.NewRole = environment_1.environment.BaseUrl + "/roles/new";
    // roles/metadata
    RoleApi.RoleMetadata = environment_1.environment.BaseUrl + "/roles/metadata";
    // roles/{roleId:min(1)}/rowaccess
    RoleApi.RowAccessSettings = environment_1.environment.BaseUrl + "/roles/{0}/rowaccess";
    return RoleApi;
}());
exports.RoleApi = RoleApi;
//# sourceMappingURL=roleApi.js.map