import { Permission } from "@sppc/core";
import { IEntity } from "@sppc/shared/models";


// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.282
//     Template Version: 1.0
//     Generation Date: 05/09/2018 12:55:10 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

export interface Role extends IEntity {
    permissions: string[];
    id: number;
    name: string;
    flag: number;
    description?: string;  
}

export interface RoleFullViewModel extends IEntity {
    id: number;
    role: Role;
    permissions: Array<Permission>;
}

export interface UserBriefViewModel extends IEntity {
    id: number;
    userName: string;
    personFullName: string;
    isEnabled: boolean;
    hasRole: boolean;
}

export interface RoleUsersViewModel extends IEntity {
    id: number;
    name: string;
    users: Array<UserBriefViewModel>;
}

export interface BranchViewModel extends IEntity {
    id: number;
    name: string;
    description?: string;
    level: number;
    isAccessible: boolean;
}

export interface RoleBranchesViewModel extends IEntity {
    id: number;
    name: string;
    branches: Array<BranchViewModel>;
}

export interface RoleDetailsViewModel extends IEntity {
    role: Role;
    permissions: Array<Permission>;
    branches: Array<BranchViewModel>;
    users: Array<UserBriefViewModel>;
}
