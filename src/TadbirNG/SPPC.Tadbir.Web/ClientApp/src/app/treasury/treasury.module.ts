import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccordionModule } from 'primeng/accordion';

import { TreasuryRoutingModule } from './treasury-routing.module';
import { CheckBookEditorComponent } from './components/check-book/check-book-editor.component';
import { CheckBookReportComponent } from './components/checkBook-report/checkBook-report.component';
import { SharedModule } from '@sppc/shared/shared.module';
import { CashRegistersComponent } from './components/cash-registers/cash-registers.component';
import { CashRegistersFormComponent } from './components/cash-registers/cash-registers-form.component';
import { CashRegisterRolesFormComponent } from './components/cash-registers/cash-register-roles-form.component';
import { CheckBookPagesComponent } from './components/check-book/check-book-pages/check-book-pages.component';
import { SourcesApplicationsComponent } from './components/sources-applications/sources-applications.component';
import { SourceAppFormComponent } from './components/sources-applications/sourceApp-form/sourceApp-form.component';
import { PayReceiveEditorComponent } from './components/pay-receive/pay-receive-editor.component';
import { PayerReceiverComponent } from './components/pay-receive/payer-receiver/payer-receiver.component';
import { PayReciveCheksComponent } from './components/pay-receive/pay-recive-cheks/pay-recive-cheks.component';
import { FundOrBankComponent } from './components/pay-receive/fund-or-bank/fund-or-bank.component';
import { SelectButtonModule } from 'primeng/selectbutton';

@NgModule({
  imports: [
    CommonModule,
    TreasuryRoutingModule,
    SharedModule,
    AccordionModule,
    SelectButtonModule
  ],
  declarations: [
    CheckBookEditorComponent,
    CheckBookReportComponent,
    CashRegistersComponent,
    CashRegistersFormComponent,
    CashRegisterRolesFormComponent,
    CheckBookPagesComponent,
    SourcesApplicationsComponent,
    SourceAppFormComponent,
    PayReceiveEditorComponent,
    PayerReceiverComponent,
    PayReciveCheksComponent,
    FundOrBankComponent
  ],
  entryComponents:[CashRegistersFormComponent,CashRegisterRolesFormComponent],
  exports: [TreasuryRoutingModule]
})
export class TreasuryModule { }
