import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from '@sppc/shared/components/layout/layout.component';
import { AuthGuard } from '@sppc/core';
import { BranchComponent } from '@sppc/organization/components/branch/branch.component';
import { CompanyComponent } from '@sppc/organization/components/company/company.component';
import { FiscalPeriodComponent } from '@sppc/organization/components/fiscalPeriod/fiscalPeriod.component';

const routes: Routes = [{
  path: 'organization',
  component: LayoutComponent,
  canActivate: [AuthGuard],
  children: [
    { path: 'branches', component: BranchComponent },
    { path: 'companies', component: CompanyComponent },
    { path: 'fiscalperiod', component: FiscalPeriodComponent },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrganizationRoutingModule { }
