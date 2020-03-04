import { Component, OnInit, Renderer2, ChangeDetectorRef, NgZone } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { Layout, Entities, MessageType } from '@sppc/env/environment';
import { DefaultComponent, Filter } from '@sppc/shared/class';
import { AutoGeneratedGridComponent } from '@sppc/shared/class';
import { OperationLog } from '@sppc/admin/models';
import { OperationLogService } from '@sppc/admin/service';
import { OperationLogApi } from '@sppc/admin/service/api';
import { GridService, BrowserStorageService, MetaDataService, LookupService, SessionKeys } from '@sppc/shared/services';
import { SettingService } from '@sppc/config/service';
import { ViewName } from '@sppc/shared/security';
import { Item } from '@sppc/shared/models';
import { BranchScopeResource, OperationLogResource, LogTypeResource, LogReportTypeResource, LogTypeKeys, LogReportTypeKeys } from '@sppc/finance/enum';
import { RowArgs } from '@progress/kendo-angular-grid';


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'operationLogs',
  templateUrl: './operationLogs.component.html',
  styles: [`
.section-option { margin-top: 15px; background-color: #f6f6f6; border: solid 1px #dadde2; padding: 15px 15px 0; }
.section-option label,input[type=text] { width:100% } /deep/.section-option kendo-dropdownlist { width:100% }
.btn-compute-default {margin-top: 25px; border: 2px solid #337ab7; color: #337ab7; padding: 5px 25px;}
.btn-compute { color: #337ab7; transition: All 0.3s 0.1s ease-out;}
.check-item { margin-top: 20px;}
.btn-show-log {padding-left:0px;}
.btn-compute-selectable{ color: #fff; background-image: linear-gradient(#c1e3ff, #337ab7);}
/deep/ .k-treeview .k-in {margin-right:12px!important;}
`],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }, DefaultComponent]
})


export class OperationLogsComponent extends AutoGeneratedGridComponent implements OnInit {

  rolesList: boolean = false;
  isNew: boolean;
  errorMessage: string;
  dateFilter: Array<Filter> = [];
  startDate: any;
  endDate: any;

  disableBranch: boolean;
  disableFiscalPeriod: boolean;
  isSystemLog: boolean;
  archiveSelected: boolean;
  branchSelected: string;
  fiscalsPeriodSelected: string;
  companySelected: string;
  entitySelected: string;
  userSelected: string;
  logType: string;
  logReportType: string;

  branchScope: Array<Item> = [
    { value: BranchScopeResource.CurrentBranch, key: "1" },
    { value: BranchScopeResource.CurrentBranchAndSubsets, key: "2" },
  ]

  LogTypes: Array<Item> = [
    //{ value: LogTypeResource.AllLogs, key: "1" },
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
  entities: Array<Item>;
  users: Array<Item>;

  detailDataItem?: OperationLog = undefined;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public gridService: GridService, public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, private operationLogService: OperationLogService,
    public settingService: SettingService, public ngZone: NgZone, public lookupService: LookupService) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, bStorageService, cdref, ngZone);
  }

  //#region Events

  ngOnInit() {
    this.entityName = Entities.OperationLog;
    this.viewId = ViewName[this.entityTypeName];
    this.fillCompanyLookup();
    this.fillFiscalPeriodLookup();
    this.branchSelected = this.branchScope[0].key;
    this.companySelected = this.CompanyId.toString();
    this.fiscalsPeriodSelected = this.FiscalPeriodId.toString();
    this.userSelected ="0";
    this.entitySelected = "0";
    this.logType = LogTypeKeys.ActiveLogs;
    this.logReportType = LogReportTypeKeys.OperationalLogs;
    this.fillUserLookup();

    this.loadStates();
    this.systemLogChanged();

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


  public editHandler(arg: any) {
    this.detailDataItem = this.selectedRows[0];
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
      this.entities = res;
      this.entitySelected = "0";
    });
  }

  fillSystemEntityLookup() {
    this.lookupService.GetSystemEntityLookupAsync().subscribe((res) => {
      this.entities = res;
      this.entitySelected = "0";
    });
  }

  fillUserLookup() {
    this.lookupService.GetUserLookupAsync().subscribe((res) => {
      this.users = res;
      this.userSelected = "0";
    });
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
  }

  dateValueChange(event: any) {
    this.startDate = event.fromDate;
    this.endDate = event.toDate;    
  }  

  //#endregion

  getReportData() {

    this.saveStates();

    if (this.logReportType == LogReportTypeKeys.SystemLogs && this.logType == LogTypeKeys.ArchivedLogs)
      this.getDataUrl = OperationLogApi.SysOperationLogsArchiveUrl;

    if (this.logReportType == LogReportTypeKeys.SystemLogs && this.logType == LogTypeKeys.ActiveLogs)
      this.getDataUrl = OperationLogApi.SysOperationLogsUrl;

    if (this.logReportType == LogReportTypeKeys.OperationalLogs && this.logType == LogTypeKeys.ArchivedLogs)
      this.getDataUrl = OperationLogApi.OperationLogsArchiveUrl;

    if (this.logReportType == LogReportTypeKeys.OperationalLogs && this.logType == LogTypeKeys.ActiveLogs)
      this.getDataUrl = OperationLogApi.OperationLogsUrl;

    this.defaultFilter = [];

    var sDate = this.startDate.replace('00:00:01', '00:00:00')
    this.defaultFilter.push(new Filter("Date", sDate, " >= {0} ", "System.DateTime"),
      new Filter("Date", this.endDate, " <= {0} ", "System.DateTime"));

 
    if (this.logReportType == LogReportTypeKeys.OperationalLogs) {
      if(this.branchSelected != "-1")
        this.defaultFilter.push(new Filter("BranchId", this.branchSelected.toString(), " == {0}", "System.Int32"));

      if (this.fiscalsPeriodSelected != "-1")
        this.defaultFilter.push(new Filter("FiscalPeriodId", this.fiscalsPeriodSelected.toString(), " == {0}", "System.Int32"));
    }

    if (this.companySelected != "-1")
      this.defaultFilter.push(new Filter("CompanyId", this.companySelected.toString(), " == {0}", "System.Int32"));

    if (this.entitySelected != "0")
      this.defaultFilter.push(new Filter("EntityTypeId", this.entitySelected.toString(), " == {0}", "System.Int32"));

    if (this.userSelected != "0")
      this.defaultFilter.push(new Filter("UserId", this.userSelected.toString(), " == {0}", "System.Int32"));

    this.reloadGrid();
    
  } 

  archiveData() {

    let modelsIdArray: Array<number> = [];
    this.selectedRows.forEach(item => {
      modelsIdArray.push(item);
    })

    if (modelsIdArray.length > 0) {
      if (this.logReportType == LogReportTypeKeys.OperationalLogs) {
        this.operationLogService.putSelectedLogsAsArchived(modelsIdArray).subscribe((res) => {
          this.showMessage(this.getText("OperationLog.SelectedLogArchived"), MessageType.Succes);
        });
      }
      else {
        this.operationLogService.putSelectedSysLogsAsArchived(modelsIdArray).subscribe((res) => {
          this.showMessage(this.getText("OperationLog.SelectedLogArchived"), MessageType.Succes);
        });
      }
    }
    else {
      if (this.logReportType == LogReportTypeKeys.OperationalLogs) {
        this.operationLogService.postSelectedLogsAsArchived(this.startDate, this.endDate).subscribe((res) => {
          this.showMessage(this.getText("OperationLog.LogArchived"), MessageType.Succes);
        });
      }
      else {
        this.operationLogService.postSelectedSysLogsAsArchived(this.startDate, this.endDate).subscribe((res) => {
          this.showMessage(this.getText("OperationLog.LogArchived"), MessageType.Succes);
        });
      }
    }

    
    
  }

  public mySelectionKey(context: RowArgs): string {
    return context.dataItem.id;
  }

  deleteArchiveData() {
    let modelsIdArray: Array<number> = [];
    this.selectedRows.forEach(item => {
      modelsIdArray.push(item);
    })
    
    if (this.logReportType == LogReportTypeKeys.SystemLogs) {
      this.operationLogService.PutSelectedArchivedSysLogsAsDeleted(modelsIdArray).subscribe((res) => {
        this.showMessage(this.getText("OperationLog.DeleteArchivedMsg"), MessageType.Succes);
      });
    }
    else {
      this.operationLogService.PutSelectedArchivedLogsAsDeleted(modelsIdArray).subscribe((res) => {
        this.showMessage(this.getText("OperationLog.DeleteArchivedMsg"), MessageType.Succes);
      });
    }

    this.getReportData();
  }

  saveStates() {

    var state = {
      logType: this.logType,
      logReportType: this.logReportType,
      branchSelected: this.branchSelected,
      fiscalsPeriodSelected: this.fiscalsPeriodSelected,
      companySelected: this.companySelected,
      userSelected: this.userSelected,
      entitySelected: this.entitySelected,
      startDate: this.startDate,
      endDate: this.endDate
    };

    this.bStorageService.setSession(SessionKeys.OperationLog, state);
  }

  loadStates() {

    var state = this.bStorageService.getSession(SessionKeys.OperationLog);
    if (state) {
      this.logType = state.logType;
      this.logReportType = state.logReportType;
    
      this.branchSelected = state.branchSelected;
      
      this.fiscalsPeriodSelected = state.fiscalsPeriodSelected;
      this.companySelected = state.companySelected;
      this.userSelected = state.userSelected;
      this.entitySelected = state.entitySelected;
      this.startDate = state.startDate;
      this.endDate = state.endDate;
    }
  }

}
