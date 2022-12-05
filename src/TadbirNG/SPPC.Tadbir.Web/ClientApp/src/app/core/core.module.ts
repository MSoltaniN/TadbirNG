import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthenticationService, AuthGuard, DashboardGuard } from '@sppc/core'

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [],
  providers: [AuthenticationService, AuthGuard, DashboardGuard]
})
export class CoreModule { }
