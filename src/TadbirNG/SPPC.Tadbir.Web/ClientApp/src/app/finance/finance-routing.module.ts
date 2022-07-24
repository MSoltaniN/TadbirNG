import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AccountComponent } from './components/base/account/account.component';
import { AccountCollectionComponent } from './components/base/accountCollection/accountCollection.component';
import { AccountGroupsComponent } from './components/base/accountGroups/accountGroups.component';
import { AccountRelationsComponent } from './components/base/accountRelations/accountRelations.component';
import { CostCenterComponent } from './components/base/costCenter/costCenter.component';
import { CurrencyComponent } from './components/base/currency/currency.component';
import { DetailAccountComponent } from './components/base/detailAccount/detailAccount.component';
import { ProjectComponent } from './components/base/project/project.component';
import { VoucherComponent } from './components/operational/voucher/voucher.component';
import { VoucherEditorComponent } from './components/operational/voucher/voucher-editor.component';
import { ViewName } from '@sppc/shared/security';
import { AccountBookComponent } from './components/reporting/accountBook/accountBook.component';
import { JournalComponent } from './components/reporting/journal/journal.component';
import { currencyRateComponent } from './components/base/currencyRate/currencyRate.component';
import { TestBalanceComponent } from '@sppc/finance/components/reporting/testBalance/testBalance.component';
import { CurrencyBookComponent } from '@sppc/finance/components/reporting/currencyBook/currencyBook.component';
import { SystemIssueComponent } from '@sppc/finance//components/reporting/systemIssue/systemIssue.component';
import { ItemBalanceComponent } from './components/reporting/itemBalance/itemBalance.component';
import { BalanceByAccountComponent } from '@sppc/finance/components/reporting/balanceByAccount/balanceByAccount.component';
import { ProfitLostComponent } from './components/reporting/profitLoss/profitLost.component';
import { BalanceSheetComponent } from './components/reporting/balanceSheet/balanceSheet.component';


const routes: Routes = [
    { path: 'account', component: AccountComponent, data: { viewIds: [ViewName.Account] }},
    { path: 'account-collection', component: AccountCollectionComponent },
    { path: 'account-groups', component: AccountGroupsComponent },
    { path: 'accountrelations', component: AccountRelationsComponent },
    { path: 'costCenter', component: CostCenterComponent },
    { path: 'currency', component: CurrencyComponent },
    { path: 'detailAccount', component: DetailAccountComponent },
    { path: 'projects', component: ProjectComponent },
    { path: 'profit-loss', component: ProfitLostComponent },
    { path: 'voucher', component: VoucherComponent },
    { path: 'voucher/:mode', component: VoucherComponent },
    { path: 'vouchers/:mode', component: VoucherEditorComponent },
    { path: 'vouchers/:mode/:type', component: VoucherEditorComponent },
    { path: 'account-book', component: AccountBookComponent },
    { path: 'journal', component: JournalComponent },
    { path: 'currency-rate/:id', component: currencyRateComponent },
    { path: 'balance', component: TestBalanceComponent },
    { path: 'itembalance', component: ItemBalanceComponent },
    { path: 'currency-book', component: CurrencyBookComponent },
    { path: 'system-issue', component: SystemIssueComponent },
    { path: 'balance-by-account', component: BalanceByAccountComponent },
    { path: 'bal-sheet', component: BalanceSheetComponent },
  ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FinanceRoutingModule { }
