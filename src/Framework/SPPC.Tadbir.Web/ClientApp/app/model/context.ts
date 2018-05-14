
import { PermissionBrief } from "./permissionBrief";

export interface Context {
    userName: string;
    password: string;
    firstName: string;
    lastName: string;
    ticket :string;
    fpId:number;
    branchId:number;
    companyId: number;
    permissions: Array<PermissionBrief>;
}