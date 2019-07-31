import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FinanceRoutingModule } from './finance-routing.module';

import { AccountComponent } from '@sppc/finance/components/base/account/account.component';
import { AccountFormComponent } from '@sppc/finance/components/base/account/account-form.component';
import { AccountCollectionComponent } from '@sppc/finance/components/base/accountCollection/accountCollection.component';
import { AccountGroupsComponent } from '@sppc/finance/components/base/accountGroups/accountGroups.component';
import { AccountGroupsFormComponent } from '@sppc/finance/components/base/accountGroups/accountGroups-form.component';
import { AccountRelationsComponent } from '@sppc/finance/components/base/accountRelations/accountRelations.component';
import { AccountRelationsFormComponent } from '@sppc/finance/components/base/accountRelations/accountRelations-form.component';
import { CostCenterComponent } from '@sppc/finance/components/base/costCenter/costCenter.component';
import { CostCenterFormComponent } from '@sppc/finance/components/base/costCenter/costCenter-form.component';
import { CurrencyComponent } from '@sppc/finance/components/base/currency/currency.component';
import { CurrencyFormComponent } from '@sppc/finance/components/base/currency/currency-form.component';
import { DetailAccountComponent } from '@sppc/finance/components/base/detailAccount/detailAccount.component';
import { DetailAccountFormComponent } from '@sppc/finance/components/base/detailAccount/detailAccount-form.component';
import { ProjectComponent } from '@sppc/finance/components/base/project/project.component';
import { ProjectFormComponent } from '@sppc/finance/components/base/project/project-form.component';

@NgModule({
  imports: [
    CommonModule,
    FinanceRoutingModule
  ],
  declarations: [AccountComponent, AccountFormComponent, AccountCollectionComponent, AccountGroupsComponent, AccountGroupsFormComponent, AccountRelationsComponent,
    AccountRelationsFormComponent, CostCenterComponent, CostCenterFormComponent, CurrencyComponent, CurrencyFormComponent, DetailAccountComponent, DetailAccountFormComponent,
    ProjectComponent, ProjectFormComponent],
  entryComponents: [AccountFormComponent, AccountGroupsFormComponent, AccountRelationsFormComponent, CostCenterFormComponent, CurrencyFormComponent, DetailAccountFormComponent,
    ProjectFormComponent]
})
export class FinanceModule { }
