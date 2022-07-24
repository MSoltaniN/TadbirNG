import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './core';
import { FinanceModule } from './finance/finance.module';
import { LayoutComponent } from './shared/components/layout/layout.component';
import { NotFoundComponent } from './shared/components/notFound/notFound.component';
export function getFinanceModule() { return FinanceModule; }

const routes: Routes = [
  // {
  //   path: 'admin', component: LayoutComponent, canActivate: [AuthGuard],
  //   loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule)
  // },
  // {
  //   path: "finance", component:LayoutComponent, canActivate: [AuthGuard],
  //   loadChildren: () => import('./finance/finance.module').then(m => m.FinanceModule)
  // },
  // {
  //   path: 'organization',component: LayoutComponent,canActivate: [AuthGuard],
  //   loadChildren: () => import('./organization/organization.module').then(m => m.OrganizationModule)
  // },
  // {
  //   path: 'config', component: LayoutComponent, canActivate: [AuthGuard],
  //   loadChildren: () => import('./config/config.module').then(m => m.ConfigModule)
  // },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
