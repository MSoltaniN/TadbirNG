import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrganizationRoutingModule } from './organization-routing.module';

import { SharedModule } from '../shared/shared.module';

import { BranchService, CompanyService, FiscalPeriodService } from '@sppc/organization/service/index';

import { BranchComponent } from '@sppc/organization/components/branch/branch.component';
import { BranchFormComponent } from '@sppc/organization/components/branch/branch-form.component';
import { BranchRolesFormComponent } from '@sppc/organization/components/branch/branch-roles-form.component';
import { CompanyComponent } from '@sppc/organization/components/company/company.component';
import { CompanyFormComponent } from '@sppc/organization/components/company/company-form.component';
import { FiscalPeriodComponent } from '@sppc/organization/components/fiscalPeriod/fiscalPeriod.component';
import { FiscalPeriodFormComponent } from '@sppc/organization/components/fiscalPeriod/fiscalPeriod-form.component';
import { FiscalPeriodRolesFormComponent } from '@sppc/organization/components/fiscalPeriod/fiscalPeriod-roles-form.component';
import { InitialWizardComponent } from '@sppc/organization/components/initialWizard/initialWizard.component';

@NgModule({
  imports: [
    CommonModule,
    OrganizationRoutingModule,
    SharedModule
  ],
  declarations: [BranchComponent, BranchFormComponent, BranchRolesFormComponent, CompanyComponent, CompanyFormComponent, FiscalPeriodComponent,
    FiscalPeriodFormComponent, FiscalPeriodRolesFormComponent, InitialWizardComponent],
  entryComponents: [BranchFormComponent, BranchRolesFormComponent, CompanyFormComponent, InitialWizardComponent, FiscalPeriodFormComponent],
  providers: [BranchService, CompanyService, FiscalPeriodService]
})
export class OrganizationModule { }
