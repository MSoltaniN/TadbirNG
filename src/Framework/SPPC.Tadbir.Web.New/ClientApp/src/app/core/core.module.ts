import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthenticationService, AuthGuard } from '@sppc/core'

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [],
  providers: [AuthenticationService, AuthGuard]
})
export class CoreModule { }
