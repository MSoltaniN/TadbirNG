import { PermissionBrief } from "./permissionBrief";
import { IEntity } from "./IEntity";

export interface Context extends IEntity {
  userName: string;
  password: string;
  firstName: string;
  lastName: string;
  ticket: string;
  fpId: number;
  branchId: number;
  companyId: number;
  permissions: Array<PermissionBrief>;
}
