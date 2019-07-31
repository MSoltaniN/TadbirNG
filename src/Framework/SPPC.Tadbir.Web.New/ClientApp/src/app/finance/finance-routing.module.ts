import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from '../shared/components/layout/layout.component';

const routes: Routes = [{
  path: 'finance',
  component: LayoutComponent,
  //canActivate: [AuthGuardService],
  children: [
    //{ path: '', component: DashboardComponent, canActivate: [AuthGuardService] },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FinanceRoutingModule { }
