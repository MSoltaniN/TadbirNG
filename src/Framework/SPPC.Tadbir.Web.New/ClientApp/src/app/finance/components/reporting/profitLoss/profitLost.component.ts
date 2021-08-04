import { Layout, Entities, environment, MessageType, CustomForm } from "@sppc/env/environment";
import { Component, OnInit, ChangeDetectorRef, Renderer2, NgZone, ViewChild } from "@angular/core";
import { String,AutoGeneratedGridComponent, Filter, FilterExpression, FilterExpressionOperator } from "@sppc/shared/class";
import { RTL } from "@progress/kendo-angular-l10n";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from "@ngx-translate/core";
import { DialogService } from "@progress/kendo-angular-dialog";
import { GridService, MetaDataService, BrowserStorageService, ReportingService, LookupService } from "@sppc/shared/services";
import { ProfitLostService } from "@sppc/finance/service/profitLost.service";
import { SettingService } from "@sppc/config/service";
import { ViewName, ProfitLossPermissions } from "@sppc/shared/security";
import { BranchScopeResource, VoucherStatusResource, ComparativeResource, ComparativeKeys } from "@sppc/finance/enum";
import { Item } from "@sppc/shared/models";
import { SelectFormComponent } from "@sppc/shared/controls";
import { ProfitLossApi, VoucherApi } from "@sppc/finance/service/api";
import { GridComponent } from "@progress/kendo-angular-grid";
import { ViewIdentifierComponent, ReportViewerComponent } from "@sppc/shared/components";
import { ReportManagementComponent } from "@sppc/shared/components/reportManagement/reportManagement.component";
import { QuickReportSettingComponent } from "@sppc/shared/components/reportManagement/QuickReport-Setting.component";
import { VoucherService } from "@sppc/finance/service";
import { QuickReportViewSetting } from "@sppc/shared/components/reportManagement/quickReportViewSetting";
import { ProfitLostLabelsComponent } from "./profitLost.labels.components";
import { LookupApi } from "@sppc/shared/services/api";
import { ReloadOption } from "@sppc/shared/class/reload-option";
import { OperationId } from "@sppc/shared/enum/operationId";

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'sppc-profitlost',
  templateUrl: './profitLost.component.html',
  styles: [`
.section-option { margin-top: 15px; background-color: #f6f6f6; border: solid 1px #dadde2; padding: 15px 15px 0; }
.section-option label,input[type=text] { width:100% } /deep/.section-option kendo-dropdownlist { width:100% }
/deep/ .k-switch-on .k-switch-handle { left: -8px !important; }
/deep/ .k-switch-off .k-switch-handle { left: -4px !important; }
/deep/ .k-switch[dir="rtl"] .k-switch-label-on { right: -22px; }
/deep/ .k-switch[dir="rtl"] .k-switch-label-off { left: 0; }
/deep/ .k-switch-label-on,/deep/ .k-switch-label-off { overflow: initial; }
.journal-type { margin:0 15px 10px; } .journal-type label { margin-top:10px; }
/deep/.k-footer-template { background-color: #b3b3b3; color: #000;}
.btn-compute-default {margin-top: 25px; border: 2px solid #337ab7; color: #337ab7; padding: 5px 25px;}
.btn-compute { color: #337ab7; transition: All 0.3s 0.1s ease-out;}
.btn-compute-selectable{ color: #fff; background-image: linear-gradient(#c1e3ff, #337ab7);}
/deep/ sppc-profitlost .k-grid tr.k-alt {background-color: rgb(248, 251, 253)!important;}
/deep/ sppc-profitlost .k-grid[dir="rtl"] td, .k-rtl .k-grid td { border-width: 0 0px 0 0!important;border: 0!important;}
.section-account button { margin: 0 2px; }
.section-account .acc-name{ width: 88% }
.section-account .acc-code{ width: 57%; position: absolute; top: -5px; }
.section-account .acc-code-rtl { left: 16px; }
.section-account .acc-code-ltr { right: 16px; }
.section-account label {width:35%}
.comparative kendo-dropdownlist {width:89%;float:left}
`],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class ProfitLostComponent extends AutoGeneratedGridComponent implements OnInit {

  fromDate: Date;
  toDate: Date;

  selectedCostCenterModel: any;
  selectedProjectModel: any;
  branchScopeSelected: string = '1';
  voucherStatusSelected: string = '2';
  filterByRef: string;
  param: any;
  paramChanged: boolean;

  comparativeItemSelected: string;
  comparativeItems: Array<any>;
  projectSelected: boolean;
  costCenterSelected: boolean;
  showFilterByRef: boolean;
  comparativeSelected: boolean;
  closing: boolean;
  tax: number;
  isDefaultBtn: boolean = true;
  oldDataUrl: string;

  referenceValues: string[];
  selectedReferences: string[];

  branchScope: Array<Item> = [
    { value: BranchScopeResource.CurrentBranch, key: "1" },
    { value: BranchScopeResource.CurrentBranchAndSubsets, key: "2" },
  ]

  comparative: Array<Item> = [
    { value: ComparativeResource.Branch, key: "1" },
    { value: ComparativeResource.FiscalPeriod, key: "2" },
    { value: ComparativeResource.CostCenter, key: "3" },
    { value: ComparativeResource.Project, key: "4" },
  ]

  voucherStatus: Array<Item> = [
    { value: VoucherStatusResource.Committed, key: "2" },
    { value: VoucherStatusResource.Finalized, key: "3" },
    { value: VoucherStatusResource.Confirmed, key: "4" },
    { value: VoucherStatusResource.Approved, key: "5" },
    { value: VoucherStatusResource.AllVouchers, key: "0" }
  ]

  viewSetting: QuickReportViewSetting = { hideHorizontalLine : true, hideVerticalLine : true}; 

  @ViewChild(GridComponent) grid: GridComponent;
  @ViewChild(ViewIdentifierComponent) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportViewerComponent) viewer: ReportViewerComponent;
  @ViewChild(ReportManagementComponent) reportManager: ReportManagementComponent;
  @ViewChild(QuickReportSettingComponent) reportSetting: QuickReportSettingComponent;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public dialogService: DialogService, public gridService: GridService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, public profitService: ProfitLostService,
    public bStorageService: BrowserStorageService, public settingService: SettingService, public reporingService: ReportingService,
    public ngZone: NgZone, public voucherService: VoucherService, public lookupService: LookupService) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, bStorageService, cdref, ngZone);
  }

  ngOnInit(): void {
    this.entityName = Entities.ProfitLost;
    this.viewId = ViewName[this.entityTypeName];
    this.showloadingMessage = false;
    this.tax = 0;
    this.isDefaultBtn = true;
    this.customListChanged = true;
    this.fillReferences();
  }
    
  dateValueChange(event: any) {
    this.fromDate = event.fromDate;
    this.toDate = event.toDate;      
  }

  fillReferences() {
    this.lookupService.getAll(LookupApi.VoucherReferences).subscribe(res => {
      var refs = <string[]>res.body;
      this.referenceValues = refs;
    })
  }

  setUrlParameters() {
    
    this.getDataUrl += "?tax=" + this.tax;

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
        this.showMessage(this.getText('ProfitLoss.SelectProject'));
        isValid = false;
      }
    }

    if (this.costCenterSelected) {
      if (!this.selectedCostCenterModel) {
        this.showMessage(this.getText('ProfitLoss.SelectCostCenter'));
        isValid = false;
      }
    }

    if (this.showFilterByRef) {
      if (!this.filterByRef) {
        this.showMessage(this.getText('ProfitLoss.FilterByRefMsg'));
        isValid = false;
      }
    }

    if (this.comparativeSelected) {
      if (!this.comparativeItemSelected || !this.comparativeItems || this.comparativeItems.length == 0) {
        this.showMessage(this.getText('ProfitLoss.ComparativeSelectItems'));
        isValid = false;
      }
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
   

    var startDate = new Date(this.fromDate).toDateString();
    var endDate = new Date(this.toDate).toDateString();
    this.listChanged = false;

    //simple
    if (startDate == endDate) {
      if (!this.comparativeSelected) {
        this.entityName = Entities.ProfitLossSimple;
        this.viewId = ViewName[this.entityTypeName];        
        this.getDataUrl = ProfitLossApi.ProfitLossSimple;
        if (this.oldDataUrl != ProfitLossApi.ProfitLossSimple) {
          this.onListChanged();
          this.oldDataUrl = ProfitLossApi.ProfitLossSimple;
        }
      }
      else {

        //branch
        if (this.comparativeItemSelected == ComparativeKeys.Branch) {
          this.getDataUrl = ProfitLossApi.ProfitLossSimpleByBranches;
          if (this.oldDataUrl != ProfitLossApi.ProfitLossSimpleByBranches) {
            this.onListChanged();
            this.oldDataUrl = ProfitLossApi.ProfitLossSimpleByBranches;
          }
        }

        //fiscal priod
        if (this.comparativeItemSelected == ComparativeKeys.FiscalPeriod) {
          this.getDataUrl = ProfitLossApi.ProfitLossSimpleByFiscalPeriods;
          if (this.oldDataUrl != ProfitLossApi.ProfitLossSimpleByFiscalPeriods) {
            this.onListChanged();
            this.oldDataUrl = ProfitLossApi.ProfitLossSimpleByFiscalPeriods;
          }
        }

        //project
        if (this.comparativeItemSelected == ComparativeKeys.Project) {
          this.getDataUrl = ProfitLossApi.ProfitLossSimpleByProjects;
          if (this.oldDataUrl != ProfitLossApi.ProfitLossSimpleByProjects) {
            this.onListChanged();
            this.oldDataUrl = ProfitLossApi.ProfitLossSimpleByProjects;
          }
        }

        //costcenter
        if (this.comparativeItemSelected == ComparativeKeys.CostCenter) {
          this.getDataUrl = ProfitLossApi.ProfitLossSimpleByCostCenters;
          if (this.oldDataUrl != ProfitLossApi.ProfitLossSimpleByCostCenters) {
            this.onListChanged();
            this.oldDataUrl = ProfitLossApi.ProfitLossSimpleByCostCenters;
          }
        }
      }

      this.setUrlParameters();
      this.getDataUrl += "&date=" + this.fromDate;
    }
    //not simple
    else {
      if (!this.comparativeSelected) {
        this.getDataUrl = ProfitLossApi.ProfitLoss;
        this.entityName = Entities.ProfitLost;
        this.viewId = ViewName[this.entityTypeName];

        if (this.oldDataUrl != ProfitLossApi.ProfitLoss) {
          this.onListChanged();
          this.oldDataUrl = ProfitLossApi.ProfitLoss;
        }
      }
      else {

        //branch
        if (this.comparativeItemSelected == ComparativeKeys.Branch) {
          this.getDataUrl = ProfitLossApi.ProfitLossByBranches;
          if (this.oldDataUrl != ProfitLossApi.ProfitLossByBranches) {
            this.onListChanged();
            this.oldDataUrl = ProfitLossApi.ProfitLossByBranches;
          }
        }

        //fiscal priod
        if (this.comparativeItemSelected == ComparativeKeys.FiscalPeriod) {
          this.getDataUrl = ProfitLossApi.ProfitLossByFiscalPeriods;
          if (this.oldDataUrl != ProfitLossApi.ProfitLossByFiscalPeriods) {
            this.onListChanged();
            this.oldDataUrl = ProfitLossApi.ProfitLossByFiscalPeriods;
          }
        }

        //project
        if (this.comparativeItemSelected == ComparativeKeys.Project) {
          this.getDataUrl = ProfitLossApi.ProfitLossByProjects;
          if (this.oldDataUrl != ProfitLossApi.ProfitLossByProjects) {
            this.onListChanged();
            this.oldDataUrl = ProfitLossApi.ProfitLossByProjects;
          }
        }

        //costcenter
        if (this.comparativeItemSelected == ComparativeKeys.CostCenter) {
          this.getDataUrl = ProfitLossApi.ProfitLossByCostCenters;
          if (this.oldDataUrl != ProfitLossApi.ProfitLossByCostCenters) {
            this.onListChanged();
            this.oldDataUrl = ProfitLossApi.ProfitLossByCostCenters;
          }
        }
      }

      this.setUrlParameters();
      this.getDataUrl += "&from=" + this.fromDate + "&to=" + this.toDate;
    }

    if (!this.comparativeSelected)
      this.refetchGridColumns();
  }
 
  getReportData() {
       
    if (!this.validateFilters())
      return;
    
    this.quickFilter = [];        
    this.getDataUrl = "";
    
    this.setDataUrl();

    if (this.branchScopeSelected == "1") {
      if ((this.comparativeSelected && this.comparativeItemSelected.toString() != ComparativeKeys.Branch) || !this.comparativeSelected)
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

    if (!this.comparativeSelected)
      this.reloadGrid();
    else {
      //with param
      if (this.parameters.length == 0) {
        this.showMessage(this.getText('ProfitLoss.ComparativeSelectItems'));
        return;
      }      

      this.param = { paraf: "", items: this.parameters };

      var options = new ReloadOption();
      options.Parameter = this.param;
      this.reloadGrid(options);
    }
  } 

  onBeforeQuickReportSetting() {
    //clear local sotrage state for prev comparative report
    if ((this.comparativeSelected && this.param && this.parameters != this.param.items) || this.paramChanged) {
      this.reportSetting.clearCurrentState(this.viewId);
      this.paramChanged = false;
    }
  }

  onChangeFilterByRef(event)
  {
    if (!this.isAccess(Entities.ProfitLost, ProfitLossPermissions.FilterByRef)) {
      setTimeout(() => {
        this.selectedReferences = [];
      });
      this.showMessage(this.getText('App.AccessDenied'), MessageType.Warning);
    }    
  }

  onChangeComparativeSelected(checked) {
    if (!checked) {
      this.comparativeSelected = false;
      this.param = [];      
      this.setDataUrl();
    }
    else
      this.reportSetting.clearCurrentState(this.viewId);
      this.comparativeSelected = true;
  }

  openComparativeSelectForm() {
    if (!this.comparativeItemSelected) {
      this.showMessage(this.getText('ProfitLoss.ComparativeSelect'));
      return;
    }

    this.dialogRef = this.dialogService.open({
      content: SelectFormComponent
    });    

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.isDisableEntities = true;
    this.dialogModel.allEntities = true;
    this.dialogModel.multipleSelect = true;
    this.dialogModel.maxSelectionCount = 5;

    switch (this.comparativeItemSelected) {
      case ComparativeKeys.Branch:
        this.dialogModel.viewID = ViewName.Branch;
        break;
      case ComparativeKeys.CostCenter:
        this.dialogModel.viewID = ViewName.CostCenter;
        break;
      case ComparativeKeys.FiscalPeriod:
        this.dialogModel.viewID = ViewName.FiscalPeriod;
        break;
      case ComparativeKeys.Project:
        this.dialogModel.viewID = ViewName.Project;
        break;
    }    

    this.dialogRef.content.instance.cancel.subscribe((res) => {
      this.dialogRef.close();
    });

    this.dialogRef.content.instance.result.subscribe((res) => {
      this.parameters = [];
      this.comparativeItems = res;      
      this.paramChanged = true;

      res.dataItem.forEach((item)=>{
        this.parameters.push(item);
      });

      this.dialogRef.close();
    });

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
    var statusFilter = this.voucherService.getStatusFilter(this.voucherStatusSelected);

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

  onLabelCustomizeClick() {
    this.dialogRef = this.dialogService.open({
      title: this.getText('ProfitLoss.ChangeLabel'),
      content: ProfitLostLabelsComponent,
      height: 550
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.formId = CustomForm.ProfitLoss;
    
    this.dialogRef.content.instance.cancel.subscribe((res) => {
      this.dialogRef.close();
    });

    this.dialogRef.content.instance.save.subscribe((res) => {      
      this.getReportData();
      this.dialogRef.close();
    });
  }

}

