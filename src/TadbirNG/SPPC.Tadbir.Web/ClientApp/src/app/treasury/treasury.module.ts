import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TreasuryRoutingModule } from './treasury-routing.module';
import { CheckBookComponent } from './components/check-book/check-book.component';
import { CheckBookReportComponent } from './components/checkBook-report/checkBook-report.component';
import { SharedModule } from '@sppc/shared/shared.module';
import { CashRegistersComponent } from './components/cash-registers/cash-registers.component';
import { CashRegistersFormComponent } from './components/cash-registers/cash-registers-form.component';
import { CashRegisterRolesFormComponent } from './components/cash-registers/cash-register-roles-form.component';

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
    CashRegisterRolesFormComponent
  ],
  entryComponents:[CashRegistersFormComponent,CashRegisterRolesFormComponent],
  exports: [TreasuryRoutingModule]
})
export class TreasuryModule { }
