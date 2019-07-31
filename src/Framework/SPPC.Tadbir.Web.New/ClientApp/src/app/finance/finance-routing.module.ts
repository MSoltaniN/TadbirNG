import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from '../shared/components/layout/layout.component';
import { AuthGuard } from '@sppc/core';
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
import { MetaDataResolver } from '@sppc/class/metadata/metadata.resolver';
import { AccountBookComponent } from './components/reporting/accountBook/accountBook.component';
import { JournalComponent } from './components/reporting/journal/journal.component';

const routes: Routes = [{
  path: 'finance',
  component: LayoutComponent,
  canActivate: [AuthGuard],
  children: [
    { path: 'account', component: AccountComponent },
    { path: 'account-collection', component: AccountCollectionComponent },
    { path: 'account-groups', component: AccountGroupsComponent },
    { path: 'accountrelations', component: AccountRelationsComponent },
    { path: 'costCenter', component: CostCenterComponent },
    { path: 'currency', component: CurrencyComponent },
    { path: 'detailAccount', component: DetailAccountComponent },
    { path: 'projects', component: ProjectComponent },
    { path: 'voucher', component: VoucherComponent },
    { path: 'vouchers/:mode', component: VoucherEditorComponent,
      data: { viewId: ViewName.Voucher },
      resolve: {
        team: MetaDataResolver
      }
    },
    { path: 'account-book', component: AccountBookComponent },
    { path: 'journal', component: JournalComponent },
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FinanceRoutingModule { }
