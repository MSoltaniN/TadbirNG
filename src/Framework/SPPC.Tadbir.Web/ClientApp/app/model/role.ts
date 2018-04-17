

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


export interface RoleFullViewModel {
    role: Role;
    permissions: Array<Permission>;
}