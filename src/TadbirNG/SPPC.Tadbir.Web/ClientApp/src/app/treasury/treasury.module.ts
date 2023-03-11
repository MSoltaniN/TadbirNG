import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TreasuryRoutingModule } from './treasury-routing.module';
import { CheckOperationsComponent } from './components/check-operations/check-operations.component';
import { CheckReportsComponent } from './components/check-reports/check-reports.component';
import { ManageCashRegistersComponent } from './components/manage-cash-registers/manage-cash-registers.component';
import { SharedModule } from '@sppc/shared/shared.module';
import { manageCashRegistersForm } from './components/manage-cash-registers/manage-cash-registers-form.component';


@NgModule({
  imports: [
    CommonModule,
    TreasuryRoutingModule,
    SharedModule
  ],
  declarations: [
    CheckOperationsComponent,
    CheckReportsComponent,
    ManageCashRegistersComponent,
    manageCashRegistersForm
  ],
  entryComponents:[],
  exports: [TreasuryRoutingModule]
})
export class TreasuryModule { }
