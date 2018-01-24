import { NgModule, NO_ERRORS_SCHEMA, forwardRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Headers, RequestOptions, BaseRequestOptions, Http } from '@angular/http';
import { APP_BASE_HREF, CommonModule, Location, LocationStrategy, HashLocationStrategy } from '@angular/common';
// third party module to display toast 
import { ToastrModule } from 'toastr-ng2';

import { ReactiveFormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';


import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { Account2Component } from './components/account2/account2.component';
import { AccountFormComponent } from './components/account2/account2-form.component';


//custom controls
import { SppcMaskTextBox } from './controls/textbox/sppc-mask-textbox';
import { SppcNumberBox } from './controls/textbox/sppc-numberbox';
import { SppcDropDownList } from './controls/dropdownlist/sppc-dropdownlist';

import {BrowserModule} from "@angular/platform-browser";
import { TranslateModule, TranslateLoader, TranslateStaticLoader } from "ng2-translate";
import { GridModule } from '@progress/kendo-angular-grid';
import { RTL, MessageService } from '@progress/kendo-angular-l10n';
import { DialogModule } from '@progress/kendo-angular-dialog';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { InputsModule } from '@progress/kendo-angular-inputs';

import { AccountService, TransactionLineService, FiscalPeriodService , GridMessageService } from './service/index';
import { SppcGridColumn } from "./directive/grid/sppc-grid-column";
import { SppcNumericFilter } from './controls/grid/sppc-numeric-filter';
import { SppcStringFilter } from './controls/grid/sppc-string-filter';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,       
        Account2Component,
        AccountFormComponent,
        SppcMaskTextBox,
        SppcNumberBox,
        SppcDropDownList,
        SppcGridColumn,
        SppcNumericFilter,
        SppcStringFilter,
    ],
    providers: [AccountService, TransactionLineService, FiscalPeriodService,
        { provide: LocationStrategy, useClass: HashLocationStrategy }, { provide: RTL, useValue: true },
        { provide: MessageService, useClass: GridMessageService }        
    ],        
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

