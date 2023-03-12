import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TreasuryRoutingModule } from './treasury-routing.module';
import { CheckBookComponent } from './components/check-book/check-book.component';
import { CheckBookReportComponent } from './components/checkBook-report/checkBook-report.component';
import { SharedModule } from '@sppc/shared/shared.module';
import { CashRegistersComponent } from './components/cash-registers/cash-registers.component';
import { CashRegistersFormComponent } from './components/cash-registers/cash-registers-form.component';
import { CashRegisterRoleFormComponent } from './components/cash-registers/cash-register-role-form/cash-register-role-form.component';


@NgModule({
  imports: [
    CommonModule,
    TreasuryRoutingModule,
    SharedModule
  ],
  declarations: [
    CheckBookComponent,
    CheckBookReportComponent,
    CashRegistersComponent,
    CashRegistersFormComponent,
    CashRegisterRoleFormComponent
  ],
  entryComponents:[CashRegistersFormComponent],
  exports: [TreasuryRoutingModule]
})
export class TreasuryModule { }
