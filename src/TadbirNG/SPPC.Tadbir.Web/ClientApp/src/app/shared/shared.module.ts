import { CommonModule, DatePipe } from "@angular/common";
import { HttpClient } from "@angular/common/http";
import { ModuleWithProviders, NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { TranslateLoader, TranslateModule } from "@ngx-translate/core";
import { TranslateHttpLoader } from "@ngx-translate/http-loader";
import { ButtonsModule } from "@progress/kendo-angular-buttons";
import { CalendarModule } from "@progress/kendo-angular-dateinputs";
import { DialogModule } from "@progress/kendo-angular-dialog";
import { DropDownsModule } from "@progress/kendo-angular-dropdowns";
import {
  ExcelModule,
  GridModule,
  PDFModule,
} from "@progress/kendo-angular-grid";
import { InputsModule } from "@progress/kendo-angular-inputs";
import { MessageService } from "@progress/kendo-angular-l10n";
import { LayoutModule } from "@progress/kendo-angular-layout";
import { ContextMenuModule } from "@progress/kendo-angular-menu";
import { PopupModule } from "@progress/kendo-angular-popup";
import { TreeViewModule } from "@progress/kendo-angular-treeview";
import { DpDatePickerModule } from "@aligorji/ng2-jalali-date-picker";

import { SharedRoutingModule } from "@sppc/shared/shared-routing.module";

import {
  AdvanceFilterService,
  BrowserStorageService,
  DashboardService,
  GridService,
  LicenseService,
  LookupService,
  MetaDataService,
  ReportingService,
} from "@sppc/shared/services/index";

import {
  InputDirective,
  SppcAutoGeneratedGridReorder,
  SppcAutoGeneratedGridResize,
  SppcAutoGridColumn,
  SppcCheckAccess,
  SppcGridColumn,
  SppcGridReorder,
  SppcGridResize,
  SppcViewTreeConfig,
} from "@sppc/shared/directive/index";

import { AutoGeneratedGridSettingComponent } from "@sppc/shared/directive/grid/component/auto-generated-grid-setting.component";
import { GridSettingComponent } from "@sppc/shared/directive/grid/component/grid-setting.component";
import { SelectFormGridSettingComponent } from "@sppc/shared/directive/grid/component/selectForm-grid-setting.component";

import { SppcDatePipe } from "@sppc/shared/pipes/sppc.date.pipe";
import { SppcNumConfigPipe } from "@sppc/shared/pipes/sppc.numConfig.pipe";
import { ReplaceLineBreaksPipe } from "@sppc/shared/pipes/sppc.replaceLineBreaks.pipe";

import { AutoGeneratedGridComponent } from "@sppc/shared/class/autoGeneratedGrid.component";
import { DefaultComponent } from "@sppc/shared/class/default.component";
import { DialogComponent } from "@sppc/shared/class/dialog.component";
import { GridExplorerComponent } from "@sppc/shared/class/gridExplorer.component";
import { MetaDataResolver } from "@sppc/shared/class/metadata/metadata.resolver";

import { GridFilterComponent } from "@sppc/shared/directive/grid/component/grid-filter.component";

import { SppcBranchScope } from "@sppc/shared/controls/branchScope/sppc-branch-scope";
import { SppcDatepicker } from "@sppc/shared/controls/datepicker/sppc-datepicker";
import { SppcGridDatepicker } from "@sppc/shared/controls/datepicker/sppc-grid-datepicker";
import { SppcDateRangeSelector } from "@sppc/shared/controls/dateRangeSelector/sppc-dateRangeSelector";
import { SppcDropDownList } from "@sppc/shared/controls/dropdownlist/sppc-dropdownlist";
import { SppcDisplayFullAccountComponent } from "@sppc/shared/controls/fullAccount/sppc-display-fullAccount";
import { SppcFullAccountComponent } from "@sppc/shared/controls/fullAccount/sppc-fullAccount";
import {
  FilterDatePickerDirective,
  SppcGridDateFilter,
} from "@sppc/shared/controls/grid/spp-grid-date-filter";
import { SppcAutoGridFilter } from "@sppc/shared/controls/grid/sppc-auto-grid-filter";
import { SppcGridFilter } from "@sppc/shared/controls/grid/sppc-grid-filter";
import { SelectFormComponent } from "@sppc/shared/controls/selectForm/selectForm.component";
import { DynamicTabsDirective } from "@sppc/shared/controls/tabs/dynamic-tabs.directive";
import { TabComponent } from "@sppc/shared/controls/tabs/tab.component";
import { TabsComponent } from "@sppc/shared/controls/tabs/tabs.component";
import { SppcMaskTextBox } from "@sppc/shared/controls/textbox/sppc-mask-textbox";
import { SppcNumericInput } from "@sppc/shared/controls/textbox/sppc-numericInput";
import { SppcNumericTextBox } from "@sppc/shared/controls/textbox/sppc-numerictextbox";
import { SppcTimepicker } from "@sppc/shared/controls/timepicker/sppc-timepicker";
import { EditorFormTitleComponent } from "@sppc/shared/directive/editorForm/editor-title.component";
import { FullCodeDirective } from "@sppc/shared/directive/fullCode/fullCode.directive";
import { FullCodeTestDirective } from "@sppc/shared/directive/fullCode/fullCodeTest.directive";
import { SpccOnlyNumberDirective } from "@sppc/shared/directive/onlyNumber/sppc.onlyNumber";
import { SppcPermissionCheckDirective } from "@sppc/shared/directive/permission/sppc-permissionCheck";
import { ConfirmEqualValidator } from "@sppc/shared/directive/Validator/confirm-equal-validator";
import { SppcCodeLengthDirective } from "@sppc/shared/directive/Validator/Sppc-code-length-validator";

import { AppfooterComponent } from "@sppc/shared/components/appfooter/appfooter.component";
import { AppheaderComponent } from "@sppc/shared/components/appheader/appheader.component";
import { AppmenuComponent } from "@sppc/shared/components/appmenu/appmenu.component";
import { AppsettingComponent } from "@sppc/shared/components/appsetting/appsetting.component";
import { BreadCumbComponent } from "@sppc/shared/components/breadCumb/breadcrumb.component";
import { DashboardComponent } from "@sppc/shared/components/dashboard/dashboard.component";
import { HomeComponent } from "@sppc/shared/components/home/home.component";
import { LayoutComponent } from "@sppc/shared/components/layout/layout.component";
import { LoginCompleteComponent } from "@sppc/shared/components/login/login.complete.component";
import { LoginComponent } from "@sppc/shared/components/login/login.component";
import { LoginContainerComponent } from "@sppc/shared/components/login/login.container.component";
import { LogoutComponent } from "@sppc/shared/components/login/logout.component";
import { NavMenuComponent } from "@sppc/shared/components/navmenu/navmenu.component";
import { QuickReportSettingComponent } from "@sppc/shared/components/reportManagement/QuickReport-Setting.component";
import { ReportManagementComponent } from "@sppc/shared/components/reportManagement/reportManagement.component";
//import { ReportsQueries } from '@sppc/shared/components/reportManagement/reports.queries';
import { GaugesModule } from "@progress/kendo-angular-gauges";
import { ClosingTmpComponent } from "@sppc/finance/components/operational/voucher/closing-tmp.component";
import { AdvanceFilterComponent } from "@sppc/shared/components/advanceFilter/advance-filter.component";
import { LicenseInfoComponent } from "@sppc/shared/components/dashboard/license-info.component";
import { SuperuserPasswordComponent } from "@sppc/shared/components/home/superuser-password.component";
import { NotFoundComponent } from "@sppc/shared/components/notFound/notFound.component";
import { ReportParametersComponent } from "@sppc/shared/components/reportParameters/reportParameters.component";
import { ReportViewerComponent } from "@sppc/shared/components/reportViewer/reportViewer.component";
import { LeftActionToolbarComponent } from "@sppc/shared/components/toolbar/leftActionToolbar.component";
import { ReportParamComponent } from "@sppc/shared/components/viewIdentifier/reportParam.component";
import { ViewIdentifierComponent } from "@sppc/shared/components/viewIdentifier/view-identifier.component";
import { GridMessageService } from "@sppc/shared/services/grid-messages.service";
import { GridsterModule } from "angular-gridster2";
import { ChartModule } from "primeng/chart";
import { ChartWidgetComponent } from "./components/dashboard/widget/chart-widget/chart-widget.component";
import { WidgetContainerComponent } from "./components/dashboard/widget/widget-container/widget-container.component";
import { WidgetHeaderComponent } from "./components/dashboard/widget/widget-header/widget-header.component";
import { WidgetComponent } from "./components/dashboard/widget/widget-layout/widget.component";
import { ErrorListComponent } from "./components/errorList/errorList.component";
import { QuickReportPageSettingComponent } from "./components/reportManagement/quick-report-page-setting.component";
import { SppcButtonDisable } from "./directive/button/buttonDisable.directive";
import { MessageBoxService } from "./services/message.service";
import { ShareDataService } from "./services/share-data.service";
import { ShortcutService } from "./services/shortcut.service";

import { TooltipDirective } from "./directive/editorForm/tooltip.directive";
import { AutoFocusDirective } from "./directive/input/auto-focus.directive";

import { AddWidgetComponent } from "./components/dashboard/add-widget/add-widget.component";
import { WidgetSettingComponent } from "./components/dashboard/widget/widget-setting/widget-setting.component";

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}

@NgModule({
  imports: [
    GaugesModule,
    GridsterModule,
    CommonModule,
    SharedRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    DialogModule,
    DropDownsModule,
    GridModule,
    InputsModule,
    CalendarModule,
    ExcelModule,
    PDFModule,
    ButtonsModule,
    PopupModule,
    TreeViewModule,
    ContextMenuModule,
    LayoutModule,
    ChartModule,

    DpDatePickerModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient],
      },
    }),
  ],

  declarations: [
    LayoutComponent,
    DashboardComponent,
    AppfooterComponent,
    AppheaderComponent,
    AppmenuComponent,
    AppsettingComponent,
    BreadCumbComponent,
    HomeComponent,
    LoginComponent,
    LoginCompleteComponent,
    LoginContainerComponent,
    LogoutComponent,
    NavMenuComponent,
    ReportManagementComponent,
    QuickReportSettingComponent,
    ReportParametersComponent,
    ReportViewerComponent,
    ReportParamComponent,
    ViewIdentifierComponent,
    SppcGridColumn,
    SppcAutoGridColumn,
    SppcGridReorder,
    SppcAutoGeneratedGridReorder,
    SppcAutoGeneratedGridResize,
    SppcGridResize,
    SppcCheckAccess,
    SppcViewTreeConfig,
    GridSettingComponent,
    AutoGeneratedGridSettingComponent,
    SelectFormGridSettingComponent,
    SppcDatePipe,
    SppcNumConfigPipe,
    ReplaceLineBreaksPipe,
    DialogComponent,
    GridFilterComponent,
    SppcBranchScope,
    SppcDatepicker,
    SppcDateRangeSelector,
    SppcDropDownList,
    SppcDisplayFullAccountComponent,
    SppcFullAccountComponent,
    SppcGridDatepicker,
    SppcGridDateFilter,
    FilterDatePickerDirective,
    SppcAutoGridFilter,
    SppcGridFilter,
    SelectFormComponent,
    DynamicTabsDirective,
    TabComponent,
    TabsComponent,
    SppcMaskTextBox,
    SppcNumericInput,
    SppcNumericTextBox,
    SppcCodeLengthDirective,
    ConfirmEqualValidator,
    EditorFormTitleComponent,
    FullCodeDirective,
    SppcButtonDisable,
    FullCodeTestDirective,
    SpccOnlyNumberDirective,
    SppcPermissionCheckDirective,
    SppcTimepicker,
    AdvanceFilterComponent,
    NotFoundComponent,
    ClosingTmpComponent,
    ErrorListComponent,
    LeftActionToolbarComponent,
    QuickReportPageSettingComponent,
    InputDirective,
    SuperuserPasswordComponent,
    LicenseInfoComponent,
    WidgetComponent,
    WidgetHeaderComponent,
    WidgetContainerComponent,
    TooltipDirective,

    ChartWidgetComponent,
    AutoFocusDirective,

    AddWidgetComponent,

    WidgetSettingComponent,
  ],

  entryComponents: [
    TabComponent,
    SppcGridDatepicker,
    SelectFormComponent,
    AdvanceFilterComponent,
    ErrorListComponent,
    QuickReportPageSettingComponent,
    SuperuserPasswordComponent,
    LicenseInfoComponent,
    AddWidgetComponent,
    WidgetSettingComponent,
  ],

  //providers: [BrowserStorageService, DashboardService, GridService, LookupService, MetaDataService, ReportingService,
  //  { provide: MessageService, useClass: GridMessageService },
  //  MetaDataResolver, DefaultComponent, AutoGeneratedGridComponent, GridExplorerComponent,
  //  DatePipe
  //],

  exports: [
    CommonModule,
    LayoutComponent,
    SppcGridColumn,
    SppcAutoGridColumn,
    SppcGridReorder,
    SppcAutoGeneratedGridReorder,
    SppcAutoGeneratedGridResize,
    SppcGridResize,
    SppcCheckAccess,
    SppcViewTreeConfig,
    GridSettingComponent,
    AutoGeneratedGridSettingComponent,
    SelectFormGridSettingComponent,
    SppcDatePipe,
    SppcNumConfigPipe,
    ReplaceLineBreaksPipe,
    FormsModule,
    ReactiveFormsModule,
    DialogModule,
    DropDownsModule,
    GridModule,
    InputsModule,
    CalendarModule,
    ButtonsModule,
    PopupModule,
    TreeViewModule,
    ContextMenuModule,
    LayoutModule,
    DpDatePickerModule,
    TranslateModule,
    DashboardComponent,
    NavMenuComponent,
    GridFilterComponent,
    SppcBranchScope,
    SppcDatepicker,
    SppcDateRangeSelector,
    SppcDropDownList,
    SppcDisplayFullAccountComponent,
    SppcFullAccountComponent,
    SppcGridDatepicker,
    SppcGridDateFilter,
    FilterDatePickerDirective,
    SppcAutoGridFilter,
    SppcGridFilter,
    SelectFormComponent,
    DynamicTabsDirective,
    TabComponent,
    TabsComponent,
    SppcMaskTextBox,
    SppcNumericInput,
    SppcNumericTextBox,
    SppcCodeLengthDirective,
    ConfirmEqualValidator,
    EditorFormTitleComponent,
    FullCodeDirective,
    FullCodeTestDirective,
    SpccOnlyNumberDirective,
    SppcButtonDisable,
    SppcPermissionCheckDirective,
    BreadCumbComponent,
    ViewIdentifierComponent,
    ReportManagementComponent,
    QuickReportSettingComponent,
    ReportParametersComponent,
    ReportParamComponent,
    ReportViewerComponent,
    SppcTimepicker,
    NotFoundComponent,
    ClosingTmpComponent,
    ErrorListComponent,
    LeftActionToolbarComponent,
    ExcelModule,
    PDFModule,
    QuickReportPageSettingComponent,
    InputDirective,
    TooltipDirective,
    AutoFocusDirective,
  ],
})
export class SharedModule {
  static forRoot(): ModuleWithProviders<SharedModule> {
    // Forcing the whole app to use the returned providers from the AppModule only.
    return {
      ngModule: SharedModule,
      //providers: [ /* All of your services here. It will hold the services needed by `itself`. */]
      providers: [
        BrowserStorageService,
        DashboardService,
        GridService,
        LookupService,
        MetaDataService,
        ReportingService,
        AdvanceFilterService,
        LicenseService,
        { provide: MessageService, useClass: GridMessageService },
        MetaDataResolver,
        DefaultComponent,
        AutoGeneratedGridComponent,
        GridExplorerComponent,
        DatePipe,
        ShortcutService,
        ShareDataService,
        MessageBoxService,
      ],
    };
  }
}
