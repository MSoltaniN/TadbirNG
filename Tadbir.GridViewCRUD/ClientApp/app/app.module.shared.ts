import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Headers, RequestOptions, BaseRequestOptions } from '@angular/http';
import { APP_BASE_HREF, CommonModule, Location, LocationStrategy, HashLocationStrategy } from '@angular/common';
// third party module to display toast 
import { ToastrModule } from 'toastr-ng2';

import { ReactiveFormsModule } from '@angular/forms';


import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { Account2Component } from './components/account2/account2.component';
import { AccountFormComponent } from './components/account2/account2-form.component';


//custom controls
import { SppcMaskTextBox } from './controls/sppc-mask-textbox';
import { SppcNumberBox } from './controls/sppc-numberbox';
import { SppcGridColumn } from './controls/sppc-grid-column';

import {BrowserModule} from "@angular/platform-browser";
import { TranslateModule } from "ng2-translate";
import { GridModule } from '@progress/kendo-angular-grid';
import { RTL } from '@progress/kendo-angular-l10n';
import { DialogModule } from '@progress/kendo-angular-dialog';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { InputsModule } from '@progress/kendo-angular-inputs';

import { ColumnComponent } from "@progress/kendo-angular-grid";
//import {  } from "@progress/kendo-angular-grid/dist/es/columns/column-base";
//import { ColumnBase } from '@progress/kendo-angular-grid/dist/es/columns/column-base';
import { AccountService, TransactionLineService, FiscalPeriodService } from './service/index';



@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,       
        Account2Component,
        AccountFormComponent,
        SppcMaskTextBox,
        SppcNumberBox,
        SppcGridColumn
        
    ],
    providers: [AccountService, TransactionLineService, FiscalPeriodService,
        { provide: LocationStrategy, useClass: HashLocationStrategy }, { provide: RTL, useValue: true }],        
    imports: [
        CommonModule,         
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        BrowserAnimationsModule,
        ToastrModule.forRoot(),
        DialogModule, DropDownsModule, GridModule, InputsModule, 
        BrowserModule,
        TranslateModule.forRoot(),
        RouterModule.forRoot([
            { path: '', redirectTo: 'account2', pathMatch: 'full' },            
            { path: 'account2', component: Account2Component },
            { path: '**', redirectTo: 'account' }
        ])        
    ],
    schemas: [NO_ERRORS_SCHEMA]
})
export class AppModuleShared {
}
