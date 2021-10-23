import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from '@sppc/shared/components/layout/layout.component';
import { AuthGuard } from '@sppc/core';
import { OperationLogsComponent } from '@sppc/admin/components/operationLogs/operationLogs.component';
import { RoleComponent } from '@sppc/admin/components/role/role.component';
import { UserComponent } from '@sppc/admin/components/user/user.component';
import { ChangePasswordComponent } from '@sppc/admin/components/user/changePassword.component';
import { ViewRowPermissionComponent } from '@sppc/admin/components/viewRowPermission/viewRowPermission.component';
import { LogSettingComponent } from '@sppc/admin/components/operationLogs/operationLogs-setting.component';



const routes: Routes = [{
  path: 'admin',
  component: LayoutComponent,
  canActivate: [AuthGuard],
  children: [
    { path: 'operation-log', component: OperationLogsComponent },
    { path: 'log-settings', component: LogSettingComponent },
    { path: 'roles', component: RoleComponent },
    { path: 'users', component: UserComponent },
    { path: 'changePassword', component: ChangePasswordComponent },
    { path: 'viewRowPermission', component: ViewRowPermissionComponent },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
