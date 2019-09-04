import { Component, OnInit, OnDestroy, ChangeDetectorRef, Renderer2, NgZone, ViewChild } from '@angular/core';
import { AutoGeneratedGridComponent, Filter, FilterExpressionOperator } from '@sppc/shared/class';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { MetaDataService, BrowserStorageService, ReportingService, GridService } from '@sppc/shared/services';
import { SettingService } from '@sppc/config/service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DialogService, DialogRef } from '@progress/kendo-angular-dialog';
import { Entities, MessageType, Layout } from '@sppc/env/environment';
import { ViewName } from '@sppc/shared/security';
import { QuickReportSettingComponent } from '@sppc/shared/components/reportManagement/QuickReport-Setting.component';
import { GridComponent, ColumnBase } from '@progress/kendo-angular-grid';
import { ViewIdentifierComponent, ReportViewerComponent } from '@sppc/shared/components';
import { ReportManagementComponent } from '@sppc/shared/components/reportManagement/reportManagement.component';
import { Item } from '@sppc/shared/models';
import { BalanceDisplayTypeResource, VoucherStatusResource, BranchScopeResource, BalanceFormatType, BalanceType, BalanceDisplayType, BalanceFormatTypeResource } from '@sppc/finance/enum/balance';
import { SelectFormComponent } from '@sppc/shared/controls';
import { RTL } from '@progress/kendo-angular-l10n';
import { VoucherApi, AccountBookApi } from '@sppc/finance/service/api';
import { TestBalanceApi } from '@sppc/finance/service/api/testBalanceApi';
import { String }  from '@sppc/shared/class';

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'sppc-testBalance',
  templateUrl: './testBalance.component.html',
  styles: [`
.section-account button { margin: 0 2px; }
.section-account .acc-name{ width: calc(100% - 102px); }
.section-account .acc-code{ width: calc(100% - 131px); position: absolute; top: -5px; }
.section-account .acc-code-rtl { left: 16px; }
.section-account .acc-code-ltr { right: 16px; }

.section-option { margin-top: 15px; background-color: #f6f6f6; border: solid 1px #dadde2; padding: 15px 15px 0; }
.section-option label,input[type=text] { width:100% } /deep/.section-option kendo-dropdownlist { width:100% }
/deep/ .k-switch-on .k-switch-handle { left: -8px !important; }
/deep/ .k-switch-off .k-switch-handle { left: -4px !important; }
/deep/ .k-switch[dir="rtl"] .k-switch-label-on { right: -22px; }
/deep/ .k-switch[dir="rtl"] .k-switch-label-off { left: 0; }
/deep/ .k-switch-label-on,/deep/ .k-switch-label-off { overflow: initial; }
.test-balance { margin:0 15px 10px; } .test-balance label { margin-top:10px; }
/deep/.k-footer-template { background-color: #b3b3b3; color: #000;}
.btn-compute-default {margin-top: 25px; border: 2px solid #337ab7; color: #337ab7; padding: 5px 25px;}
.btn-compute { color: #337ab7; transition: All 0.3s 0.1s ease-out;}
.btn-compute-selectable{ color: #fff; background-image: linear-gradient(#c1e3ff, #337ab7);}
.acc-options {margin-top:30px;}
/deep/ .k-header k-grid-draggable-header { text-align: center !important; }
`],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class TestBalanceComponent extends AutoGeneratedGridComponent implements OnInit, OnDestroy {


  displayType: Array<Item> = [
    { value: BalanceDisplayTypeResource.ByLedger, key: "1" },
    { value: BalanceDisplayTypeResource.BySubsidiary, key: "2" },
    { value: BalanceDisplayTypeResource.ByDetail, key: "3" },
    { value: BalanceDisplayTypeResource.BySubsidiaryOfLeader, key: "4" },
    { value: BalanceDisplayTypeResource.ByDetailOfSubsidiary, key: "5" }
  ]

  formatTypes: Array<Item> = [
    { value: BalanceFormatTypeResource.Balance2Column, key: BalanceFormatType.Balance2Column },
    { value: BalanceFormatTypeResource.Balance4Column, key: BalanceFormatType.Balance4Column},
    { value: BalanceFormatTypeResource.Balance6Column, key: BalanceFormatType.Balance6Column  },
    { value: BalanceFormatTypeResource.Balance8Column, key: BalanceFormatType.Balance8Column }
  ]

  voucherStatus: Array<Item> = [
    { value: VoucherStatusResource.Committed, key: "2" },
    { value: VoucherStatusResource.Finalized, key: "3" },
    { value: VoucherStatusResource.Confirmed, key: "4" },
    { value: VoucherStatusResource.Approved, key: "5" },
    { value: VoucherStatusResource.AllVouchers, key: "0" }
  ]
  branchScope: Array<Item> = [
    { value: BranchScopeResource.CurrentBranch, key: "1" },
    { value: BranchScopeResource.CurrentBranchAndSubsets, key: "2" },
  ]

  bookType: Array<any> = [];
 

  private docClickSubscription: any;
  public formGroup: FormGroup;

  @ViewChild(GridComponent) grid: GridComponent;
  @ViewChild(ViewIdentifierComponent) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportViewerComponent) viewer: ReportViewerComponent;
  @ViewChild(ReportManagementComponent) reportManager: ReportManagementComponent;
  @ViewChild(QuickReportSettingComponent) reportSetting: QuickReportSettingComponent;

  fromDate: Date;
  toDate: Date;
  fromVoucher: string;
  toVoucher: string;
  testBalanceType: string = '1';

  creditSum: number = 0;
  debitSum: number = 0;

  dialogRef: DialogRef;
  dialogModel: any;
  selectedViewId: number;
  selectedModel: any;
  selectedModelTitle: string;
  baseModelTitle: string;
  modelUrl: string;
  selectedBookType: number = 1;

  isClosing: boolean = false;
  isCloseAccounts: boolean = false;
  isDefaultBtn: boolean = true;
  isApplyBranchSeparation: boolean = false;

  formatSelected: string = "6";
  displayTypeSelected: string = '1';
  branchScopeSelected: string = '1';
  voucherStatusSelected: string = '2';
  articleTypeSelected: string = '1';
  selectedBranchSeparation: boolean = false;
  gridColumnsRow: any[] = [];
  gridGroupColumnsRow: any[] = [];
  gridGroupColumnNames: any[] = [];

  constructor(public toastrService: ToastrService, public translate: TranslateService, public dialogService: DialogService, public gridService: GridService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, public bStorageService: BrowserStorageService,
    public settingService: SettingService, public reporingService: ReportingService, public ngZone: NgZone, public formBuilder: FormBuilder) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, bStorageService, cdref, ngZone);
  }

  ngOnInit() {
    this.entityName = Entities.TestBalance6Column;
    this.viewId = ViewName[this.entityTypeName];

    this.translate.get('Entity.Account').subscribe(res => {
      this.baseModelTitle = res;
      this.selectedModelTitle = this.baseModelTitle;
    })

    this.getFirstAndLastVoucherNo();

    this.showloadingMessage = false;

  }

  getFirstAndLastVoucherNo() {
    this.gridService.getModels(VoucherApi.EnvironmentItemRange).subscribe(res => {
      this.fromVoucher = res.firstNo.toString();
      this.toVoucher = res.lastNo.toString();
    })
  }

  ngOnDestroy(): void {
    
  }

  nextModel() {
    if (this.selectedModel)
      this.modelUrl = String.Format(AccountBookApi.NextEnvironmentItem, this.selectedViewId, this.selectedModel.id);

    this.getModel();
  }

  getModel() {
    this.gridService.getModels(this.modelUrl).subscribe(res => {
      this.changeParam();
      this.selectedModel = res;
      this.initValue();
    }, error => {
      if (error.status)
        this.showMessage(this.getText('App.RecordNotFound'), MessageType.Warning);
    })
  }

  previousModel() {
    if (this.selectedModel)
      this.modelUrl = String.Format(AccountBookApi.PreviousEnvironmentItem, this.selectedViewId, this.selectedModel.id);

    this.getModel();
  }

  changeParam() {
    this.isDefaultBtn = false;

    this.creditSum = 0;
    this.debitSum = 0;
    this.selectedRows = [];
    this.pageIndex = 0;
    this.showloadingMessage = false;
    this.totalRecords = 0;
    this.rowData = undefined;
  }

  prepareColumns() {
    if (!this.selectedBranchSeparation) {
      this.gridColumnsRow = this.gridColumns.filter(f => f.name != "BranchName" && f.groupName == "");
      this.gridGroupColumnsRow = this.gridColumns.filter(f => f.groupName);

      this.gridGroupColumnNames = this.gridGroupColumnsRow.filter((item, pos) => {
        return this.gridGroupColumnsRow.findIndex(i => i.groupName === item.groupName) == pos;
      });
    }
    else {
      this.gridColumnsRow = this.gridColumns.filter(f => f.groupName == "");;
      this.gridGroupColumnsRow = this.gridColumns.filter(f => f.groupName);

      this.gridGroupColumnNames = this.gridColumns.filter(function (item, pos) {
        return this.gridColumns.findIndex(i => i.groupName === item.groupName) == pos;
      });
    }
  }

  getColumns(e: any) {
    this.gridColumns = e;

    this.prepareColumns();
  }

  getColumnRows(item) {
    return this.gridGroupColumnsRow.filter(col => col.groupName === item.groupName);
  }

  changedVoucherNum() {
    this.isDefaultBtn = false;
  }

  dateValueChange(event: any) {
    this.fromDate = event.fromDate;
    this.toDate = event.toDate;
    this.isDefaultBtn = false;
  }

  changeBranchSeparation() {
    //if (this.isAccess(Entities.Journal, JournalPermissions.ByBranch)) {
      //this.isApplyBranchSeparation = true;
      //if (!this.selectedBranchSeparation) {
      //  this.gridColumnsRow = this.gridColumns.filter(f => f.name != "BranchName");
      //}
      //else {
      //  this.gridColumnsRow = this.gridColumns;
      //}
      this.changeParam();

      this.prepareColumns()
  }
    //else {
    //  this.isApplyBranchSeparation = false;
    //  this.showMessage(this.getText('App.AccessDenied'), MessageType.Warning);
    //}


  openSelectForm() {
    this.dialogRef = this.dialogService.open({
      content: SelectFormComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;

    this.dialogModel.viewID = this.selectedViewId;

    this.dialogRef.content.instance.cancel.subscribe((res) => {
      this.dialogRef.close();
    });

    this.dialogRef.content.instance.result.subscribe((res) => {
      this.changeParam();
      this.selectedModel = res.dataItem;
      this.selectedViewId = res.viewId;
      this.initValue();
      this.dialogRef.close();
    });

  }

  initValue() {
    this.selectedModelTitle = this.baseModelTitle;
    var model = this.bookType.find(f => f.viewId == this.selectedViewId && f.level == this.selectedModel.level);
    if (model) {
      this.selectedBookType = model.key;

      if (this.selectedViewId != 1)
        this.selectedModelTitle = model.title;
    }
  }

  changeType() {

    this.changeParam();
  }

  changeFormatType() {

    switch (this.formatSelected) {
      case '2':
        this.entityName = Entities.TestBalance2Column;        
        break;
      case '4':
        this.entityName = Entities.TestBalance4Column;        
        break;
      case '6':
        this.entityName = Entities.TestBalance6Column;        
        break;
      case '8':
        this.entityName = Entities.TestBalance8Column;        
        break;
    }

    this.viewId = ViewName[this.entityTypeName];

    this.changeParam();
  }

  getReportData() {
    if (this.testBalanceType) {

      this.changeParam();

      this.defaultFilter = [];

      switch (this.voucherStatusSelected) {
        case "2": {
          this.defaultFilter.push(new Filter("VoucherStatusId", this.voucherStatusSelected, "== {0}", "System.Int32"));
          break;
        }
        case "3": {
          this.defaultFilter.push(new Filter("VoucherStatusId", this.voucherStatusSelected, "== {0}", "System.Int32"));
          break;
        }
        case "4": {
          this.defaultFilter.push(new Filter("VoucherConfirmedById", "", "!= null", ""));
          break;
        }
        case "5": {
          this.defaultFilter.push(new Filter("VoucherApprovedById", "", "!= null", ""));
          break;
        }
        default:
      }

      if (this.branchScopeSelected == "1") {
        this.defaultFilter.push(new Filter("BranchId", this.BranchId.toString(), "== {0}", "System.Int32"));
      }
      
      if (!this.isApplyBranchSeparation)
        this.selectedBranchSeparation = false;

      
      switch (parseInt(this.displayTypeSelected)) {
        case BalanceDisplayType.ByLedger:
            {

            switch (this.formatSelected) {
              case BalanceFormatType.Balance2Column:
                this.entityName = Entities.TestBalance2Column;
                this.getDataUrl = TestBalanceApi.TwoColumnLedgerBalance;
                break;
              case BalanceFormatType.Balance4Column:
                this.entityName = Entities.TestBalance4Column;
                this.getDataUrl = TestBalanceApi.FourColumnLedgerBalance;
                break;
              case BalanceFormatType.Balance6Column:
                this.entityName = Entities.TestBalance6Column;
                this.getDataUrl = TestBalanceApi.SixColumnLedgerBalance;
                break;
              case BalanceFormatType.Balance8Column:
                this.entityName = Entities.TestBalance8Column;
                this.getDataUrl = TestBalanceApi.EightColumnLedgerBalance;
                break;
            }
            

              break;
          }

        case BalanceDisplayType.BySubsidiary: {

            switch (this.formatSelected) {
              case BalanceFormatType.Balance2Column:
                this.entityName = Entities.TestBalance2Column;
                this.getDataUrl = TestBalanceApi.TwoColumnSubsidiaryBalance;
                break;
              case BalanceFormatType.Balance4Column:
                this.entityName = Entities.TestBalance4Column;
                this.getDataUrl = TestBalanceApi.FourColumnSubsidiaryBalance;
                break;
              case BalanceFormatType.Balance6Column:
                this.entityName = Entities.TestBalance6Column;
                this.getDataUrl = TestBalanceApi.SixColumnSubsidiaryBalance;
                break;
              case BalanceFormatType.Balance8Column:
                this.entityName = Entities.TestBalance8Column;
                this.getDataUrl = TestBalanceApi.EightColumnSubsidiaryBalance;
                break;
            }


            break;
        }

        case BalanceDisplayType.ByDetail: {

          switch (this.formatSelected) {
            case BalanceFormatType.Balance2Column:
              this.entityName = Entities.TestBalance2Column;
              this.getDataUrl = TestBalanceApi.TwoColumnDetailBalance;
              break;
            case BalanceFormatType.Balance4Column:
              this.entityName = Entities.TestBalance4Column;
              this.getDataUrl = TestBalanceApi.FourColumnDetailBalance;
              break;
            case BalanceFormatType.Balance6Column:
              this.entityName = Entities.TestBalance6Column;
              this.getDataUrl = TestBalanceApi.SixColumnDetailBalance;
              break;
            case BalanceFormatType.Balance8Column:
              this.entityName = Entities.TestBalance8Column;
              this.getDataUrl = TestBalanceApi.EightColumnDetailBalance;
              break;
          }


          break;
        }
          
          default:
        }

      if (this.testBalanceType == BalanceType.ByDate)
          this.getDataUrl += "?from=" + this.fromDate + "&to=" + this.toDate;

      if (this.testBalanceType == BalanceType.ByVoucher)
        this.getDataUrl += "?from=" + this.fromVoucher + "&to=" + this.toVoucher;
      
      this.getDataUrl += "&byBranch=" + this.selectedBranchSeparation;
      

      this.reloadGrid();
      

      
    }

  }

  reloadGrid(insertedModel?: any) {
    if (this.getDataUrl) {
      this.grid.loading = true;

      if (this.totalRecords == this.skip && this.totalRecords != 0) {
        this.skip = this.skip - this.pageSize;
      }

      if (insertedModel)
        this.goToLastPage(this.totalRecords);

      this.reportFilter = null;

      var currentFilter = this.currentFilter;
      this.defaultFilter.forEach(item => {
        currentFilter = this.addFilterToFilterExpression(currentFilter,
          item, FilterExpressionOperator.And);
      })
      var filter = currentFilter;

      this.reportFilter = filter;

      this.gridService.getAll(this.getDataUrl, this.pageIndex, this.pageSize, this.sort, filter).subscribe((res) => {

        var resData = res.body;
        var totalCount = 0;

        if (res.headers != null) {
          var headers = res.headers != undefined ? res.headers : null;
          if (headers != null) {
            var retheader = headers.get('X-Total-Count');
            if (retheader != null)
              totalCount = parseInt(retheader.toString());
          }
        }
        this.rowData = {
          data: resData.items,
          total: totalCount
        }

        this.viewId = this.tempViewId;

        if (this.rowData && this.rowData.total > 0) {
          var columnsToFit: Array<ColumnBase> = new Array<ColumnBase>();
          this.grid.leafColumns.forEach(function (item) {
            var column = item as ColumnBase;
            if (column.width == undefined) {
              columnsToFit.push(column);
            }
          });
          this.fitColumns(columnsToFit);
        }
        this.isDefaultBtn = true;
        this.creditSum = resData.creditSum;
        this.debitSum = resData.debitSum;

        this.showloadingMessage = !(resData.items.length == 0);
        this.totalRecords = totalCount;
        this.grid.loading = false;
      })
    }
    this.cdref.detectChanges();
  }

  public showReport() {
    if (this.validateReport()) {
      if (!this.reportManager.directShowReport()) {
        this.showMessage(this.getText("Report.PleaseSetQReportSetting"));
        this.showReportSetting();
      }
    }
  }

  public validateReport() {
    if (!this.rowData || this.rowData.total == 0) {
      this.showMessage(this.getText("Report.QuickReportValidate"));
      return false;
    }
    return true;
  }

  public showReportSetting() {
    if (this.validateReport()) {
      this.reportSetting.showReportSetting(this.gridColumns, this.entityTypeName, this.viewId, this.reportManager);
    }
  }


}


  

