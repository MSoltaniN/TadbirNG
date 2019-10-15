import { Component, OnInit, OnDestroy, ChangeDetectorRef, Renderer2, NgZone, ViewChild } from '@angular/core';
import { AutoGeneratedGridComponent, Filter, FilterExpressionOperator } from '@sppc/shared/class';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { MetaDataService, BrowserStorageService, ReportingService, GridService, SessionKeys } from '@sppc/shared/services';
import { SettingService } from '@sppc/config/service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DialogService, DialogRef } from '@progress/kendo-angular-dialog';
import { Entities, MessageType, Layout } from '@sppc/env/environment';
import { ViewName, TestBalancePermissions } from '@sppc/shared/security';
import { QuickReportSettingComponent } from '@sppc/shared/components/reportManagement/QuickReport-Setting.component';
import { GridComponent, ColumnBase } from '@progress/kendo-angular-grid';
import { ViewIdentifierComponent, ReportViewerComponent, AdvanceFilterComponent } from '@sppc/shared/components';
import { ReportManagementComponent } from '@sppc/shared/components/reportManagement/reportManagement.component';
import { Item, FilterColumn } from '@sppc/shared/models';
import { BalanceDisplayTypeResource, VoucherStatusResource, BranchScopeResource, BalanceFormatType, BalanceType, BalanceDisplayType, BalanceFormatTypeResource, TestBalanceOptions } from '@sppc/finance/enum/balance';
import { SelectFormComponent } from '@sppc/shared/controls';
import { RTL } from '@progress/kendo-angular-l10n';
import { VoucherApi, AccountBookApi } from '@sppc/finance/service/api';
import { TestBalanceApi } from '@sppc/finance/service/api/testBalanceApi';
import { String }  from '@sppc/shared/class';
import { AccountInfo, TestBalanceService } from '@sppc/finance/service';
import { TestBalanceModeInfo } from '@sppc/finance/models/testBalanceModeInfo';
import { AccountBookComponent } from '@sppc/finance/components/reporting/accountBook/accountBook.component';
import { SettingKey } from '@sppc/shared/enum';

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
.lm {margin-left:4px!important;}
/deep/ .k-header k-grid-draggable-header { text-align: center !important; }
`],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class TestBalanceComponent extends AutoGeneratedGridComponent implements OnInit, OnDestroy {


  //displayType: Array<Item> = [
  //  { value: BalanceDisplayTypeResource.ByLedger, key: "1" },
  //  { value: BalanceDisplayTypeResource.BySubsidiary, key: "2" },
  //  { value: BalanceDisplayTypeResource.ByDetail, key: "3" },
  //  { value: BalanceDisplayTypeResource.BySubsidiaryOfLeader, key: "4" },
  //  { value: BalanceDisplayTypeResource.ByDetailOfSubsidiary, key: "5" }
  //]

  displayType: Array<TestBalanceModeInfo>;
  displayTypeName: string;

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
  breadCrumbList: Array<any> = [];

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

  endBalanceCredit: number = 0;
  endBalanceDebit: number = 0;  
  startBalanceCredit: number = 0;
  startBalanceDebit: number = 0;
  turnoverCredit: number = 0;
  turnoverDebit: number = 0;
  operationSumCredit: number = 0;
  operationSumDebit: number = 0;

  dialogRef: DialogRef;
  dialogModel: any;

  selectedViewId: number;
  selectedModel: any;
  selectedModelTitle: string;
  baseModelTitle: string;
  modelUrl: string;
  selectedBookType: number = 1;

  activeOptions: boolean = false;
  useClosingVoucher: boolean;
  useClosingTempVoucher: boolean;
  openingVoucherAsInitBalance: boolean;
  showZeroBalanceItems: boolean;
  isDefaultBtn: boolean = true;
  isApplyBranchSeparation: boolean = false;
  disableAccountLookup: boolean = true;

  formatSelected: string = "6";
  displayTypeSelected: number = 0;
  branchScopeSelected: string = '1';
  voucherStatusSelected: string = '2';
  articleTypeSelected: string = '1';
  selectedBranchSeparation: boolean = false;
  gridColumnsRow: any[] = [];
  gridGroupColumnsRow: any[] = [];
  gridGroupColumnNames: any[] = [];

  addOpeningVoucherToInitBalance: boolean;

  clickedRowItem :any;
  
  constructor(public toastrService: ToastrService, public translate: TranslateService, public dialogService: DialogService, public gridService: GridService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, public bStorageService: BrowserStorageService,
    public settingService: SettingService, public reporingService: ReportingService, public ngZone: NgZone, public formBuilder: FormBuilder
    , public testBalanceService: TestBalanceService) {
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
    this.fillDisplayTypes();
    this.showloadingMessage = false;

    this.settingService.getSettingById(SettingKey.TestBalanceConfig).subscribe((res) => {
      if (res) {        
        this.openingVoucherAsInitBalance = res.values.addOpeningVoucherToInitBalance;
      }
    });

    this.loadStates();
    
  }

  fillDisplayTypes() {
    this.testBalanceService.getAll(TestBalanceApi.TestBalanceTypeLookup).subscribe(res => {
      var modes = <Array<TestBalanceModeInfo>>res.body;
      this.displayType = modes;
    })
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

  okDialog() {
    this.closeOptions();
  }

  showOptions() {
    this.activeOptions = true;
  }

  closeOptions() {
    this.activeOptions = false;
  }

  previousModel() {
    if (this.selectedModel)
      this.modelUrl = String.Format(AccountBookApi.PreviousEnvironmentItem, this.selectedViewId, this.selectedModel.id);

    this.getModel();
  }

  changeParam(clearBreadCrumb:boolean = true) {
    this.isDefaultBtn = false;

    this.endBalanceCredit = 0;
    this.endBalanceDebit = 0;
    this.turnoverCredit = 0;
    this.turnoverDebit = 0;
    this.startBalanceCredit = 0;
    this.startBalanceDebit = 0;

    this.selectedRows = [];
    this.pageIndex = 0;
    this.showloadingMessage = false;
    this.totalRecords = 0;
    this.rowData = undefined;

    if (clearBreadCrumb && this.breadCrumbList.length) {
      var relatedDisplayType = this.displayType.filter(d => d.id === this.displayTypeSelected)[0];
      if (this.breadCrumbList.length > 0
        && this.breadCrumbList[this.breadCrumbList.length - 1].displayType.level > relatedDisplayType.level) {
        this.breadCrumbList = new Array<any>();
      }
      this.displayTypeName = relatedDisplayType.name;
    }    
  
  }

  prepareColumns() {
    if (!this.selectedBranchSeparation) {
      this.gridColumnsRow = this.gridColumns.filter(f => f.name != "BranchName" && f.groupName == "");
      this.gridGroupColumnsRow = this.gridColumns.filter(f => f.groupName);

      this.gridGroupColumnNames = this.gridGroupColumnsRow.filter((item, pos) => {
        return this.gridGroupColumnsRow.findIndex(i => i.groupName === item.groupName) == pos;
      });
    }
    else if (this.isApplyBranchSeparation){
      this.gridColumnsRow = this.gridColumns.filter(f => f.groupName == "");;
      this.gridGroupColumnsRow = this.gridColumns.filter(f => f.groupName);

      this.gridGroupColumnNames = this.gridColumns.filter((item, pos) => {
        return this.gridColumns.findIndex(i => i.groupName === item.groupName && item.groupName != "") == pos;
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

  getSumValue(gcn) {
    var value = 0;

    switch (gcn.name) {
      case "StartBalanceCredit":
        value = this.startBalanceCredit;
        break;
      case "EndBalanceCredit":
        value = this.endBalanceCredit;
        break;
      case "TurnoverCredit":
        value = this.turnoverCredit;
        break;
      case "StartBalanceDebit":        
        value = this.startBalanceDebit;
        break;
      case "EndBalanceDebit":
        value = this.endBalanceDebit;
        break;
      case "TurnoverDebit":
        value = this.turnoverDebit;
        break;
      case "OperationSumDebit":
        value = this.operationSumDebit;
        break;
      case "OperationSumCredit":
        value = this.operationSumCredit;
        break;
    }

    return value;
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
    if (this.isAccess(Entities.TestBalance, TestBalancePermissions.ByBranch)) {
      this.isApplyBranchSeparation = true;
      //if (!this.selectedBranchSeparation) {
      //  this.gridColumnsRow = this.gridColumns.filter(f => f.name != "BranchName");
      //}
      //else {
      //  this.gridColumnsRow = this.gridColumns;
      //}
      this.changeParam();
      this.prepareColumns();
    }
    else {
      this.isApplyBranchSeparation = false;      
      this.showMessage(this.getText('App.AccessDenied'), MessageType.Warning);
    }

  }


  openSelectForm() {
    this.dialogRef = this.dialogService.open({
      content: SelectFormComponent
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.isDisableEntities = true;
    this.dialogModel.viewID = this.selectedViewId;

    if (this.displayTypeSelected == BalanceDisplayType.BySubsidiaryOfLeader)
      this.dialogModel.defaultCriteria = new Filter("level", "0", " == {0}", "System.String")

    if (this.displayTypeSelected == BalanceDisplayType.ByDetailOfSubsidiary)
      this.dialogModel.defaultCriteria = new Filter("level", "1", " == {0}", "System.String")

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

  onCellClick(e) {
    this.clickedRowItem = e.dataItem;
  }

  changeDisplayTypeByAccId() {
    var currentLevel = this.displayType.filter(dt => dt.id === this.displayTypeSelected)[0].level
    var upperLevelExist = this.displayType.filter((dt: TestBalanceModeInfo) => dt.level > currentLevel && dt.isDetail == true).length > 0;
    if (upperLevelExist) {
      if (this.clickedRowItem.accountId) {

        //if (this.displayTypeSelected == BalanceDisplayType.ByLedger) {
        //  this.displayTypeSelected = BalanceDisplayType.BySubsidiaryOfLeader;
        //}
        //else if (this.displayTypeSelected == BalanceDisplayType.BySubsidiary)
        //  this.displayTypeSelected = BalanceDisplayType.ByDetailOfSubsidiary;
        //else if (this.displayTypeSelected == BalanceDisplayType.BySubsidiaryOfLeader)
        //  this.displayTypeSelected = BalanceDisplayType.ByDetailOfSubsidiary;

        this.displayTypeSelected = this.displayType.filter((dt: TestBalanceModeInfo) => dt.level > currentLevel && dt.isDetail == true)[0].id;

        var account = new AccountInfo();
        account.fullCode = this.clickedRowItem.accountFullCode;
        account.name = this.clickedRowItem.accountName;
        account.id = this.clickedRowItem.accountId;
        this.selectedModel = account;
        this.disableAccountLookup = false;
      }
    }
    else {
      this.showMessage(this.getText('Balance.LevelIsNotExists'), MessageType.Warning);
    }
  }

  onDblClick(event) {
    if (this.clickedRowItem) {
      var currentLevel = this.displayType.filter(dt => dt.id === this.displayTypeSelected)[0].level
      var upperLevelExist = this.displayType.filter((dt: TestBalanceModeInfo) => dt.level > currentLevel && dt.isDetail == true).length > 0;
      if (upperLevelExist) {
        this.getReportData(this.clickedRowItem.accountId);
        this.changeDisplayTypeByAccId();
        this.updateBreadCrumb(this.clickedRowItem);

        this.selectedRows = new Array<any>();
      }
      else {
        this.showMessage(this.getText('Balance.LevelIsNotExists'), MessageType.Warning);
      }
    }
  }

  showSubAccountsBalance() {
    var currentLevel = this.displayType.filter(dt => dt.id === this.displayTypeSelected)[0].level
    var upperLevelExist = this.displayType.filter((dt: TestBalanceModeInfo) => dt.level > currentLevel && dt.isDetail == true).length > 0;
    if (upperLevelExist) {
      this.clickedRowItem = this.selectedRows[0];
      if (this.clickedRowItem) {
        this.getReportData(this.clickedRowItem.accountId);
        this.changeDisplayTypeByAccId();
        this.updateBreadCrumb(this.clickedRowItem);
      }
      else {
        this.showMessage(this.getText('Balance.PleaseSelectAccount'), MessageType.Warning);
      }
    }
    else {
      this.showMessage(this.getText('Balance.LevelIsNotExists'), MessageType.Warning);
    }
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
    this.breadCrumbList = new Array<any>();
    
    if (this.displayTypeSelected <= 2) {
      this.selectedModel = undefined;
      this.disableAccountLookup = true;
    } 
    else
    {
      this.disableAccountLookup = false;
      this.openSelectForm();
    }
        
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

  saveStates() {

    var state = {
      formatSelected: this.formatSelected,
      branchScopeSelected: this.branchScopeSelected,
      voucherStatusSelected: this.voucherStatusSelected,
      selectedBranchSeparation: this.selectedBranchSeparation,
      useClosingVoucher: this.useClosingVoucher,
      useClosingTempVoucher: this.useClosingTempVoucher,
      showZeroBalanceItems: this.showZeroBalanceItems
    };

    this.bStorageService.setSession(SessionKeys.TestBalance, state);
  }

  loadStates() {

    var state = this.bStorageService.getSession(SessionKeys.TestBalance);
    if (state) {
      this.formatSelected = state.formatSelected;
      this.branchScopeSelected = state.branchScopeSelected;
      this.voucherStatusSelected = state.voucherStatusSelected,
      this.selectedBranchSeparation = state.selectedBranchSeparation,
      this.useClosingVoucher = state.useClosingVoucher,
      this.useClosingTempVoucher = state.useClosingTempVoucher,
      this.showZeroBalanceItems = state.showZeroBalanceItems

      this.changeFormatType();
      this.changeBranchSeparation();
    }
  }


  showFilterComponent() {
    this.dialogRef = this.dialogService.open({
      content: AdvanceFilterComponent,
      title: this.getText('Balance.FilterByRef')
    });

    var filterDialogModel = <AdvanceFilterComponent> this.dialogRef.content.instance;

    var filterColumns = new Array<FilterColumn>();
    var fi = new FilterColumn();
    fi.dataType = "string";
    fi.name = "VoucherReference";
    fi.title = this.getText('Balance.Reference');    
    filterColumns.push(fi);

    if (this.advanceFilterList) {
      filterDialogModel.filters = this.advanceFilterList;
    }

    filterDialogModel.columnsList = filterColumns;
    filterDialogModel.selectedLogicalOperator = "or";
    filterDialogModel.lgoIsDisabled = true;
    filterDialogModel.selectedColumn = "VoucherReference";

    this.dialogRef.content.instance.cancel.subscribe((res) => {
      this.dialogRef.close();
    });

    this.dialogRef.content.instance.result.subscribe((res) => {
      this.advanceFilters = res.filters;
      this.advanceFilterList = res.filterList;
      this.dialogRef.close();
      this.getReportData();
    });
  }

  showAccountBook() {

    if (this.testBalanceType == '1') {
      this.clickedRowItem = this.selectedRows[0];
      if (this.clickedRowItem) {
        this.dialogRef = this.dialogService.open({
          content: AccountBookComponent,
          title: this.getText('Entity.AccountBook')
        });

        this.dialogModel = this.dialogRef.content.instance;
        var account = new AccountInfo();
        account.fullCode = this.clickedRowItem.accountFullCode;
        account.name = this.clickedRowItem.accountName;
        account.id = this.clickedRowItem.accountId;
        this.dialogModel.selectedModel = account;
        this.dialogModel.disableAccountLookup = true;
        this.dialogModel.fromDate = this.fromDate;
        this.dialogModel.toDate = this.toDate;
        this.dialogModel.selectedBookType = this.clickedRowItem.accountLevel;
        this.dialogModel.selectedViewId = 1;
        this.dialogModel.getReportData();
      }
      else {
        this.showMessage(this.getText('Balance.PleaseSelectAccount'), MessageType.Warning);
      }
    }
    else {
      this.showMessage(this.getText('Balance.ShowAccountBookHint'), MessageType.Warning);
    }
    
  }

  getReportData(accountId?:any,clearbreadCrumb:boolean = true) {

    this.changeParam(clearbreadCrumb);

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
      this.defaultFilter.push(new Filter("BranchId", this.BranchId.toString(), "== {0}", "System.String"));
    }

    if (!accountId) {      
        switch (this.formatSelected) {
          case BalanceFormatType.Balance2Column:
            this.entityName = Entities.TestBalance2Column;
            this.getDataUrl = TestBalanceApi.TwoColumnLevelBalance;
            break;
          case BalanceFormatType.Balance4Column:
            this.entityName = Entities.TestBalance4Column;
            this.getDataUrl = TestBalanceApi.FourColumnLevelBalance;
            break;
          case BalanceFormatType.Balance6Column:
            this.entityName = Entities.TestBalance6Column;
            this.getDataUrl = TestBalanceApi.SixColumnLevelBalance;
            break;
          case BalanceFormatType.Balance8Column:
            this.entityName = Entities.TestBalance8Column;
            this.getDataUrl = TestBalanceApi.EightColumnLevelBalance;
        }

        var displayTypeLevel = this.displayType.filter(f => f.id === this.displayTypeSelected)[0].level;
        this.getDataUrl = String.Format(this.getDataUrl,displayTypeLevel);
        
        
        if (this.testBalanceType == BalanceType.ByDate)
          this.getDataUrl += "?from=" + this.fromDate + "&to=" + this.toDate;

        if (this.testBalanceType == BalanceType.ByVoucher)
          this.getDataUrl += "?from=" + this.fromVoucher + "&to=" + this.toVoucher;

        this.getDataUrl += "&byBranch=" + this.selectedBranchSeparation;

        var options = (this.useClosingTempVoucher ? TestBalanceOptions.UseClosingTempVoucher : 0) |
          (this.useClosingVoucher ? TestBalanceOptions.UseClosingVoucher : 0) |
          (this.showZeroBalanceItems ? TestBalanceOptions.ShowZeroBalanceItems : 0) |
          (this.openingVoucherAsInitBalance ? TestBalanceOptions.OpeningVoucherAsInitBalance : 0);

        this.getDataUrl += "&options=" + options;

        
        this.reloadGrid();



      
    }
    else {

      switch (this.formatSelected) {
        case BalanceFormatType.Balance2Column:
          this.entityName = Entities.TestBalance2Column;
          this.getDataUrl = TestBalanceApi.TwoColumnChildItemsBalance;
          break;
        case BalanceFormatType.Balance4Column:
          this.entityName = Entities.TestBalance4Column;
          this.getDataUrl = TestBalanceApi.FourColumnChildItemsBalance;
          break;
        case BalanceFormatType.Balance6Column:
          this.entityName = Entities.TestBalance6Column;
          this.getDataUrl = TestBalanceApi.SixColumnChildItemsBalance;
          break;
        case BalanceFormatType.Balance8Column:
          this.entityName = Entities.TestBalance8Column;
          this.getDataUrl = TestBalanceApi.EightColumnChildItemsBalance;
          break;
      }

      this.getDataUrl = String.Format(this.getDataUrl, accountId);

      if (this.testBalanceType == BalanceType.ByDate)
        this.getDataUrl += "?from=" + this.fromDate + "&to=" + this.toDate;

      if (this.testBalanceType == BalanceType.ByVoucher)
        this.getDataUrl += "?from=" + this.fromVoucher + "&to=" + this.toVoucher;

      this.getDataUrl += "&byBranch=" + this.selectedBranchSeparation;      
      
      var options = (this.useClosingTempVoucher ? TestBalanceOptions.UseClosingTempVoucher : 0) |
        (this.useClosingVoucher ? TestBalanceOptions.UseClosingVoucher : 0) |
        (this.showZeroBalanceItems ? TestBalanceOptions.ShowZeroBalanceItems : 0) |
        (this.openingVoucherAsInitBalance ? TestBalanceOptions.OpeningVoucherAsInitBalance : 0);

      this.getDataUrl += "&options=" + options;

      this.reloadGrid();


    }
    
    
  }

  selectBreadCrumb(item) {
    this.displayTypeSelected = item.displayType.id;
    this.getReportData(item.account,false);    
    this.displayTypeName = item.displayType.name;

    if (item.accountId) {
      var account = new AccountInfo();
      account.fullCode = item.accountFullCode;
      account.name = item.accountName;
      account.id = item.accountId;
      this.selectedModel = account;
    }

    var i = 0;
    var index = -1;
    this.breadCrumbList.forEach((br: any) => {
      if (index == -1 && br.displayType.level > item.displayType.level) {
        index = i;
      }
      i++;
    });

    if (index > -1) {
      this.breadCrumbList.splice(index);
    }
  }

  updateBreadCrumb(acc?:any) {
    if (!this.breadCrumbList)
      this.breadCrumbList = new Array<any>();

    var relatedDisplayType = this.displayType.filter(d => d.id === this.displayTypeSelected)[0];
    
    var data = {
      displayType: relatedDisplayType,
      accountId: (acc) ? acc.accountId : undefined,
      accountName: (acc) ? acc.accountName : undefined,
      accountFullCode: (acc) ? acc.accountFullCode : undefined
    };

    if (this.breadCrumbList.findIndex(b => b.displayType.id === relatedDisplayType.id) == -1) {
      this.displayTypeName = relatedDisplayType.name;
      if (this.breadCrumbList.length > 0
        && this.breadCrumbList[this.breadCrumbList.length - 1].displayType.level > relatedDisplayType.level) {
        this.breadCrumbList = new Array<any>();
        this.breadCrumbList.push(data);
      }
      else {
        this.breadCrumbList.push(data);
      }
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

      //and filter to advanceFilters
      if(this.advanceFilters)
        currentFilter = this.andFilterToFilterExpression(currentFilter,
        this.advanceFilters);
      
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

        //this.viewId = this.tempViewId;

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
        this.endBalanceCredit = resData.total.endBalanceCredit;
        this.endBalanceDebit = resData.total.endBalanceDebit;

        this.startBalanceCredit = resData.total.startBalanceCredit;
        this.startBalanceDebit = resData.total.startBalanceDebit;

        this.turnoverCredit = resData.total.turnoverCredit;
        this.turnoverDebit = resData.total.turnoverDebit;

        this.operationSumCredit = resData.total.operationSumCredit;
        this.operationSumDebit = resData.total.operationSumDebit;

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


  

