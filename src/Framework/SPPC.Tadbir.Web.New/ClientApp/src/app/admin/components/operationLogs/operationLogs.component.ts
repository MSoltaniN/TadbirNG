import { Component, OnInit, Renderer2, ChangeDetectorRef, NgZone } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { Layout, Entities } from '@sppc/env/environment';
import { DefaultComponent, Filter } from '@sppc/shared/class';
import { AutoGeneratedGridComponent } from '@sppc/shared/class';
import { OperationLog } from '@sppc/admin/models';
import { OperationLogService } from '@sppc/admin/service';
import { OperationLogApi } from '@sppc/admin/service/api';
import { GridService, BrowserStorageService, MetaDataService, LookupService, SessionKeys } from '@sppc/shared/services';
import { SettingService } from '@sppc/config/service';
import { ViewName } from '@sppc/shared/security';
import { Item } from '@sppc/shared/models';
import { BranchScopeResource, OperationLogResource } from '@sppc/finance/enum';


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
.btn-compute-selectable{ color: #fff; background-image: linear-gradient(#c1e3ff, #337ab7);}
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
  entitySelected: number;
  userSelected: number;

  branchScope: Array<Item> = [
    { value: BranchScopeResource.CurrentBranch, key: "1" },
    { value: BranchScopeResource.CurrentBranchAndSubsets, key: "2" },
  ]

  defFiscalPeriod: Item = { value: OperationLogResource.AllFiscalPeriod, key: "-1" }
  defCompany: Item = { value: OperationLogResource.AllCompanies, key: "-1" }

  fiscalsPeriods: Array<Item>;
  companies: Array<Item>;

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

    this.loadStates();
    this.cdref.detectChanges();
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

  systemLogChanged() {
    if (this.isSystemLog) {
      this.disableBranch = true;
      this.disableFiscalPeriod = true;
      this.branchSelected = this.branchScope[0].key;
      this.fiscalsPeriodSelected = "-1";
    }
    else {
      this.disableBranch = false;
      this.disableFiscalPeriod = false;
    }
  }

  dateValueChange(event: any) {
    this.startDate = event.fromDate;
    this.endDate = event.toDate;    
  }  

  //#endregion

  getReportData() {

    this.saveStates();

    if (this.isSystemLog && this.archiveSelected)
      this.getDataUrl = OperationLogApi.SysOperationLogsArchiveUrl;

    if (this.isSystemLog && !this.archiveSelected)
      this.getDataUrl = OperationLogApi.AllSysOperationLogsUrl;

    if (!this.isSystemLog && this.archiveSelected)
      this.getDataUrl = OperationLogApi.OperationLogsArchiveUrl;

    if (!this.isSystemLog && !this.archiveSelected)
      this.getDataUrl = OperationLogApi.AllOperationLogsUrl;

    this.defaultFilter = [];

    this.defaultFilter.push(new Filter("Date", this.startDate, " >= {0} ", "System.DateTime"),
      new Filter("Date", this.endDate, " <= {0} ", "System.DateTime"));

    if (!this.isSystemLog) {
      if(this.branchSelected != "-1")
        this.defaultFilter.push(new Filter("BranchId", this.branchSelected.toString(), " == {0}", "System.Int32"));

      if (this.fiscalsPeriodSelected != "-1")
        this.defaultFilter.push(new Filter("FiscalPeriodId", this.fiscalsPeriodSelected.toString(), " == {0}", "System.Int32"));
    }

    if (this.companySelected != "-1")
      this.defaultFilter.push(new Filter("CompanyId", this.companySelected.toString(), " == {0}", "System.Int32"));

    if (this.entitySelected > -1)
      this.defaultFilter.push(new Filter("EntityTypeId", this.entitySelected.toString(), " == {0}", "System.Int32"));

    if (this.userSelected > -1)
      this.defaultFilter.push(new Filter("UserId", this.userSelected.toString(), " == {0}", "System.Int32"));

    this.reloadGrid();
    
  } 

  archiveData() {
    this.operationLogService.postSelectedLogsAsArchived(this.startDate, this.endDate).subscribe((res) => {
      this.showMessage(this.getText("OperationLog.LogArchived"));
    });
  }

  saveStates() {

    var state = {
      archiveSelected: this.archiveSelected,
      isSystemLog: this.isSystemLog,
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
      this.archiveSelected = state.archiveSelected;
      this.isSystemLog = state.isSystemLog;
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
