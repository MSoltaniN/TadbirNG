import { environment } from "@sppc/env/environment";
import { Layout, Entities, MessageType, CustomForm } from "@sppc/shared/enum/metadata";
import { Component, OnInit, ChangeDetectorRef, Renderer2, NgZone, ViewChild, ElementRef } from "@angular/core";
import { String,AutoGeneratedGridComponent, Filter, FilterExpression, FilterExpressionOperator } from "@sppc/shared/class";
import { RTL } from "@progress/kendo-angular-l10n";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from "@ngx-translate/core";
import { DialogService } from "@progress/kendo-angular-dialog";
import { GridService, MetaDataService, BrowserStorageService, ReportingService, SessionKeys, LookupService } from "@sppc/shared/services";
import { SettingService } from "@sppc/config/service";
import { ViewName, BalanceSheetPermissions } from "@sppc/shared/security";
import { BranchScopeResource, VoucherStatusResource, ComparativeResource, ComparativeKeys } from "@sppc/finance/enum";
import { Item } from "@sppc/shared/models";
import { SelectFormComponent } from "@sppc/shared/controls";
import { VoucherApi } from "@sppc/finance/service/api";
import { GridComponent } from "@progress/kendo-angular-grid";
import { ViewIdentifierComponent, ReportViewerComponent } from "@sppc/shared/components";
import { ReportManagementComponent } from "@sppc/shared/components/reportManagement/reportManagement.component";
import { QuickReportSettingComponent } from "@sppc/shared/components/reportManagement/QuickReport-Setting.component";
import { VoucherService } from "@sppc/finance/service";
import { QuickReportViewSetting } from "@sppc/shared/components/reportManagement/quickReportViewSetting";
import { BalanceSheetApi } from "@sppc/finance/service/api/balanceSheetApi";
import { LookupApi } from "@sppc/shared/services/api";
import { OperationId } from "@sppc/shared/enum/operationId";
import { Persist, SavePersist } from "@sppc/shared/decorator/persist.decorator";
import { BranchScopeType, VoucherStatusType } from "@sppc/finance/enum/shared";
import { LoadPersist } from "@sppc/shared/decorator/load-persist.decorator";


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@LoadPersist()
@Component({
  selector: 'sppc-balance-sheet',
  templateUrl: './balanceSheet.component.html',
  styles: [`
.section-option { margin-top: 15px; background-color: #f6f6f6; border: solid 1px #dadde2; padding: 15px 15px 0; }
.section-option label,input[type=text] { width:100% } /deep/.section-option kendo-dropdownlist { width:100% }
/deep/ .k-switch-on .k-switch-handle { left: -8px !important; }
/deep/ .k-switch-off .k-switch-handle { left: -4px !important; }
/deep/ .k-switch[dir="rtl"] .k-switch-label-on { right: -22px; }
/deep/ .k-switch[dir="rtl"] .k-switch-label-off { left: 0; }
/deep/ .k-switch-label-on,/deep/ .k-switch-label-off { overflow: initial; }
/deep/.k-footer-template { background-color: #b3b3b3; color: #000;}
.btn-compute-default {margin-top: 25px; border: 2px solid #337ab7; color: #337ab7; padding: 5px 25px;}
.btn-compute { color: #337ab7; transition: All 0.3s 0.1s ease-out;}
.btn-compute-selectable{ color: #fff; background-image: linear-gradient(#c1e3ff, #337ab7);}
/deep/ sppc-balance-sheet .k-grid tr.k-alt {background-color: rgb(248, 251, 253)!important;}
/deep/ sppc-balance-sheet .k-grid[dir="rtl"] td, .k-rtl .k-grid td { border-width: 0 0px 0 0!important;border: 0!important;}
.section-account button { margin: 0 2px; }
.section-account .acc-name{ width: 88% }
.section-account .acc-code{ width: 57%; position: absolute; top: -5px; }
.section-account .acc-code-rtl { left: 30px; }
.section-account .acc-code-ltr { right: 30px; }
.section-account label {width:35%}
`],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class BalanceSheetComponent extends AutoGeneratedGridComponent implements OnInit {

  toDate: Date;

  @Persist() selectedCostCenterModel: any;
  @Persist() selectedProjectModel: any;

  @Persist() branchScopeSelected: string = BranchScopeType.CurrentBranch;

  @Persist() voucherStatusSelected: string = VoucherStatusType.Committed;

  filterByRef: string;
  param: any;
  
  @Persist() projectSelected: boolean;
  @Persist() costCenterSelected: boolean;
  showFilterByRef: boolean;  
  @Persist() closing: boolean;  
  isDefaultBtn: boolean = true;

  referenceValues: string[];
  
  @Persist() selectedReferences: string[];

  voucherStatus: Array<Item> = [
    { value: VoucherStatusResource.Committed, key: VoucherStatusType.Committed },
    { value: VoucherStatusResource.Finalized, key: VoucherStatusType.Finalized },
    { value: VoucherStatusResource.Confirmed, key: VoucherStatusType.Confirmed },
    { value: VoucherStatusResource.Approved, key: VoucherStatusType.Approved },
    { value: VoucherStatusResource.AllVouchers, key: VoucherStatusType.AllVouchers }
  ]

  branchScope: Array<Item> = [
    { value: BranchScopeResource.CurrentBranch, key: BranchScopeType.CurrentBranch  },
    { value: BranchScopeResource.CurrentBranchAndSubsets, key: BranchScopeType.CurrentBranchAndSubsets},
  ]

  comparative: Array<Item> = [
    { value: ComparativeResource.Branch, key: ComparativeKeys.Branch },
    { value: ComparativeResource.FiscalPeriod, key: ComparativeKeys.FiscalPeriod },
    { value: ComparativeResource.CostCenter, key: ComparativeKeys.CostCenter },
    { value: ComparativeResource.Project, key: ComparativeKeys.Project},
  ]  

  viewSetting: QuickReportViewSetting = { hideHorizontalLine : true, hideVerticalLine : true}; 

  @ViewChild(GridComponent) grid: GridComponent;
  @ViewChild(ViewIdentifierComponent) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportViewerComponent) viewer: ReportViewerComponent;
  @ViewChild(ReportManagementComponent) reportManager: ReportManagementComponent;
  @ViewChild(QuickReportSettingComponent) reportSetting: QuickReportSettingComponent;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public dialogService: DialogService, public gridService: GridService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService,
    public bStorageService: BrowserStorageService, public settingService: SettingService,
    public reporingService: ReportingService, public ngZone: NgZone, public voucherService: VoucherService, public lookupService: LookupService,public elem:ElementRef) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, bStorageService, cdref, ngZone,elem);
  }

  ngOnInit(): void {
    this.entityName = Entities.BalanceSheet;
    this.viewId = ViewName[this.entityTypeName];
    this.showloadingMessage = false;    
    this.isDefaultBtn = true;
    this.toDate = this.FiscalPeriodEndDate;
    
    this.fillReferences();
  }   

  fillReferences() {
    this.lookupService.getAll(LookupApi.VoucherReferences).subscribe(res => {
      var refs = <string[]>res.body;
      this.referenceValues = refs;
    })
  }

  setUrlParameters() {
    
    this.getDataUrl += "?";

    if (this.projectSelected)
      this.getDataUrl += "&projectid=" + this.selectedProjectModel.id;

    if (this.costCenterSelected)
      this.getDataUrl += "&ccenterId=" + this.selectedCostCenterModel.id;    

    if (this.closing)
      this.getDataUrl += "&closing=" + this.closing;

  }

  chkCostCenterChange(checked) {
    if (!checked)
      this.selectedCostCenterModel = undefined;
  }

  chkProjectChange(checked) {
    if (!checked)
      this.selectedProjectModel = undefined;
  }

  validateFilters() {
    var isValid: boolean = true;
    if (this.projectSelected) {
      if (!this.selectedProjectModel) {
        this.showMessage(this.getText('BalanceSheet.SelectProject'));
        isValid = false;
      }
    }

    if (this.costCenterSelected) {
      if (!this.selectedCostCenterModel) {
        this.showMessage(this.getText('BalanceSheet.SelectCostCenter'));
        isValid = false;
      }
    }    

    if (this.compareDate(this.toDate, this.FiscalPeriodStartDate) == -1) {
      this.showMessage(this.getText("Sppc-DateRange.InvalidFpStart"), MessageType.Warning);
      this.toDate = this.FiscalPeriodEndDate;
      isValid = false;
    }
    else if (this.compareDate(this.toDate, this.FiscalPeriodEndDate) == 1) {
      this.showMessage(this.getText("Sppc-DateRange.InvalidFpEnd"), MessageType.Warning);
      this.toDate = this.FiscalPeriodEndDate;
      isValid = false;
    }

    return isValid;
  }

  onAdvanceFilterOk(): any {
    this.enableViewListChanged(this.viewId);
    this.operationId = OperationId.Filter;
    this.getReportData();
  }

  onListChanged() {
    this.listChanged = true;
  }

  setDataUrl() {           
    var endDate = new Date(this.toDate).toDateString();   
    this.getDataUrl = BalanceSheetApi.BalanceSheet;
    this.setUrlParameters();
    this.getDataUrl += "&date=" + endDate; 
  }

  dateChanged() {
    if (this.compareDate(this.toDate, this.FiscalPeriodStartDate) == -1) {
      this.showMessage("تاریخ ابتدا کوچکتر از ابتدای دوره مالی میباشد", MessageType.Warning);
      this.toDate = this.FiscalPeriodEndDate;      
    }
    else if (this.compareDate(this.toDate, this.FiscalPeriodEndDate) == 1) {
      this.showMessage("تاریخ انتها بزرگتر از انتهای دوره مالی میباشد", MessageType.Warning);
      this.toDate = this.FiscalPeriodEndDate;      
    }
  }
 
  @SavePersist()
  getReportData() {
       
    if (!this.validateFilters())
      return;
    
    this.quickFilter = [];        
    this.getDataUrl = "";
    
    this.setDataUrl();

    if (this.branchScopeSelected == "1") {      
        this.quickFilter.push(new Filter("BranchId", this.BranchId.toString(), " == {0}", "System.Int32"));
    }

    switch (this.voucherStatusSelected) {
      case "2": {
        this.quickFilter.push(new Filter("VoucherStatusId", this.voucherStatusSelected, " >= {0}", "System.Int32"));
        break;
      }
      case "3": {
        this.quickFilter.push(new Filter("VoucherStatusId", this.voucherStatusSelected, " == {0}", "System.Int32"));
        break;
      }
      case "4": {
        this.quickFilter.push(new Filter("VoucherConfirmedById", "", " != null", ""));
        break;
      }
      case "5": {
        this.quickFilter.push(new Filter("VoucherApprovedById", "", " != null", ""));
        break;
      }
      default:
    }

    if (this.selectedReferences && this.selectedReferences.length > 0) {
      var referencesFilter: FilterExpression = null;
      var lastItem = this.selectedReferences[this.selectedReferences.length - 1];
      var i = 1;
      this.selectedReferences.forEach((item) => {
        var refFilter = new Filter("VoucherReference", item, " == {0}", "System.String");
        refFilter.id = i.toString();
        referencesFilter = this.addFilterExpressionWithBrace(referencesFilter, refFilter, item === lastItem, this.selectedReferences.length > 1);
      });

      this.customQuickFilter = referencesFilter;
      this.useCustomQuickFilterExpression = true;
    }
    else {
      this.customQuickFilter = undefined;
      this.useCustomQuickFilterExpression = false;
    }
    
    this.reloadGrid();    
  }   

  onChangeFilterByRef() {
    if (!this.isAccess(Entities.BalanceSheet, BalanceSheetPermissions.FilterByRef)) {
      setTimeout(() => {
        this.selectedReferences = [];
      });
      this.showMessage(this.getText('App.AccessDenied'), MessageType.Warning);
    }
  }

  openProjectSelectForm() {

    this.dialogRef = this.dialogService.open({
      content: SelectFormComponent
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.isDisableEntities = true;        
    this.dialogModel.viewID = ViewName.Project;

    this.dialogRef.content.instance.cancel.subscribe((res) => {
      this.dialogRef.close();
    });

    this.dialogRef.content.instance.result.subscribe((res) => {      
      this.selectedProjectModel = res.dataItem;

      this.dialogRef.close();
    });

  }

  openCostCenterSelectForm() {

    this.dialogRef = this.dialogService.open({
      content: SelectFormComponent
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.isDisableEntities = true;

    this.dialogModel.viewID = ViewName.CostCenter;

    this.dialogRef.content.instance.cancel.subscribe((res) => {
      this.dialogRef.close();
    });

    this.dialogRef.content.instance.result.subscribe((res) => {
      this.selectedCostCenterModel = res.dataItem;

      this.dialogRef.close();
    });

  }

  onChangeVoucherStatus() {
    
    let statusFilterExp: FilterExpression = undefined;
    var statusFilter = this.voucherService.getStatusFilter(this.voucherStatusSelected, this.branchScopeSelected == "1" ? this.BranchId.toString() : undefined);

    if (statusFilter.filter.length > 0) {
      statusFilter.filter.forEach(item => {
        statusFilterExp = this.addFilterToFilterExpression(statusFilterExp,
          item, FilterExpressionOperator.And);
      })

      this.voucherService.getVoucherNumberByStatus(VoucherApi.VoucherCountByStatus, statusFilterExp).subscribe(res => {
        if (res > 0)
          this.showMessage(String.Format(this.getText('Messages.VoucherNumberByStatus'), res.toString(), this.getText(statusFilter.key), statusFilter.url), MessageType.Info);
      })
    }    
  }    
}

