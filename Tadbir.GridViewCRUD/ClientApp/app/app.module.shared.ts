import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Headers, RequestOptions, BaseRequestOptions } from '@angular/http';
import { APP_BASE_HREF, CommonModule, Location, LocationStrategy, HashLocationStrategy } from '@angular/common';
// third party module to display toast 
import { ToastrModule } from 'toastr-ng2';
//PRIMENG - Third party module
import { InputTextModule, DataTableModule, ButtonModule, DialogModule, PanelModule, DropdownModule } from 'primeng/primeng';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { AccountComponent } from './components/account/account.component';

import { Account2Component } from './components/account2/account2.component';

import {BrowserModule} from "@angular/platform-browser";

import { TranslateModule } from "ng2-translate";

import { GridModule } from '@progress/kendo-angular-grid';

import { RTL } from '@progress/kendo-angular-l10n';


import { AccountService, TransactionLineService, FiscalPeriodService } from './service/index';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        AccountComponent,
        Account2Component
    ],
    providers: [AccountService, TransactionLineService, FiscalPeriodService,
        { provide: LocationStrategy, useClass: HashLocationStrategy }, { provide: RTL, useValue: true }],        
    imports: [
        CommonModule,         
        HttpModule,
        FormsModule,
        BrowserAnimationsModule,
        ToastrModule.forRoot(),
        InputTextModule, DataTableModule, ButtonModule, DialogModule, PanelModule, DropdownModule, GridModule ,
        BrowserModule,
        TranslateModule.forRoot(),
        RouterModule.forRoot([
            { path: '', redirectTo: 'account2', pathMatch: 'full' },
            { path: 'account', component: AccountComponent },
            { path: 'account2', component: Account2Component },
            { path: '**', redirectTo: 'account' }
        ])        
    ],
    schemas: [NO_ERRORS_SCHEMA]
})
export class AppModuleShared {
}
