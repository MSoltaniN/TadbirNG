import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';

import { SharedModule } from '../shared/shared.module';

import { OperationLogService, RoleService, UserService, ViewRowPermissionService } from '@sppc/admin/service/index';

import { OperationLogsComponent } from '@sppc/admin/components/operationLogs/operationLogs.component';
import { OperationLogsDetailComponent } from '@sppc/admin/components/operationLogs/operationLogs-detail.component';
import { RoleComponent } from '@sppc/admin/components/role/role.component';
import { RoleFormComponent } from '@sppc/admin/components/role/role-form.component';
import { RoleBranchFormComponent } from '@sppc/admin/components/role/role-branch-form.component';
import { RoleDetailFormComponent } from '@sppc/admin/components/role/role-detail-form.component';
import { RoleFiscalPeriodFormComponent } from '@sppc/admin/components/role/role-fiscalPeriod-form.component';
import { RoleUserFormComponent } from '@sppc/admin/components/role/role-user-form.component';
import { UserComponent } from '@sppc/admin/components/user/user.component';
import { UserFormComponent } from '@sppc/admin/components/user/user-form.component';
import { UserRolesFormComponent } from '@sppc/admin/components/user/user-roles-form.component';
import { ChangePasswordComponent } from '@sppc/admin/components/user/changePassword.component';
import { ViewRowPermissionComponent } from '@sppc/admin/components/viewRowPermission/viewRowPermission.component';
import { ViewRowPermissionSingleFormComponent } from '@sppc/admin/components/viewRowPermission/viewRowPermission-single-form.component';
import { ViewRowPermissionMultipleFormComponent } from '@sppc/admin/components/viewRowPermission/viewRowPermission-multiple-form.component';


@NgModule({
  imports: [
    CommonModule,
    AdminRoutingModule,
    SharedModule
  ],
  declarations: [OperationLogsComponent, OperationLogsDetailComponent, OperationLogsDetailComponent, RoleComponent, RoleFormComponent, RoleBranchFormComponent, RoleDetailFormComponent,
    RoleFiscalPeriodFormComponent, RoleUserFormComponent, UserComponent, UserFormComponent, UserRolesFormComponent, ChangePasswordComponent, ViewRowPermissionComponent,
    ViewRowPermissionSingleFormComponent, ViewRowPermissionMultipleFormComponent],
  entryComponents: [],
  providers: [OperationLogService, RoleService, UserService, ViewRowPermissionService]
})
export class AdminModule { }
