import { NgModule, NO_ERRORS_SCHEMA, forwardRef } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { Headers, RequestOptions, BaseRequestOptions, Http } from '@angular/http';
import { APP_BASE_HREF, CommonModule, Location, LocationStrategy, HashLocationStrategy, DatePipe } from '@angular/common';
// third party module to display toast 
import { ToastrModule } from 'ngx-toastr';
import { TreeViewModule } from '@progress/kendo-angular-treeview';

//import { Ng4LoadingSpinnerModule } from 'ng4-loading-spinner';

import { ReactiveFormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';
import { TextMaskModule } from 'angular2-text-mask';


import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { AccountComponent } from './components/account/account.component';
import { AccountFormComponent } from './components/account/account-form.component';
import { VoucherComponent } from './components/voucher/voucher.component';
import { VoucherFormComponent } from './components/voucher/voucher-form.component';
import { VoucherLineComponent } from './components/voucherLine/voucherLine.component';
import { VoucherLineFormComponent } from './components/voucherLine/voucherLine-form.component'
import { UserComponent } from './components/user/user.component';
import { UserFormComponent } from './components/user/user-form.component';
import { RoleComponent } from './components/role/role.component';
import { RoleFormComponent } from './components/role/role-form.component';
import { RoleUserFormComponent } from './components/role/role-user-form.component';
import { RoleBranchFormComponent } from './components/role/role-branch-form.component';
import { RoleDetailFormComponent } from './components/role/role-detail-form.component';
import { ChangePasswordComponent } from './components/user/changePassword.component';
import { DetailAccountComponent } from './components/detailAccount/detailAccount.component';
import { DetailAccountFormComponent } from './components/detailAccount/detailAccount-form.component';
import { CostCenterComponent } from './components/costCenter/costCenter.component';
import { CostCenterFormComponent } from './components/costCenter/costCenter-form.component';
import { ProjectComponent } from './components/project/project.component';
import { ProjectFormComponent } from './components/project/project-form.component';

import { DpDatePickerModule } from 'ng2-jalali-date-picker';
import { ConfirmEqualValidator } from './directive/Validator/confirm-equal-validator';


import { Layout } from './enviroment';

//custom controls
import { SppcMaskTextBox } from './controls/textbox/sppc-mask-textbox';
import { SppcNumberBox } from './controls/textbox/sppc-numberbox';
import { SppcDropDownList } from './controls/dropdownlist/sppc-dropdownlist';
import { SppcDatepicker } from './controls/datepicker/sppc-datepicker';
import { SppcFullAccount } from './controls/fullAccount/sppc-fullAccount'

import { BrowserModule } from "@angular/platform-browser";
import { TranslateModule, TranslateLoader, TranslateStaticLoader } from "ng2-translate";
import { GridModule } from '@progress/kendo-angular-grid';
import { RTL, MessageService } from '@progress/kendo-angular-l10n';

import { DialogModule } from '@progress/kendo-angular-dialog';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { CalendarModule } from '@progress/kendo-angular-dateinputs';

import {
    AccountService, VoucherLineService, FiscalPeriodService, GridMessageService, CompanyService, UserService, RoleService, DetailAccountService, CostCenterService,
    BranchService, VoucherService, LookupService, FullAccountService, ProjectService

} from './service/index';
import { SppcGridColumn } from "./directive/grid/sppc-grid-column";

import { SppcGridFilter } from './controls/grid/sppc-grid-filter';


//import { Context } from "./components/login/login.component";
import { LoginComponent } from "./components/login/login.component";
import { LoginCompleteComponent } from "./components/login/login.complete.component";
import { LoginContainerComponent } from "./components/login/login.container.component";
import { LogoutComponent } from "./components/login/logout.component";

import { AuthenticationService, AuthGuard } from "./service/login/index";

import { SppcDatePipe } from "./pipes/index"
import { MetaDataService } from './service/metadata/metadata.service';
import { BaseService } from './class/base.service';
import { SppcLoadingComponent, SppcLoadingService } from './controls/sppcLoading/index';




@NgModule({
    declarations: [
        AppComponent,
        SppcLoadingComponent,
        NavMenuComponent,
        AccountComponent,
        AccountFormComponent,        
        LoginComponent,
        LoginCompleteComponent,
        LoginContainerComponent,
        LogoutComponent,
        SppcMaskTextBox,
        SppcNumberBox,
        SppcDropDownList,
        SppcDatepicker,
        SppcFullAccount,
        SppcGridColumn,
        SppcGridFilter,
        VoucherComponent,
        VoucherFormComponent,
        VoucherLineComponent,
        VoucherLineFormComponent,
        UserComponent,
        UserFormComponent,
        RoleComponent,
        RoleFormComponent,
        RoleUserFormComponent,
        RoleBranchFormComponent,
        RoleDetailFormComponent,
        ChangePasswordComponent,
        DetailAccountComponent,
        DetailAccountFormComponent,
        CostCenterComponent,
        ProjectComponent,
        ProjectFormComponent,
        CostCenterFormComponent,
        ConfirmEqualValidator,
        SppcDatePipe        

    ],
    providers: [AccountService, VoucherLineService, FiscalPeriodService, BranchService, CompanyService, VoucherService, LookupService, MetaDataService, SppcLoadingService,
        UserService, RoleService, FullAccountService, DetailAccountService, CostCenterService, ProjectService,
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
        //Ng4LoadingSpinnerModule.forRoot(),
        ButtonsModule,
        CommonModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        BrowserAnimationsModule,
        ToastrModule.forRoot(),
        DialogModule, DropDownsModule, GridModule, InputsModule, CalendarModule,
        BrowserModule,
        DpDatePickerModule,
        TextMaskModule,
        TreeViewModule,
        TranslateModule.forRoot(),
        RouterModule.forRoot([
            { path: '', redirectTo: 'login', pathMatch: 'full' },
            { path: 'account', component: AccountComponent, canActivate: [AuthGuard] },
            { path: 'login', component: LoginContainerComponent },
            { path: 'logout', component: LogoutComponent },
            { path: 'voucher', component: VoucherComponent, canActivate: [AuthGuard] },
            { path: 'users', component: UserComponent, canActivate: [AuthGuard] },
            { path: 'roles', component: RoleComponent, canActivate: [AuthGuard] },
            { path: 'changePassword', component: ChangePasswordComponent, canActivate: [AuthGuard] },
            { path: 'detailAccount', component: DetailAccountComponent, canActivate: [AuthGuard] },
            { path: 'costCenter', component: CostCenterComponent, canActivate: [AuthGuard] },
            { path: 'projects', component: ProjectComponent, canActivate: [AuthGuard] },
            { path: '**', redirectTo: 'account' }
        ])
    ],
    schemas: [NO_ERRORS_SCHEMA]
})
export class AppModuleShared {
}

