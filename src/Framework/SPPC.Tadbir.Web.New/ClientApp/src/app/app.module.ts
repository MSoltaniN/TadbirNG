
import { NgModule, NO_ERRORS_SCHEMA, forwardRef, ErrorHandler } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { Headers, RequestOptions, BaseRequestOptions, Http, HttpModule } from '@angular/http';
import { APP_BASE_HREF, CommonModule, Location, LocationStrategy, HashLocationStrategy, DatePipe } from '@angular/common';
// third party module to display toast 
import { ToastrModule } from 'ngx-toastr';
import { TreeViewModule } from '@progress/kendo-angular-treeview';
import { ReactiveFormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';
import { TextMaskModule } from 'angular2-text-mask';
import { PopupModule } from '@progress/kendo-angular-popup';
import { ContextMenuModule } from '@progress/kendo-angular-menu';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { AccountComponent } from './components/account/account.component';
import { AccountFormComponent } from './components/account/account-form.component';
import { VoucherComponent } from './components/voucher/voucher.component';
import { VoucherEditorComponent } from './components/voucher/voucher-editor.component';
import { VoucherLineComponent } from './components/voucherLine/voucherLine.component';
import { VoucherLineFormComponent } from './components/voucherLine/voucherLine-form.component'
import { UserComponent } from './components/user/user.component';
import { UserFormComponent } from './components/user/user-form.component';
import { UserRolesFormComponent } from './components/user/user-roles-form.component';
import { RoleComponent } from './components/role/role.component';
import { RoleFormComponent } from './components/role/role-form.component';
import { RoleUserFormComponent } from './components/role/role-user-form.component';
import { RoleBranchFormComponent } from './components/role/role-branch-form.component';
import { RoleFiscalPeriodFormComponent } from './components/role/role-fiscalPeriod-form.component';
import { RoleDetailFormComponent } from './components/role/role-detail-form.component';
import { ChangePasswordComponent } from './components/user/changePassword.component';
import { DetailAccountComponent } from './components/detailAccount/detailAccount.component';
import { DetailAccountFormComponent } from './components/detailAccount/detailAccount-form.component';
import { CostCenterComponent } from './components/costCenter/costCenter.component';
import { CostCenterFormComponent } from './components/costCenter/costCenter-form.component';
import { ProjectComponent } from './components/project/project.component';
import { ProjectFormComponent } from './components/project/project-form.component';
import { FiscalPeriodComponent } from './components/fiscalPeriod/fiscalPeriod.component';
import { FiscalPeriodFormComponent } from './components/fiscalPeriod/fiscalPeriod-form.component';
import { FiscalPeriodRolesFormComponent } from './components/fiscalPeriod/fiscalPeriod-roles-form.component';
import { BranchComponent } from './components/branch/branch.component';
import { BranchFormComponent } from './components/branch/branch-form.component';
import { BranchRolesFormComponent } from './components/branch/branch-roles-form.component';
import { CompanyComponent } from './components/company/company.component';
import { CompanyFormComponent } from './components/company/company-form.component';
import { AccountRelationsComponent } from './components/accountRelations/accountRelations.component';
import { AccountRelationsFormComponent } from './components/accountRelations/accountRelations-form.component';
import { SettingsComponent } from './components/settings/settings.component';
import { SettingsFormComponent } from './components/settings/settings-form.component';
import { ViewRowPermissionComponent } from './components/viewRowPermission/viewRowPermission.component';
import { ViewRowPermissionSingleFormComponent } from './components/viewRowPermission/viewRowPermission-single-form.component';
import { ViewRowPermissionMultipleFormComponent } from './components/viewRowPermission/viewRowPermission-multiple-form.component';
import { OperationLogsComponent } from './components/operationLogs/operationLogs.component';
import { OperationLogsDetailComponent } from './components/operationLogs/operationLogs-detail.component';
import { EditorFormTitleComponent } from './directive/editorForm/editor-title.component';
import { ViewTreeConfigComponent } from './components/viewTreeConfig/viewTreeConfig.component';
import { AccountGroupsComponent } from './components/accountGroups/accountGroups.component';
import { AccountGroupsFormComponent } from './components/accountGroups/accountGroups-form.component';
//import { InlineTestComponent } from './components/inlineTest/inlineTest.component';
import { RelatedAccountsComponent } from './components/relatedAccounts/relatedAccounts.component';
import { RelatedAccountsFormComponent } from './components/relatedAccounts/relatedAccounts-form.component';
import { JournalComponent } from './components/journal/journal.component';
import { HomeComponent } from './components/home/home.component';
import { AccountBookComponent } from './components/accountBook/accountBook.component';

import { DialogComponent } from './class/dialog.component';

import { DpDatePickerModule } from 'ng2-jalali-date-picker';
import { ConfirmEqualValidator } from './directive/Validator/confirm-equal-validator';
import { FullCodeDirective } from './directive/fullCode/fullCode.directive';
import { FullCodeTestDirective } from './directive/fullCode/fullCodeTest.directive';
import { SppcCodeLengthDirective } from './directive/Validator/Sppc-code-length-validator';
import { SpccOnlyNumberDirective } from './directive/onlyNumber/sppc.onlyNumber';

import { Layout } from '../environments/environment';

//custom controls
import { SppcMaskTextBox } from './controls/textbox/sppc-mask-textbox';
import { SppcNumericTextBox } from './controls/textbox/sppc-numerictextbox';
import { SppcNumericInput } from './controls/textbox/sppc-numericInput';
import { SppcDropDownList } from './controls/dropdownlist/sppc-dropdownlist';
import { SppcDatepicker } from './controls/datepicker/sppc-datepicker';
import { SppcDateRangeSelector } from './controls/dateRangeSelector/sppc-dateRangeSelector';
import { SppcFullAccountComponent } from './controls/fullAccount/sppc-fullAccount';
import { SppcDisplayFullAccountComponent } from './controls/fullAccount/sppc-display-fullAccount';
import { SppcBranchScope } from './controls/branchScope/sppc-branch-scope';
import { SelectFormComponent } from './controls/selectForm/selectForm.component';

import { BrowserModule } from "@angular/platform-browser";
//import { TranslateModule, TranslateLoader, TranslateStaticLoader } from "ng2-translate";
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { GridModule } from '@progress/kendo-angular-grid';
import { RTL, MessageService } from '@progress/kendo-angular-l10n';

import { DialogModule } from '@progress/kendo-angular-dialog';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { CalendarModule } from '@progress/kendo-angular-dateinputs';

import { HotkeyModule } from 'angular2-hotkeys';

import {
  AccountService, VoucherLineService, FiscalPeriodService, GridMessageService, CompanyService, UserService, RoleService, DetailAccountService, CostCenterService,
  BranchService, VoucherService, LookupService, FullAccountService, ProjectService, AccountRelationsService, SettingService, ViewRowPermissionService, FullCodeService,
  OperationLogService, DashboardService, AccountGroupsService, AccountCollectionService, GridService
} from './service/index';
import { SppcGridColumn } from "./directive/grid/sppc-grid-column";
import { SppcGridReorder } from "./directive/grid/sppc-grid-reorder";
import { SppcAutoGeneratedGridReorder } from './directive/grid/sppc-auto-generated-grid-reorder';
import { SppcAutoGeneratedGridResize } from './directive/grid/sppc-auto-generated-grid-resize';
import { SppcGridFilter } from './controls/grid/sppc-grid-filter';


//import { Context } from "./components/login/login.component";
import { LoginComponent } from "./components/login/login.component";
import { LoginCompleteComponent } from "./components/login/login.complete.component";
import { LoginContainerComponent } from "./components/login/login.container.component";
import { LogoutComponent } from "./components/login/logout.component";

import { AuthenticationService, AuthGuard } from "./service/login/index";

import { SppcDatePipe, SppcNumConfigPipe } from "./pipes/index"
import { ReplaceLineBreaksPipe } from './pipes/sppc.replaceLineBreaks.pipe';
import { MetaDataService } from './service/metadata/metadata.service';
import { BaseService } from './class/base.service';
import { SppcLoadingComponent, SppcLoadingService } from './controls/sppcLoading/index';
import { SppcGridResize } from './directive/grid/sppc-grid-resize';
import { GridSettingComponent } from './directive/grid/component/grid-setting.component';
import { AutoGeneratedGridSettingComponent } from './directive/grid/component/auto-generated-grid-setting.component';
import { SelectFormGridSettingComponent } from './directive/grid/component/selectForm-grid-setting.component';
import { SppcCheckAccess } from './directive/grid/sppc-check-access';
import { SppcViewTreeConfig } from './directive/grid/sppc-viewTree-config';
import { LocalizationService } from "@progress/kendo-angular-l10n";
import { EditService } from '@progress/kendo-angular-grid/dist/es2015/editing/edit.service';
import { EnviromentComponent } from './class/enviroment.component';
import { Permissions } from './security/permissions';
import { SppcGridDateFilter, FilterDatePickerDirective } from './controls/grid/spp-grid-date-filter';
import { SppcGridDatepicker } from './controls/datepicker/sppc-grid-datepicker';
import { GeneralErrorHandler } from './class/error.handler';
import { HttpClientModule, HttpClient } from '@angular/common/http';


import { NgProgressModule } from '@ngx-progressbar/core';
import { NgProgressHttpModule } from '@ngx-progressbar/http';
import { NgProgressRouterModule } from '@ngx-progressbar/router';
import { GridFilterComponent } from './directive/grid/component/grid-filter.component';
import { DefaultComponent } from './class/default.component';
import { AutoGeneratedGridComponent } from './class/autoGeneratedGrid.component';
import { GridExplorerComponent } from './class/gridExplorer.component';
import { AppmenuComponent } from './components/appmenu/appmenu.component';
import { AppheaderComponent } from './components/appheader/appheader.component';
import { AppfooterComponent } from './components/appfooter/appfooter.component';
import { AppsettingComponent } from './components/appsetting/appsetting.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { BreadCumbComponent } from './components/breadCumb/breadcrumb.component';
import { ReportViewerComponent } from './components/reportViewer/reportViewer.component';
import { ReportBaseService } from './class/report.base.service';
import { ReportingService } from './service/report/reporting.service';
import { ReportManagementComponent } from "./components/reportManagement/reportManagement.component";
import { AccountCollectionComponent } from './components/accountCollection/accountCollection.component';
import { ReportParametersComponent } from './components/reportParameters/reportParameters.component';
import { LayoutModule } from '@progress/kendo-angular-layout';
import { ViewIdentifierComponent } from './components/viewIdentifier/view-identifier.component';
import { ReportParamComponent } from './components/viewIdentifier/reportParam.component';
import { TabComponent } from './controls/tabs/tab.component';
import { TabsComponent } from './controls/tabs/tabs.component';
import { DynamicTabsDirective } from './controls/tabs/dynamic-tabs.directive';
import { from } from 'rxjs/observable/from';
import { ReportMGComponent } from './components/viewIdentifier/report.mg.component';



export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}

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
    SppcNumericTextBox,
    SppcNumericInput,
    SppcDropDownList,
    SppcDatepicker,
    SppcDateRangeSelector,
    SppcFullAccountComponent,
    SppcDisplayFullAccountComponent,
    SppcGridColumn,
    SppcGridReorder,
    SppcAutoGeneratedGridReorder,
    SppcAutoGeneratedGridResize,
    SppcGridResize,
    SppcGridFilter,
    SppcBranchScope,
    VoucherComponent,
    VoucherEditorComponent,
    VoucherLineComponent,
    VoucherLineFormComponent,
    UserComponent,
    UserFormComponent,
    UserRolesFormComponent,
    RoleComponent,
    RoleFormComponent,
    GridSettingComponent,
    AutoGeneratedGridSettingComponent,
    SelectFormGridSettingComponent,
    GridFilterComponent,
    RoleUserFormComponent,
    RoleBranchFormComponent,
    RoleFiscalPeriodFormComponent,
    RoleDetailFormComponent,
    ChangePasswordComponent,
    DetailAccountComponent,
    DetailAccountFormComponent,
    CostCenterComponent,
    ProjectComponent,
    ProjectFormComponent,
    CostCenterFormComponent,
    ConfirmEqualValidator,
    FullCodeDirective,
    FullCodeTestDirective,
    SppcCodeLengthDirective,
    SpccOnlyNumberDirective,
    FiscalPeriodComponent,
    FiscalPeriodFormComponent,
    FiscalPeriodRolesFormComponent,
    BranchComponent,
    BranchFormComponent,
    BranchRolesFormComponent,
    CompanyComponent,
    CompanyFormComponent,
    SppcDatePipe,
    SppcNumConfigPipe,
    ReplaceLineBreaksPipe,
    SppcCheckAccess,
    SppcViewTreeConfig,
    AccountRelationsComponent,
    AccountRelationsFormComponent,
    SettingsComponent,
    SettingsFormComponent,
    ViewRowPermissionComponent,
    ViewRowPermissionSingleFormComponent,
    ViewRowPermissionMultipleFormComponent,
    OperationLogsComponent,
    OperationLogsDetailComponent,
    EditorFormTitleComponent,
    ViewTreeConfigComponent,
    SppcGridDateFilter,
    FilterDatePickerDirective,
    SppcGridDatepicker,
    AppmenuComponent,
    AppheaderComponent,
    AppfooterComponent,
    AppsettingComponent,
    DashboardComponent,
    BreadCumbComponent,
    ReportViewerComponent,
    AccountGroupsComponent,
    AccountGroupsFormComponent,
    ReportManagementComponent,
    RelatedAccountsComponent,
    RelatedAccountsFormComponent,
    DialogComponent,
    AccountCollectionComponent,
    DialogComponent,
    JournalComponent,
    HomeComponent,
    AccountBookComponent,
    ReportParametersComponent,
    ViewIdentifierComponent,
    ReportParamComponent,
    TabComponent,
    TabsComponent,
    DynamicTabsDirective,
    ReportMGComponent,
    SelectFormComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    ButtonsModule,
    CommonModule,
    HttpModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    PopupModule,
    ContextMenuModule,
    ToastrModule.forRoot({ preventDuplicates: true }),
    HotkeyModule.forRoot(),
    DialogModule, DropDownsModule, GridModule, InputsModule, CalendarModule,
    DpDatePickerModule,
    TextMaskModule,
    TreeViewModule,
    HttpClientModule,
    NgProgressModule.forRoot({
      direction: 'ltr+',
      spinnerPosition: 'left',
      color: 'white',
      thick: true,
      meteor: false
    }),
    NgProgressRouterModule,
    NgProgressHttpModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
    RouterModule.forRoot([
      { path: '', redirectTo: 'login', pathMatch: 'full' },
      { path: 'account', component: AccountComponent, canActivate: [AuthGuard] },
      { path: 'login', component: LoginContainerComponent },
      { path: 'logout', component: LogoutComponent },
      { path: 'voucher', component: VoucherComponent, canActivate: [AuthGuard] },
      { path: 'vouchers/:mode', component: VoucherEditorComponent, canActivate: [AuthGuard] },
      { path: 'users', component: UserComponent, canActivate: [AuthGuard] },
      { path: 'roles', component: RoleComponent, canActivate: [AuthGuard] },
      { path: 'changePassword', component: ChangePasswordComponent, canActivate: [AuthGuard] },
      { path: 'detailAccount', component: DetailAccountComponent, canActivate: [AuthGuard] },
      { path: 'costCenter', component: CostCenterComponent, canActivate: [AuthGuard] },
      { path: 'projects', component: ProjectComponent, canActivate: [AuthGuard] },
      { path: 'fiscalperiod', component: FiscalPeriodComponent, canActivate: [AuthGuard] },
      { path: 'branches', component: BranchComponent, canActivate: [AuthGuard] },
      { path: 'companies', component: CompanyComponent, canActivate: [AuthGuard] },
      { path: 'accountrelations', component: AccountRelationsComponent, canActivate: [AuthGuard] },
      { path: 'settings', component: SettingsComponent, canActivate: [AuthGuard] },
      { path: 'viewRowPermission', component: ViewRowPermissionComponent, canActivate: [AuthGuard] },
      { path: 'operation-log', component: OperationLogsComponent, canActivate: [AuthGuard] },
      { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
      { path: 'account-groups', component: AccountGroupsComponent, canActivate: [AuthGuard] },
      { path: 'accounts/group/:groupid', component: RelatedAccountsComponent, canActivate: [AuthGuard] },
      { path: 'account-collection', component: AccountCollectionComponent, canActivate: [AuthGuard] },
      { path: 'journal', component: JournalComponent, canActivate: [AuthGuard] },
      { path: 'account-book', component: AccountBookComponent, canActivate: [AuthGuard] },
      { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
      { path: 'reports', component: ReportManagementComponent, canActivate: [AuthGuard] },
      //{ path: 'inlinetest', component: InlineTestComponent, canActivate: [AuthGuard] },
      { path: '**', redirectTo: 'dashboard' }
    ]),
    LayoutModule
  ],
  providers: [AccountService, VoucherLineService, FiscalPeriodService, BranchService, CompanyService, VoucherService, LookupService, MetaDataService, SppcLoadingService,
    UserService, RoleService, FullAccountService, DetailAccountService, CostCenterService, ProjectService, AccountRelationsService, SettingService, ViewRowPermissionService,
    FullCodeService, OperationLogService, DashboardService, ReportingService, AccountGroupsService, AccountCollectionService, GridService,
    { provide: LocationStrategy, useClass: HashLocationStrategy },
    { provide: ErrorHandler, useClass: GeneralErrorHandler },
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
    Layout,
    EnviromentComponent,
    Permissions,
    DefaultComponent,
    AutoGeneratedGridComponent,
    GridExplorerComponent
  ],
  schemas: [NO_ERRORS_SCHEMA],
  entryComponents: [
    SppcGridDatepicker, AccountFormComponent, CostCenterFormComponent, DetailAccountFormComponent, ProjectFormComponent, RelatedAccountsFormComponent, VoucherLineFormComponent,
    TabComponent, AccountGroupsFormComponent, VoucherEditorComponent, SelectFormComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
