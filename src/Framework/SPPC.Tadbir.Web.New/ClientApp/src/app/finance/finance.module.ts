import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FinanceRoutingModule } from './finance-routing.module';

import { SharedModule } from '../shared/shared.module';

import {
  AccountCollectionService, AccountGroupsService, AccountRelationsService, AccountService, CostCenterService, CurrencyService, DetailAccountService,
  FullAccountService, FullCodeService, ProjectService, VoucherLineService, VoucherService
} from '@sppc/finance/service/index';

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
import { VoucherComponent } from '@sppc/finance/components/operational/voucher/voucher.component';
import { VoucherEditorComponent } from '@sppc/finance/components/operational/voucher/voucher-editor.component';
import { VoucherLineComponent } from '@sppc/finance/components/operational/voucherLine/voucherLine.component';
import { VoucherLineFormComponent } from '@sppc/finance/components/operational/voucherLine/voucherLine-form.component';
import { AccountBookComponent } from '@sppc/finance/components/reporting/accountBook/accountBook.component';
import { JournalComponent } from '@sppc/finance/components/reporting/journal/journal.component';
import { currencyRateComponent } from '@sppc/finance/components/base/currencyRate/currencyRate.component';
import { CurrencyRateFormComponent } from '@sppc/finance/components/base/currencyRate/currencyRate-form.component';

@NgModule({
  imports: [
    CommonModule,
    FinanceRoutingModule,
    SharedModule
  ],
  declarations: [AccountComponent, AccountFormComponent, AccountCollectionComponent, AccountGroupsComponent, AccountGroupsFormComponent, AccountRelationsComponent,
    AccountRelationsFormComponent, CostCenterComponent, CostCenterFormComponent, CurrencyComponent, CurrencyFormComponent, DetailAccountComponent, DetailAccountFormComponent,
    ProjectComponent, ProjectFormComponent, VoucherComponent, VoucherEditorComponent, VoucherLineComponent, VoucherLineFormComponent, AccountBookComponent, JournalComponent,
    currencyRateComponent, CurrencyRateFormComponent],
  entryComponents: [AccountFormComponent, AccountGroupsFormComponent, CostCenterFormComponent, CurrencyFormComponent, DetailAccountFormComponent,
    ProjectFormComponent, VoucherEditorComponent, VoucherLineFormComponent, CurrencyRateFormComponent],
  providers: [AccountCollectionService, AccountGroupsService, AccountRelationsService, AccountService, CostCenterService, CurrencyService, DetailAccountService,
    FullAccountService, FullCodeService, ProjectService, VoucherLineService, VoucherService],
  schemas: [NO_ERRORS_SCHEMA]
})
export class FinanceModule { }
