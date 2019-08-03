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
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  {
    path: '',
    component: LayoutComponent,
    canActivate: [AuthGuard],
    children: [
      { path: 'dashboard', component: DashboardComponent },
      { path: 'home', component: HomeComponent },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'reports', component: ReportManagementComponent },
    ]
  },
  { path: 'login', component: LoginContainerComponent },
  { path: 'logout', component: LogoutComponent },
  { path: '**', redirectTo: 'dashboard' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SharedRoutingModule { }
