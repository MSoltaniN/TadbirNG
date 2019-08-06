import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './components/layout/layout.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AuthGuard } from '@sppc/core';
import { HomeComponent } from './components/home/home.component';
import { ReportManagementComponent } from './components/reportManagement/reportManagement.component';
import { LoginContainerComponent } from './components/login/login.container.component';
import { LogoutComponent } from './components/login/logout.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '', redirectTo: 'login', pathMatch: 'full' },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'home', component: HomeComponent },
      { path: 'reports', component: ReportManagementComponent },
      { path: 'login', component: LoginContainerComponent },
      { path: 'logout', component: LogoutComponent },
    ]
  },


];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SharedRoutingModule { }
