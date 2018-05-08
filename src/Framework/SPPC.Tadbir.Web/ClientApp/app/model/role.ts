

export interface Role {
    id: number;
    permissions: string[];
    name: string;
    description?: string;    
}

export interface Permission {
    id: number;
    groupId: number;
    groupName: string;
    isEnabled: boolean;
    name: string;
    flag: number;
    description?: string;  
}

export interface TreeNode {
    id: number;
    parentId?: number;
    name: string;
}

export class TreeNodeInfo implements TreeNode {
    constructor(public id: number = 0,
        public parentId: number | undefined,
        public name: string) { }
}


export interface RoleFullViewModel {
    role: Role;
    permissions: Array<Permission>;
}


export interface UserBriefViewModel {
    id: number;
    userName: string;
    personFirstName: string;
    personLastName: string;
    isEnabled: boolean;
    hasRole: boolean;
}


export interface RoleUsersViewModel {
    id: number;
    name: string;
    users: Array<UserBriefViewModel>;
}

export interface BranchViewModel {
    id: number;
    name: string;
    description?: string;
    level: number;
    companyId: number;
    isAccessible: boolean;
}

export interface RoleBranchesViewModel {
    id: number;
    name: string;
    branches: Array<BranchViewModel>;
}

export interface RoleDetailsViewModel {
    role: Role;
    permissions: Array<Permission>;
    branches: Array<BranchViewModel>;
    users: Array<UserBriefViewModel>;
}