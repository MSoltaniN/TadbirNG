import { NgModule, NO_ERRORS_SCHEMA, forwardRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Headers, RequestOptions, BaseRequestOptions, Http } from '@angular/http';
import { APP_BASE_HREF, CommonModule, Location, LocationStrategy, HashLocationStrategy, DatePipe } from '@angular/common';
// third party module to display toast 
import { ToastrModule } from 'toastr-ng2';

import { ReactiveFormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';


import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { Account2Component } from './components/account2/account2.component';
import { AccountFormComponent } from './components/account2/account2-form.component';
import { TransactionComponent } from './components/transaction/transaction.component';
import { TransactionFormComponent } from './components/transaction/transaction-form.component';

import { DpDatePickerModule } from 'ng2-jalali-date-picker';


import { Layout } from './enviroment';

//custom controls
import { SppcMaskTextBox } from './controls/textbox/sppc-mask-textbox';
import { SppcNumberBox } from './controls/textbox/sppc-numberbox';
import { SppcDropDownList } from './controls/dropdownlist/sppc-dropdownlist';
import { SppcDatepicker } from './controls/datepicker/sppc-datepicker'

import {BrowserModule} from "@angular/platform-browser";
import { TranslateModule, TranslateLoader, TranslateStaticLoader } from "ng2-translate";
import { GridModule } from '@progress/kendo-angular-grid';
import { RTL, MessageService } from '@progress/kendo-angular-l10n';
import { DialogModule } from '@progress/kendo-angular-dialog';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { CalendarModule } from '@progress/kendo-angular-dateinputs';

import { AccountService, TransactionLineService, FiscalPeriodService, GridMessageService, CompanyService, BranchService, TransactionService } from './service/index';
import { SppcGridColumn } from "./directive/grid/sppc-grid-column";
import { SppcNumericFilter } from './controls/grid/sppc-numeric-filter';
import { SppcStringFilter } from './controls/grid/sppc-string-filter';

//import { Context } from "./components/login/login.component";
import { LoginComponent } from "./components/login/login.component";
import { LoginCompleteComponent } from "./components/login/login.complete.component";
import { LoginContainerComponent } from "./components/login/login.container.component";
import { LogoutComponent } from "./components/login/logout.component";

import { AuthenticationService, AuthGuard } from "./service/login/index";

import { SppcDatePipe } from "./pipes/index"


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,       
        Account2Component,
        AccountFormComponent,
        LoginComponent,
        LoginCompleteComponent,
        LoginContainerComponent,
        LogoutComponent,
        SppcMaskTextBox,
        SppcNumberBox,
        SppcDropDownList,
        SppcDatepicker,
        SppcGridColumn,
        SppcNumericFilter,
        SppcStringFilter,
        TransactionComponent,
        TransactionFormComponent,
        SppcDatePipe
    ],
    providers: [AccountService, TransactionLineService, FiscalPeriodService, BranchService, CompanyService, TransactionService,
        { provide: LocationStrategy, useClass: HashLocationStrategy },
        //{
        //    provide: RTL,
        //    useFactory: function () {
        //        var lang = localStorage.getItem('lang');
        //        if (lang == "en") {
        //            return false;
        //        } 
        //        else 
        //            return true;
        //    }            
        //},
        { provide: MessageService, useClass: GridMessageService },
        AuthGuard,        
        AuthenticationService,
        DatePipe,
        Layout
    ],        
    imports: [
        CommonModule,         
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        BrowserAnimationsModule,
        ToastrModule.forRoot(),
        DialogModule, DropDownsModule, GridModule, InputsModule, CalendarModule,
        BrowserModule,
        DpDatePickerModule,
        TranslateModule.forRoot(),
        RouterModule.forRoot([
            { path: '', redirectTo: 'login', pathMatch: 'full' },            
            { path: 'account2', component: Account2Component, canActivate: [AuthGuard]},
            { path: 'login', component: LoginContainerComponent },
            { path: 'logout', component: LogoutComponent },
            { path: 'transaction', component: TransactionComponent, canActivate: [AuthGuard] },
            { path: '**', redirectTo: 'account' }
        ])        
    ],
    schemas: [NO_ERRORS_SCHEMA]
})
export class AppModuleShared {
}

