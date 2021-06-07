import { NgModule, ModuleWithProviders, ErrorHandler } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MessageService } from '@progress/kendo-angular-l10n';
import { GridModule, ExcelModule, PDFModule } from '@progress/kendo-angular-grid';
import { DialogModule } from '@progress/kendo-angular-dialog';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { CalendarModule } from '@progress/kendo-angular-dateinputs';
import { TreeViewModule } from '@progress/kendo-angular-treeview';
import { PopupModule } from '@progress/kendo-angular-popup';
import { ContextMenuModule } from '@progress/kendo-angular-menu';
import { LayoutModule } from '@progress/kendo-angular-layout';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { ToastrModule } from 'ngx-toastr';
import { DpDatePickerModule } from 'ng2-jalali-date-picker';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';


import { SharedRoutingModule } from '@sppc/shared/shared-routing.module';

import { BrowserStorageService, DashboardService, GridService, LookupService, MetaDataService, ReportingService,AdvanceFilterService } from '@sppc/shared/services/index';

import {
  SppcGridColumn, SppcAutoGridColumn, SppcGridReorder, SppcAutoGeneratedGridReorder, SppcAutoGeneratedGridResize, SppcGridResize, SppcCheckAccess,
  SppcViewTreeConfig
} from '@sppc/shared/directive/index';

import { GridSettingComponent } from '@sppc/shared/directive/grid/component/grid-setting.component';
import { AutoGeneratedGridSettingComponent } from '@sppc/shared/directive/grid/component/auto-generated-grid-setting.component';
import { SelectFormGridSettingComponent } from '@sppc/shared/directive/grid/component/selectForm-grid-setting.component';

import { SppcDatePipe } from '@sppc/shared/pipes/sppc.date.pipe';
import { SppcNumConfigPipe } from '@sppc/shared/pipes/sppc.numConfig.pipe';
import { ReplaceLineBreaksPipe } from '@sppc/shared/pipes/sppc.replaceLineBreaks.pipe';

import { MetaDataResolver } from '@sppc/shared/class/metadata/metadata.resolver';
import { DefaultComponent } from '@sppc/shared/class/default.component';
import { AutoGeneratedGridComponent } from '@sppc/shared/class/autoGeneratedGrid.component';
import { GridExplorerComponent } from '@sppc/shared/class/gridExplorer.component';
import { DialogComponent } from '@sppc/shared/class/dialog.component';

import { GridFilterComponent } from '@sppc/shared/directive/grid/component/grid-filter.component';

import { SppcBranchScope } from '@sppc/shared/controls/branchScope/sppc-branch-scope';
import { SppcDatepicker } from '@sppc/shared/controls/datepicker/sppc-datepicker';
import { SppcDateRangeSelector } from '@sppc/shared/controls/dateRangeSelector/sppc-dateRangeSelector';
import { SppcDropDownList } from '@sppc/shared/controls/dropdownlist/sppc-dropdownlist';
import { SppcDisplayFullAccountComponent } from '@sppc/shared/controls/fullAccount/sppc-display-fullAccount';
import { SppcFullAccountComponent } from '@sppc/shared/controls/fullAccount/sppc-fullAccount';
import { SppcGridDatepicker } from '@sppc/shared/controls/datepicker/sppc-grid-datepicker';
import { SppcGridDateFilter } from '@sppc/shared/controls/grid/spp-grid-date-filter';
import { FilterDatePickerDirective } from '@sppc/shared/controls/grid/spp-grid-date-filter';
import { SppcAutoGridFilter } from '@sppc/shared/controls/grid/sppc-auto-grid-filter';
import { SppcGridFilter } from '@sppc/shared/controls/grid/sppc-grid-filter';
import { SelectFormComponent } from '@sppc/shared/controls/selectForm/selectForm.component';
import { DynamicTabsDirective } from '@sppc/shared/controls/tabs/dynamic-tabs.directive';
import { TabComponent } from '@sppc/shared/controls/tabs/tab.component';
import { TabsComponent } from '@sppc/shared/controls/tabs/tabs.component';
import { SppcMaskTextBox } from '@sppc/shared/controls/textbox/sppc-mask-textbox';
import { SppcNumericInput } from '@sppc/shared/controls/textbox/sppc-numericInput';
import { SppcNumericTextBox } from '@sppc/shared/controls/textbox/sppc-numerictextbox';
import { SppcCodeLengthDirective } from '@sppc/shared/directive/Validator/Sppc-code-length-validator';
import { ConfirmEqualValidator } from '@sppc/shared/directive/Validator/confirm-equal-validator';
import { EditorFormTitleComponent } from '@sppc/shared/directive/editorForm/editor-title.component';
import { FullCodeDirective } from '@sppc/shared/directive/fullCode/fullCode.directive';
import { FullCodeTestDirective } from '@sppc/shared/directive/fullCode/fullCodeTest.directive';
import { SpccOnlyNumberDirective } from '@sppc/shared/directive/onlyNumber/sppc.onlyNumber';
import { SppcPermissionCheckDirective } from '@sppc/shared/directive/permission/sppc-permissionCheck';
import { SppcTimepicker } from '@sppc/shared/controls/timepicker/sppc-timepicker';



import { LayoutComponent } from '@sppc/shared/components/layout/layout.component';
import { DashboardComponent } from '@sppc/shared/components/dashboard/dashboard.component';
import { AppfooterComponent } from '@sppc/shared/components/appfooter/appfooter.component';
import { AppheaderComponent } from '@sppc/shared/components/appheader/appheader.component';
import { AppmenuComponent } from '@sppc/shared/components/appmenu/appmenu.component';
import { AppsettingComponent } from '@sppc/shared/components/appsetting/appsetting.component';
import { BreadCumbComponent } from '@sppc/shared/components/breadCumb/breadcrumb.component';
import { HomeComponent } from '@sppc/shared/components/home/home.component';
import { LoginComponent } from '@sppc/shared/components/login/login.component';
import { LoginCompleteComponent } from '@sppc/shared/components/login/login.complete.component';
import { LoginContainerComponent } from '@sppc/shared/components/login/login.container.component';
import { LogoutComponent } from '@sppc/shared/components/login/logout.component';
import { NavMenuComponent } from '@sppc/shared/components/navmenu/navmenu.component';
import { ReportManagementComponent } from '@sppc/shared/components/reportManagement/reportManagement.component';
import { QuickReportSettingComponent } from '@sppc/shared/components/reportManagement/QuickReport-Setting.component';
//import { ReportsQueries } from '@sppc/shared/components/reportManagement/reports.queries';
import { ReportParametersComponent } from '@sppc/shared/components/reportParameters/reportParameters.component';
import { ReportViewerComponent } from '@sppc/shared/components/reportViewer/reportViewer.component';
import { ReportParamComponent } from '@sppc/shared/components/viewIdentifier/reportParam.component';
import { ViewIdentifierComponent } from '@sppc/shared/components/viewIdentifier/view-identifier.component';
import { GridMessageService } from '@sppc/shared/services/grid-messages.service';
import { AdvanceFilterComponent } from '@sppc/shared/components/advanceFilter/advance-filter.component';
import { NotFoundComponent } from '@sppc/shared/components/notFound/notFound.component';
import { ClosingTmpComponent } from '@sppc/finance/components/operational/voucher/closing-tmp.component';
import { ErrorListComponent } from './components/errorList/errorList.component';
import { LeftActionToolbarComponent } from '@sppc/shared/components/toolbar/leftActionToolbar.component';
import { QuickReportPageSettingComponent } from './components/reportManagement/quick-report-page-setting.component';
import { BaseService } from './class';
import { ShortcutService } from './services/shortcut.service';
import { ShareDataService } from './services/share-data.service';


export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}

@NgModule({
  imports: [
    CommonModule,
    SharedRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    DialogModule, DropDownsModule, GridModule, InputsModule, CalendarModule, ExcelModule, PDFModule ,
    ButtonsModule,
    BrowserAnimationsModule,
    PopupModule, TreeViewModule, ContextMenuModule,
    LayoutModule,
    ToastrModule.forRoot(
      {
        preventDuplicates: true,
        toastClass:'toast toastr-rtl'
      }),
    DpDatePickerModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
  ],

  declarations: [LayoutComponent, DashboardComponent, AppfooterComponent, AppheaderComponent, AppmenuComponent, AppsettingComponent, BreadCumbComponent, HomeComponent, LoginComponent,
    LoginCompleteComponent, LoginContainerComponent, LogoutComponent, NavMenuComponent, ReportManagementComponent, QuickReportSettingComponent, ReportParametersComponent,
    ReportViewerComponent, ReportParamComponent, ViewIdentifierComponent, SppcGridColumn, SppcAutoGridColumn, SppcGridReorder, SppcAutoGeneratedGridReorder, SppcAutoGeneratedGridResize,
    SppcGridResize, SppcCheckAccess, SppcViewTreeConfig, GridSettingComponent, AutoGeneratedGridSettingComponent, SelectFormGridSettingComponent, SppcDatePipe, SppcNumConfigPipe,
    ReplaceLineBreaksPipe, DialogComponent, GridFilterComponent, SppcBranchScope, SppcDatepicker, SppcDateRangeSelector, SppcDropDownList, SppcDisplayFullAccountComponent,
    SppcFullAccountComponent, SppcGridDatepicker, SppcGridDateFilter, FilterDatePickerDirective, SppcAutoGridFilter, SppcGridFilter, SelectFormComponent, DynamicTabsDirective,
    TabComponent, TabsComponent, SppcMaskTextBox, SppcNumericInput, SppcNumericTextBox, SppcCodeLengthDirective, ConfirmEqualValidator, EditorFormTitleComponent, FullCodeDirective,
    FullCodeTestDirective, SpccOnlyNumberDirective, SppcPermissionCheckDirective, SppcTimepicker, AdvanceFilterComponent,
    NotFoundComponent, ClosingTmpComponent, ErrorListComponent, LeftActionToolbarComponent, QuickReportPageSettingComponent
  ],

  entryComponents: [TabComponent, SppcGridDatepicker, SelectFormComponent, AdvanceFilterComponent,ErrorListComponent,QuickReportPageSettingComponent],

  //providers: [BrowserStorageService, DashboardService, GridService, LookupService, MetaDataService, ReportingService, 
  //  { provide: MessageService, useClass: GridMessageService },
  //  MetaDataResolver, DefaultComponent, AutoGeneratedGridComponent, GridExplorerComponent,
  //  DatePipe
  //],

  exports: [CommonModule, LayoutComponent, SppcGridColumn, SppcAutoGridColumn, SppcGridReorder, SppcAutoGeneratedGridReorder, SppcAutoGeneratedGridResize, SppcGridResize, SppcCheckAccess,
    SppcViewTreeConfig, GridSettingComponent, AutoGeneratedGridSettingComponent, SelectFormGridSettingComponent, SppcDatePipe, SppcNumConfigPipe, ReplaceLineBreaksPipe,
    FormsModule, ReactiveFormsModule, HttpClientModule, DialogModule, DropDownsModule, GridModule, InputsModule, CalendarModule, ButtonsModule, BrowserAnimationsModule,
    PopupModule, TreeViewModule, ContextMenuModule, LayoutModule, ToastrModule, DpDatePickerModule, TranslateModule, DashboardComponent, NavMenuComponent, GridFilterComponent,
    SppcBranchScope, SppcDatepicker, SppcDateRangeSelector, SppcDropDownList, SppcDisplayFullAccountComponent, SppcFullAccountComponent, SppcGridDatepicker, SppcGridDateFilter,
    FilterDatePickerDirective, SppcAutoGridFilter, SppcGridFilter, SelectFormComponent, DynamicTabsDirective, TabComponent, TabsComponent, SppcMaskTextBox, SppcNumericInput,
    SppcNumericTextBox, SppcCodeLengthDirective, ConfirmEqualValidator, EditorFormTitleComponent, FullCodeDirective, FullCodeTestDirective, SpccOnlyNumberDirective,
    SppcPermissionCheckDirective, BreadCumbComponent, ViewIdentifierComponent, ReportManagementComponent, QuickReportSettingComponent, ReportParametersComponent, ReportParamComponent,
    ReportViewerComponent, SppcTimepicker, NotFoundComponent, ClosingTmpComponent, ErrorListComponent, LeftActionToolbarComponent, ExcelModule, PDFModule, QuickReportPageSettingComponent
  ]
})
export class SharedModule {
  static forRoot(): ModuleWithProviders { 
    // Forcing the whole app to use the returned providers from the AppModule only.
    return {
      ngModule: SharedModule,
      //providers: [ /* All of your services here. It will hold the services needed by `itself`. */]
      providers: [BrowserStorageService, DashboardService, GridService, LookupService, MetaDataService, ReportingService, AdvanceFilterService,
        { provide: MessageService, useClass: GridMessageService },
        MetaDataResolver, DefaultComponent, AutoGeneratedGridComponent, GridExplorerComponent,
        DatePipe, ShortcutService, ShareDataService
      ]
    };
  }
}
