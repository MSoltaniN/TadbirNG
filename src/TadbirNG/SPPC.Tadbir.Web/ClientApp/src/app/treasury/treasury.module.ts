import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TreasuryRoutingModule } from './treasury-routing.module';
import { BaseDataComponent } from './components/base-data/base-data.component';
import { CheckOperationsComponent } from './components/check-operations/check-operations.component';
import { CheckReportsComponent } from './components/check-reports/check-reports.component';


@NgModule({
  declarations: [
    BaseDataComponent,
    CheckOperationsComponent,
    CheckReportsComponent
  ],
  imports: [
    CommonModule,
    TreasuryRoutingModule
  ]
})
export class TreasuryModule { }
