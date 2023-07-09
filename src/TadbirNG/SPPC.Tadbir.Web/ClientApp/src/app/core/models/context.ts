import { PermissionBrief } from "./permissionBrief";

export interface Context {
  userName: string;
  fullName: string;
  ticket: string;
  fpId: number;
  branchId: number;
  companyId: number;
  branchName: string;
  companyName: string;
  fiscalPeriodName: string;
  permissions: Array<PermissionBrief>;
  roles: Array<number>;
}
