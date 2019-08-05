import { IEntity } from "@sppc/shared/models";
import { Role, UserBrief } from ".";
import { Permission } from "@sppc/core";
import { Branch } from "@sppc/organization";

// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.282
//     Template Version: 1.0
//     Generation Date: 05/09/2018 01:43:51 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------



export interface RoleDetails extends IEntity{
    role: Role;
    permissions: Array<Permission>;
    branches: Array<Branch>;
    users: Array<UserBrief>;
}
