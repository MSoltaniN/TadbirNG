import { Component, OnInit, Renderer2, ChangeDetectorRef, NgZone, ViewChild, HostListener, ElementRef, OnDestroy } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
// import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { Layout, Entities, MessageType } from '@sppc/shared/enum/metadata';
import { DefaultComponent, Filter, FilterExpressionOperator, FilterExpression } from '@sppc/shared/class';
import { AutoGeneratedGridComponent } from '@sppc/shared/class';
import { OperationLog } from '@sppc/admin/models';
import { OperationLogService } from '@sppc/admin/service';
import { OperationLogApi } from '@sppc/admin/service/api';
import { GridService, BrowserStorageService, MetaDataService, LookupService, SessionKeys } from '@sppc/shared/services';
import { SettingService } from '@sppc/config/service';
import { OperationLogPermissions, ViewName } from '@sppc/shared/security';
import { Item, Braces } from '@sppc/shared/models';
import { BranchScopeResource, OperationLogResource, LogTypeResource, LogReportTypeResource, LogTypeKeys, LogReportTypeKeys } from '@sppc/finance/enum';
import { RowArgs, GridComponent } from '@progress/kendo-angular-grid';
import { EntityTypeInfo } from '@sppc/admin/models/entityType';
import { ReportManagementComponent } from '@sppc/shared/components/reportManagement/reportManagement.component';
import { QuickReportSettingComponent } from '@sppc/shared/components/reportManagement/QuickReport-Setting.component';
import { ViewIdentifierComponent, ReportViewerComponent } from '@sppc/shared/components';
import { OperationId } from '@sppc/shared/enum/operationId';
import { SuperuserPasswordComponent } from '@sppc/shared/components/home/superuser-password.component';
import { Persist, SavePersist } from '@sppc/shared/decorator/persist.decorator';
import { BranchScopeType } from '@sppc/finance/enum/shared';
import { ServiceLocator } from '@sppc/service.locator';
import { ShareDataService } from '@sppc/shared/services/share-data.service';


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'operationLogs',
  templateUrl: './operationLogs.component.html',
  styles: [`
.section-option { margin-top: 15px; background-color: #f6f6f6; border: solid 1px #dadde2; padding: 15px 15px 0; }
.section-option label,input[type=text] { width:100% } ::ng-deep.section-option kendo-dropdownlist { width:100% }
.btn-compute-default {margin-top: 25px; border: 2px solid #337ab7; color: #337ab7;width:100%}
.btn-compute { color: #fff; transition: All 0.3s 0.1s ease-out;}
.check-item { margin-top: 20px;}
.btn-show-log {padding-left:0px;}
.btn-compute-selectable{ color: #fff; background-image: linear-gradient(#c1e3ff, #337ab7);}
::ng-deep .k-treeview .k-in {margin-right:12px!important;}
`],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }, DefaultComponent]
})


export class OperationLogsComponent extends AutoGeneratedGridComponent implements OnInit,OnDestroy {

  scopes = ["OperationLogsComponent","AutoGeneratedGridComponent"];

  rolesList: boolean = false;
  isNew: boolean;
  errorMessage: string;
  dateFilter: Array<Filter> = [];
  
  @Persist()
  startDate: any;
  
  @Persist()
  endDate: any;

  disableBranch: boolean;
  disableButtons: boolean;
  disableFiscalPeriod: boolean;
  isSystemLog: boolean;
  archiveSelected: boolean;
  
  @Persist()
  branchSelected: string;
  
  @Persist()
  fiscalsPeriodSelected: string;
  
  @Persist()
  companySelected: string;

  @Persist()
  entitySelected: string;
  
  @Persist()
  userSelected: string;
  
  @Persist()
  logType: string;

  @Persist()
  logReportType: string;

  deleteArchiveConfirm: boolean;
  archiveConfirm: boolean;

  isSuperAdmin: boolean = false;
  showGetPasswordModal: boolean = false;
  //counter 'E' key 
  specialKeyPressCounter: number;

 
  branchScope: Array<Item> = [
    { value: BranchScopeResource.CurrentBranch, key: BranchScopeType.CurrentBranch  },
    { value: BranchScopeResource.CurrentBranchAndSubsets, key: BranchScopeType.CurrentBranchAndSubsets},
  ]

  LogTypes: Array<Item> = [
    { value: LogTypeResource.AllLogs, key: LogTypeKeys.AllLogs },
    { value: LogTypeResource.ActiveLogs, key: LogTypeKeys.ActiveLogs },
    { value: LogTypeResource.ArchivedLogs, key: LogTypeKeys.ArchivedLogs },
  ]

  LogReportTypes: Array<Item> = [    
    { value: LogReportTypeResource.OperationalLogs, key: LogReportTypeKeys.OperationalLogs },
    { value: LogReportTypeResource.SystemLogs, key: LogReportTypeKeys.SystemLogs },
  ]

  defFiscalPeriod: Item = { value: OperationLogResource.AllFiscalPeriod, key: "-1" }
  defCompany: Item = { value: OperationLogResource.AllCompanies, key: "-1" }

  fiscalsPeriods: Array<Item>;
  companies: Array<Item>;
  entities: Array<EntityTypeInfo>;
  users: Array<Item>;

  detailDataItem?: OperationLog = undefined;

  @ViewChild(GridComponent, {static: true}) grid: GridComponent;
  @ViewChild(ViewIdentifierComponent, {static: true}) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportViewerComponent, {static: true}) viewer: ReportViewerComponent;
  @ViewChild(ReportManagementComponent, {static: true}) reportManager: ReportManagementComponent;
  @ViewChild(QuickReportSettingComponent, {static: true}) reportSetting: QuickReportSettingComponent;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public gridService: GridService, public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, private operationLogService: OperationLogService,
    public settingService: SettingService, public ngZone: NgZone, public lookupService: LookupService,public elem:ElementRef) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, bStorageService, cdref, ngZone,elem);

    this.scopeService = ServiceLocator.injector.get(ShareDataService);    
    this.scopeService.setScope(this);
  }

  //#region Events

  ngOnInit() {
    if (!this.isAccess(Entities.OperationLog, OperationLogPermissions.View)) {
      this.showMessage(
        this.getText("App.AccessDenied"),
        MessageType.Warning
      );
    }
    this.entityName = Entities.OperationLog;
    this.viewId = ViewName[this.entityTypeName];
    this.fillCompanyLookup();
    this.fillFiscalPeriodLookup();
    this.branchSelected = this.branchScope[0].key;
    this.companySelected = this.CompanyId.toString();
    this.fiscalsPeriodSelected = this.FiscalPeriodId.toString();
    this.userSelected ="0";
    this.entitySelected = "-1";
    this.logType = LogTypeKeys.ActiveLogs;
    this.logReportType = LogReportTypeKeys.OperationalLogs;
    this.fillUserLookup();

    this.useCustomFilterExpression = true;
    this.systemLogChanged();
    this.logTypeChanged();

    //system log
    if (this.logReportType == LogReportTypeKeys.SystemLogs) {
      this.fillSystemEntityLookup();
    }
    else {
      this.fillEntityLookup();
    }    

    this.cdref.detectChanges();
    this.showloadingMessage = false;
  }

  ngOnDestroy(): void {    
    this.scopeService = ServiceLocator.injector.get(ShareDataService);    
    this.scopeService.clearScope(this);
  }


  public editHandler(arg: any) {
    var recordId = this.selectedRows[0];
    var record = this.rowData.data.find((f) => f.id == recordId);

    this.detailDataItem = record;
  }

  public cancelHandler() {
    this.detailDataItem = undefined;
  }

  fillFiscalPeriodLookup() {
    this.lookupService.GetFiscalPeriodsLookupAsync(this.CompanyId, this.UserId).subscribe((res) => {      
      this.fiscalsPeriods = res;
      this.fiscalsPeriods.splice(0, 0, this.defFiscalPeriod);
    });
  }

  fillCompanyLookup() {
    this.lookupService.GetCompanyLookupAsync(this.UserId).subscribe((res) => {
      this.companies = res;
      this.companies.splice(0, 0, this.defCompany);
    });
  }

  fillEntityLookup() {
    this.lookupService.GetEntityLookupAsync().subscribe((res) => {
      var items = new Array<EntityTypeInfo>();
      var index = 0;
      (<Array<any>>res).forEach((item) => {
        
        var entity = new EntityTypeInfo();
        entity.value = item.name;
        entity.key = "-1";
        if (item.entityTypeId) {
          index++;
          entity.key = index + "-" + item.entityTypeId;
          entity.isEntity = true;
        }

        if (item.sourceId) {
          index++;
          entity.key = index + "-" + item.sourceId;
          entity.isEntity = false;
        }
        items.push(entity);
      });

      this.entities = items;
      this.entitySelected = "-1";
    });
  }

  fillSystemEntityLookup() {
    this.lookupService.GetSystemEntityLookupAsync().subscribe((res) => {
      var items = new Array<EntityTypeInfo>();
      var index = 0;
      (<Array<any>>res).forEach((item) => {

        var entity = new EntityTypeInfo();
        entity.value = item.name;
        entity.key = "-1";
        if (item.entityTypeId) {
          index++;
          entity.key = index + "-" + item.entityTypeId;
          entity.isEntity = true;
        }

        if (item.sourceId) {
          index++;
          entity.key = index + "-" + item.sourceId;
          entity.isEntity = false;
        }
        items.push(entity);
      });

      this.entities = items;
      this.entitySelected = "-1";
    });
  }

  fillUserLookup() {
    this.lookupService.GetUserLookupAsync().subscribe((res) => {
      this.users = res;
      if (!this.IsAdmin)
        this.userSelected = this.UserId.toString();
      else
        this.userSelected = "0";
    });
  }

  logTypeChanged() {
    if (this.logType == LogTypeKeys.AllLogs) {
      this.disableButtons = true;      
    }
    else {
      this.disableButtons = false;      
    }
    this.selectedRows = [];
    this.changeViewType();
  }

  changeViewType() {           

    this.rowData = undefined;
    this.showloadingMessage = false;

    if (this.logType == LogTypeKeys.ActiveLogs && this.logReportType == LogReportTypeKeys.SystemLogs) {
      this.entityName = Entities.SysOperationLog;
      this.viewId = ViewName[this.entityTypeName];
    }
    else if (this.logType == LogTypeKeys.ArchivedLogs && this.logReportType == LogReportTypeKeys.OperationalLogs) {
      this.entityName = Entities.OperationLogArchive;
      this.viewId = ViewName[this.entityTypeName];
    }
    else if (this.logType == LogTypeKeys.ArchivedLogs && this.logReportType == LogReportTypeKeys.SystemLogs) {
      this.entityName = Entities.SysOperationLogArchive;
      this.viewId = ViewName[this.entityTypeName];
    }
    else {
      this.entityName = Entities.OperationLog;
      this.viewId = ViewName[this.entityTypeName];
    }
  }

  systemLogChanged() {
    //system log
    if (this.logReportType == LogReportTypeKeys.SystemLogs) {
      this.disableBranch = true;
      this.disableFiscalPeriod = true;
      this.branchSelected = this.branchScope[0].key;
      this.fiscalsPeriodSelected = "-1";
      this.fillSystemEntityLookup();
    }
    else {
      this.disableBranch = false;
      this.disableFiscalPeriod = false;
      this.fillEntityLookup();
    }

    this.changeViewType();
  }

  dateValueChange(event: any) {
    this.startDate = event.fromDate;
    this.endDate = event.toDate;    
  }  

  //#endregion

  @SavePersist()
  getReportData() {
    

    if (this.logReportType == LogReportTypeKeys.OperationalLogs && this.logType == LogTypeKeys.AllLogs)
      this.getDataUrl = OperationLogApi.AllOperationLogs;

    if (this.logReportType == LogReportTypeKeys.SystemLogs && this.logType == LogTypeKeys.AllLogs)
      this.getDataUrl = OperationLogApi.AllSysOperationLogs;

    if (this.logReportType == LogReportTypeKeys.SystemLogs && this.logType == LogTypeKeys.ArchivedLogs)
      this.getDataUrl = OperationLogApi.SysOperationLogsArchive;

    if (this.logReportType == LogReportTypeKeys.SystemLogs && this.logType == LogTypeKeys.ActiveLogs)
      this.getDataUrl = OperationLogApi.SysOperationLogs;

    if (this.logReportType == LogReportTypeKeys.OperationalLogs && this.logType == LogTypeKeys.ArchivedLogs)
      this.getDataUrl = OperationLogApi.OperationLogsArchive;

    if (this.logReportType == LogReportTypeKeys.OperationalLogs && this.logType == LogTypeKeys.ActiveLogs)
      this.getDataUrl = OperationLogApi.OperationLogs;

    this.defaultFilter = [];

    var sDate = this.startDate.replace('00:00:01', '00:00:00')
    this.defaultFilter.push(new Filter("Date", sDate, " >= {0} ", "System.DateTime"),
      new Filter("Date", this.endDate, " <= {0} ", "System.DateTime"));

 
    if (this.logReportType == LogReportTypeKeys.OperationalLogs) {
      if (this.branchSelected == this.branchScope[0].key)
        this.defaultFilter.push(new Filter("BranchId", this.BranchId.toString(), " == {0}", "System.Int32"));

      if (this.fiscalsPeriodSelected != "-1")
        this.defaultFilter.push(new Filter("FiscalPeriodId", this.fiscalsPeriodSelected.toString(), " == {0}", "System.Int32"));
    }

    var loginSourceId = 7;
    var sourceId = 0;
    if (this.entitySelected != "-1") {
      var filters = this.entities.filter(f => f.key === this.entitySelected);
      if (filters.length > 0) {
        if (filters[0].isEntity)
          this.defaultFilter.push(new Filter("EntityTypeId", this.entitySelected.split('-')[1].toString(), " == {0}", "System.Int32"));
        else {
          this.defaultFilter.push(new Filter("SourceId", this.entitySelected.split('-')[1].toString(), " == {0}", "System.Int32"));
          sourceId = parseInt(this.entitySelected.split('-')[1].toString());
        }
      }
    }

    if (this.userSelected != "0")
      this.defaultFilter.push(new Filter("UserId", this.userSelected.toString(), " == {0}", "System.Int32"));   

    this.customFilter = undefined;
    var orFilter: Filter = null;
    if (this.companySelected != "-1" && sourceId == loginSourceId) {
      var startBrace = new Array<Braces>();
      var endBrace = new Array<Braces>();
      var id1 = "1";
      var id2 = "2";
      var brace1: Braces = { brace: "(", outerId: id2 };
      var brace2: Braces = { brace: ")", outerId: id1 };
      startBrace.push(brace1);
      endBrace.push(brace2);

      var valueFilter = new Filter("CompanyId", this.companySelected.toString(), " == {0}", "System.Int32", startBrace);
      var nullFilter = new Filter("CompanyId", "null", " == {0}", "System.Int32", endBrace);
      var fil1 = new FilterExpression();
      fil1.filter = valueFilter;
      fil1.operator = " && ";

      var fil2 = new FilterExpression();
      fil2.filter = nullFilter;
      fil2.operator = " || ";

      if (this.customFilter = undefined) {
        this.customFilter == new FilterExpression();
        this.customFilter.operator = " && ";
      }

      this.customFilter.children.push(fil1);
      this.customFilter.children.push(fil2);
    }
    else if (this.companySelected != "-1" ) {
      this.defaultFilter.push(new Filter("CompanyId", this.companySelected.toString(), " == {0}", "System.Int32"));
    }


    if (!this.isSuperAdmin && this.logReportType == LogReportTypeKeys.OperationalLogs) {
      var startBrace = new Array<Braces>();
      var endBrace = new Array<Braces>();
      var id1 = "3";
      var id2 = "4";
      var brace1: Braces = { brace: "(", outerId: id2 };
      var brace2: Braces = { brace: ")", outerId: id1 };
      startBrace.push(brace1);
      endBrace.push(brace2);

      let undoFinalize: number = OperationId.UndoFinalize;
      let groupUndoFinalize: number = OperationId.GroupUndoFinalize;

      var valueFilter = new Filter("OperationId", groupUndoFinalize.toString(), " != {0}", "System.Int32", startBrace);
      var nullFilter = new Filter("OperationId", undoFinalize.toString(), " != {0}", "System.Int32", endBrace);
      var fil1 = new FilterExpression();
      fil1.filter = valueFilter;
      fil1.operator = " && ";

      var fil2 = new FilterExpression();
      fil2.filter = nullFilter;
      fil2.operator = " && ";

      if (this.customFilter == undefined) {
        this.customFilter = new FilterExpression();
        this.customFilter.operator = " && ";
      }

      this.customFilter.children.push(fil1);
      this.customFilter.children.push(fil2);
    }

    this.reloadGrid();

  }

  @HostListener('document:keydown', [])
  handleKeyboardEvent() {
    var event: KeyboardEvent = <KeyboardEvent>window.event;
    this.handleCtrlE(event);
  }

  confirmArchiveData() {
    if (this.rowData && this.rowData.data.length > 0) {
      this.archiveConfirm = true;
    }
  }

  confirmDeleteArchiveData() {
    if (this.selectedRows.length > 0) {
      if (this.rowData && this.rowData.data.length > 0) {
        this.deleteArchiveConfirm = true;
      }
    }
    else {
      this.showMessage(this.getText("OperationLog.PleaseSelectRowsAsDeleteArchive"), MessageType.Warning);
    }
  }

  archiveData(archive:boolean) {
    if (archive) {     

      let modelsIdArray: Array<number> = [];
      this.selectedRows.forEach(item => {
        modelsIdArray.push(item);
      })

      this.resetSelections();

      if (modelsIdArray.length > 0) {
        if (this.logReportType == LogReportTypeKeys.OperationalLogs) {
          this.operationLogService.putSelectedLogsAsArchived(modelsIdArray).subscribe((res) => {
            this.showMessage(this.getText("OperationLog.SelectedLogArchived"), MessageType.Succes);
            this.refreshGrid();
          },
          (error => {            
            this.showMessage(this.errorHandlingService.handleError(error), MessageType.Warning);
          }));
        }
        else {
          this.operationLogService.putSelectedSysLogsAsArchived(modelsIdArray).subscribe((res) => {
            this.showMessage(this.getText("OperationLog.SelectedLogArchived"), MessageType.Succes);
            this.refreshGrid();
          },
            (error => {
              this.showMessage(this.errorHandlingService.handleError(error), MessageType.Warning);
          }));
        }
      }
      else {
        if (this.logReportType == LogReportTypeKeys.OperationalLogs) {
          this.operationLogService.postSelectedLogsAsArchived(this.startDate, this.endDate).subscribe((res) => {
            this.showMessage(this.getText("OperationLog.LogArchived"), MessageType.Succes);
            this.refreshGrid();
          },
          (error => {
            this.showMessage(this.errorHandlingService.handleError(error));
          }));
        }
        else {
          this.operationLogService.postSelectedSysLogsAsArchived(this.startDate, this.endDate).subscribe((res) => {
            this.showMessage(this.getText("OperationLog.LogArchived"), MessageType.Succes);
            this.refreshGrid();
          },
          (error => {
            this.showMessage(this.errorHandlingService.handleError(error), MessageType.Warning);
          }));
        }
      }
    }

    this.hideConfirm();    
  }

  public mySelectionKey(context: RowArgs): string {
    return context.dataItem.id;
  }

  deleteArchiveData(deleteArchive: boolean) {
    if (deleteArchive) {      

      let modelsIdArray: Array<number> = [];
      this.selectedRows.forEach(item => {
        modelsIdArray.push(item);
      })

      this.resetSelections();

      if (this.logReportType == LogReportTypeKeys.SystemLogs) {
        this.operationLogService.PutSelectedArchivedSysLogsAsDeleted(modelsIdArray).subscribe((res) => {
          this.showMessage(this.getText("OperationLog.DeleteArchivedMsg"), MessageType.Succes);
          this.refreshGrid();
        },
        (error => {
          this.showMessage(this.errorHandlingService.handleError(error), MessageType.Warning);
        }));
      }
      else {
        this.operationLogService.PutSelectedArchivedLogsAsDeleted(modelsIdArray).subscribe((res) => {
          this.showMessage(this.getText("OperationLog.DeleteArchivedMsg"), MessageType.Succes);
          this.refreshGrid();
        },
        (error => {
          this.showMessage(this.errorHandlingService.handleError(error), MessageType.Warning);
        }));
      }
    }

    this.hideConfirm();
  }  

  onAdvanceFilterOk(): any {
    this.enableViewListChanged(this.viewId);
    this.operationId = OperationId.Filter;
    this.getReportData();
  }

  refreshGrid() {
    this.resetSelections();
    this.getReportData();
  }

  resetSelections() {
    this.selectedRows = [];
  }

  hideConfirm() {
    this.deleteArchiveConfirm = false;
    this.archiveConfirm = false;
  }
}
