
//#region Imports
import { Component, OnInit, OnDestroy, ChangeDetectorRef, Renderer2, NgZone, ViewChild } from '@angular/core';
import { AutoGeneratedGridComponent, Filter, FilterExpressionOperator, FilterExpression } from '@sppc/shared/class';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { MetaDataService, BrowserStorageService, ReportingService, GridService, SessionKeys } from '@sppc/shared/services';
import { SettingService } from '@sppc/config/service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DialogService, DialogRef } from '@progress/kendo-angular-dialog';
import { Entities, MessageType, Layout } from '@sppc/env/environment';
import { ViewName, ItemBalancePermissions, AccountPermissions, AccountBookPermissions } from '@sppc/shared/security';
import { QuickReportSettingComponent } from '@sppc/shared/components/reportManagement/QuickReport-Setting.component';
import { GridComponent, ColumnBase } from '@progress/kendo-angular-grid';
import { ViewIdentifierComponent, ReportViewerComponent, AdvanceFilterComponent } from '@sppc/shared/components';
import { ReportManagementComponent } from '@sppc/shared/components/reportManagement/reportManagement.component';
import { Item, FilterColumn } from '@sppc/shared/models';
import { BalanceDisplayTypeResource, VoucherStatusResource, BranchScopeResource, BalanceFormatType, BalanceType, BalanceDisplayType, BalanceFormatTypeResource,BalanceOptions } from '@sppc/finance/enum/balance';
import { SelectFormComponent } from '@sppc/shared/controls';
import { RTL } from '@progress/kendo-angular-l10n';
import { VoucherApi, AccountBookApi } from '@sppc/finance/service/api';
import { ItemBalanceApi } from '@sppc/finance/service/api/ItemBalanceApi';
import { String }  from '@sppc/shared/class';
import { AccountInfo, ItemBalanceService } from '@sppc/finance/service';
import { ItemBalanceModeInfo } from '@sppc/finance/models/ItemBalanceModeInfo';
import { AccountBookComponent } from '@sppc/finance/components/reporting/accountBook/accountBook.component';
import { SettingKey } from '@sppc/shared/enum';
import { LookupApi } from '@sppc/shared/services/api';
//#endregion

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'sppc-itemBalance',
  templateUrl: './itemBalance.component.html',
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
 .k-header k-grid-draggable-header { text-align: center !important; }
 .ref-filter {  padding-right: 0px !important; padding-left: 2px !important; padding-top:30px;}
 .ref-filter .ref { top: -5px;}
.left-pane { padding-left:0!important; padding-right:0!important;padding-top:15px!important;}
.right-pane { padding-left:0!important;}
`],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class ItemBalanceComponent extends AutoGeneratedGridComponent implements OnInit {
    
  //#region Fields
  displayType: Array<ItemBalanceModeInfo>;
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
  itemBalanceType: string = '1';

  

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
  selectedBookType: Item;
  filterByRef: string;

  activeOptions: boolean = false;
  useClosingVoucher: boolean;
  useClosingTempVoucher: boolean;
  openingVoucherAsInitBalance: boolean;
  showZeroBalanceItems: boolean;
  isDefaultBtn: boolean = true;
  isApplyBranchSeparation: boolean = false;
  disableAccountLookup: boolean = true;
  showFilterByRef: boolean = false;

  formatSelected: string = "6";
  displayTypeSelected: number = 0;
  branchScopeSelected: string = '1';
  voucherStatusSelected: string = '2';
  articleTypeSelected: string = '1';
  selectedBranchSeparation: boolean = false;
  gridColumnsRow: any[] = [];
  gridGroupColumnsRow: any[] = [];
  gridGroupColumnNames: any[] = [];

  ReportType: Array<any> = [];
  ddlEntites: Array<Item> = [];
  selectedEntityId: Item;

  addOpeningVoucherToInitBalance: boolean;

  clickedRowItem: any;
  //#endregion

  //#region Constructor
  constructor(public toastrService: ToastrService, public translate: TranslateService, public dialogService: DialogService, public gridService: GridService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, public bStorageService: BrowserStorageService,
    public settingService: SettingService, public reporingService: ReportingService, public ngZone: NgZone, public formBuilder: FormBuilder
    , public itemBalanceService: ItemBalanceService) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, bStorageService, cdref, ngZone);
  }
  //#endregion
    
  getFirstAndLastVoucherNo() {
    this.gridService.getModels(VoucherApi.EnvironmentItemRange).subscribe(res => {
      this.fromVoucher = res.firstNo.toString();
      this.toVoucher = res.lastNo.toString();
    })
  }

  nextModel() {
    if (this.selectedModel)
      this.modelUrl = String.Format(AccountBookApi.NextEnvironmentItem, this.selectedViewId, this.selectedModel.id);

    this.getModel();
  }

  previousModel() {
    if (this.selectedModel)
      this.modelUrl = String.Format(AccountBookApi.PreviousEnvironmentItem, this.selectedViewId, this.selectedModel.id);

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
   
  changeParam(clearBreadCrumb: boolean = true) {
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

  loadEntity() {
    this.settingService.getAll(LookupApi.TreeViews).subscribe(res => {      
      var result = <Array<any>>res.body;
      result.splice(0, 1); 
      this.ddlEntites = result;
      this.selectedEntityId = result[0];
      this.fillDisplayTypes();
    })    
  }

  changeEntityTitle() {
    switch (this.selectedViewId) {      
      case 7:
        this.selectedModelTitle = this.getText("ItemBalance.CostCenter")
        break;
      case 8:
        this.selectedModelTitle = this.getText("ItemBalance.Project")
        break;
      case 6:      
        this.selectedModelTitle = this.getText("ItemBalance.DetailAccount")
        break;
    } 
  }

  onchangeEntity() {
    this.selectedViewId = parseInt(this.selectedEntityId.key);
    this.fillDisplayTypes();
    this.selectedModel = undefined;
    this.changeFormatType();
    this.breadCrumbList = new Array<any>();  

    this.changeEntityTitle();
  }

  fillDisplayTypes() {
    var url = String.Format(ItemBalanceApi.ItemBalanceTypeLookup, this.selectedEntityId.key);
    this.itemBalanceService.getAll(url).subscribe(res => {
      var modes = <Array<ItemBalanceModeInfo>>res.body;
      this.displayType = modes;
      this.displayTypeSelected = modes[0].id;
    })
  }

  prepareColumns() {
    if (!this.selectedBranchSeparation) {
      this.gridColumnsRow = this.gridColumns.filter(f => f.name != "BranchName" && f.groupName == "");
      this.gridGroupColumnsRow = this.gridColumns.filter(f => f.groupName);

      this.gridGroupColumnNames = this.gridGroupColumnsRow.filter((item, pos) => {
        return this.gridGroupColumnsRow.findIndex(i => i.groupName === item.groupName) == pos;
      });
    }
    else if (this.isApplyBranchSeparation) {
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
   
  valueChangeBranchSeparation(value) {
    if (this.isAccess(Entities.ItemBalance, ItemBalancePermissions.ByBranch)) {
      this.isApplyBranchSeparation = true;
      this.changeParam();
      this.prepareColumns();
    }
    else {
      this.isApplyBranchSeparation = false;
      this.showMessage(this.getText('App.AccessDenied'), MessageType.Warning);
      setTimeout(() => { this.selectedBranchSeparation = false; }, 1000);

    }

    this.saveStates();
  }
  
  openSelectForm() {

    this.dialogRef = this.dialogService.open({
      content: SelectFormComponent
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.isDisableEntities = true;    
    this.dialogModel.viewID = parseInt(this.selectedEntityId.key);

    var dt = this.displayType.filter(f => f.id === this.displayTypeSelected);
    if (dt.length > 0) {
      this.dialogModel.defaultCriteria = new Filter("level", (dt[0].level - 2).toString(), " == {0}", "System.String");
    }

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
   
  changeDisplayTypeByAccId() {
    var currentLevel = this.displayType.filter(dt => dt.id === this.displayTypeSelected)[0].level
    var upperLevelExist = this.displayType.filter((dt: ItemBalanceModeInfo) => dt.level > currentLevel && dt.isDetail == true).length > 0;
    if (upperLevelExist) {
      if (this.clickedRowItem.accountId) {

        this.displayTypeSelected = this.displayType.filter((dt: ItemBalanceModeInfo) => dt.level > currentLevel && dt.isDetail == true)[0].id;

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
   
  showSubAccountsBalance() {
    var currentLevel = this.displayType.filter(dt => dt.id === this.displayTypeSelected)[0].level
    var upperLevelExist = this.displayType.filter((dt: ItemBalanceModeInfo) => dt.level > currentLevel && dt.isDetail == true).length > 0;
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
  }

  showSelectForm() {
    var currentDisplay = this.displayType.filter(f => f.id === this.displayTypeSelected)[0];
    if (!currentDisplay.isDetail) {
      this.selectedModel = undefined;
      this.disableAccountLookup = true;
    }
    else {
      this.disableAccountLookup = false;
      this.openSelectForm();

    }
  }


  setApiUrl(accountId) {

    if (!accountId) {
      switch (this.selectedEntityId.key) {
        case "6":
          {
            switch (this.formatSelected) {
              case BalanceFormatType.Balance2Column:
                this.entityName = Entities.DetailAccountBalance2Column;
                this.getDataUrl = ItemBalanceApi.TwoColumnLevelBalance;
                break;
              case BalanceFormatType.Balance4Column:
                this.entityName = Entities.DetailAccountBalance4Column;
                this.getDataUrl = ItemBalanceApi.FourColumnLevelBalance;
                break;
              case BalanceFormatType.Balance6Column:
                this.entityName = Entities.DetailAccountBalance6Column;
                this.getDataUrl = ItemBalanceApi.SixColumnLevelBalance;
                break;
              case BalanceFormatType.Balance8Column:
                this.entityName = Entities.DetailAccountBalance8Column;
                this.getDataUrl = ItemBalanceApi.EightColumnLevelBalance;
                break;
            }
          }
          break;
        case "7":
          {
            switch (this.formatSelected) {
              case BalanceFormatType.Balance2Column:
                this.entityName = Entities.CostCenterBalance2Column;
                this.getDataUrl = ItemBalanceApi.TwoColumnLevelBalance;
                break;
              case BalanceFormatType.Balance4Column:
                this.entityName = Entities.CostCenterBalance4Column;
                this.getDataUrl = ItemBalanceApi.FourColumnLevelBalance;
                break;
              case BalanceFormatType.Balance6Column:
                this.entityName = Entities.CostCenterBalance6Column;
                this.getDataUrl = ItemBalanceApi.SixColumnLevelBalance;
                break;
              case BalanceFormatType.Balance8Column:
                this.entityName = Entities.CostCenterBalance8Column;
                this.getDataUrl = ItemBalanceApi.EightColumnLevelBalance;
                break;
            }
          }
          break;
        case "8":
          {
            switch (this.formatSelected) {
              case BalanceFormatType.Balance2Column:
                this.entityName = Entities.ProjectBalance2Column;
                this.getDataUrl = ItemBalanceApi.TwoColumnLevelBalance;
                break;
              case BalanceFormatType.Balance4Column:
                this.entityName = Entities.ProjectBalance4Column;
                this.getDataUrl = ItemBalanceApi.FourColumnLevelBalance;
                break;
              case BalanceFormatType.Balance6Column:
                this.entityName = Entities.ProjectBalance6Column;
                this.getDataUrl = ItemBalanceApi.SixColumnLevelBalance;
                break;
              case BalanceFormatType.Balance8Column:
                this.entityName = Entities.ProjectBalance8Column;
                this.getDataUrl = ItemBalanceApi.EightColumnLevelBalance;
                break;
            }
          }
          break;
      }

      this.viewId = ViewName[this.entityTypeName];
      var bookItem = this.displayType.filter(bt => bt.id === this.displayTypeSelected)[0];
      this.getDataUrl = String.Format(this.getDataUrl, this.selectedEntityId.key, bookItem.level);
    }
    else {
      switch (this.selectedEntityId.key) {
        case "6":
          {
            switch (this.formatSelected) {
              case BalanceFormatType.Balance2Column:
                this.entityName = Entities.DetailAccountBalance2Column;
                this.getDataUrl = ItemBalanceApi.TwoColumnChildItemsBalance;
                break;
              case BalanceFormatType.Balance4Column:
                this.entityName = Entities.DetailAccountBalance4Column;
                this.getDataUrl = ItemBalanceApi.FourColumnChildItemsBalance;
                break;
              case BalanceFormatType.Balance6Column:
                this.entityName = Entities.DetailAccountBalance6Column;
                this.getDataUrl = ItemBalanceApi.SixColumnChildItemsBalance;
                break;
              case BalanceFormatType.Balance8Column:
                this.entityName = Entities.DetailAccountBalance8Column;
                this.getDataUrl = ItemBalanceApi.EightColumnChildItemsBalance;
                break;
            }
          }
          break;
        case "7":
          {
            switch (this.formatSelected) {
              case BalanceFormatType.Balance2Column:
                this.entityName = Entities.CostCenterBalance2Column;
                this.getDataUrl = ItemBalanceApi.TwoColumnChildItemsBalance;
                break;
              case BalanceFormatType.Balance4Column:
                this.entityName = Entities.CostCenterBalance4Column;
                this.getDataUrl = ItemBalanceApi.FourColumnChildItemsBalance;
                break;
              case BalanceFormatType.Balance6Column:
                this.entityName = Entities.CostCenterBalance6Column;
                this.getDataUrl = ItemBalanceApi.SixColumnChildItemsBalance;
                break;
              case BalanceFormatType.Balance8Column:
                this.entityName = Entities.CostCenterBalance8Column;
                this.getDataUrl = ItemBalanceApi.EightColumnChildItemsBalance;
                break;
            }
          }
          break;
        case "8":
          {
            switch (this.formatSelected) {
              case BalanceFormatType.Balance2Column:
                this.entityName = Entities.ProjectBalance2Column;
                this.getDataUrl = ItemBalanceApi.TwoColumnChildItemsBalance;
                break;
              case BalanceFormatType.Balance4Column:
                this.entityName = Entities.ProjectBalance4Column;
                this.getDataUrl = ItemBalanceApi.FourColumnChildItemsBalance;
                break;
              case BalanceFormatType.Balance6Column:
                this.entityName = Entities.ProjectBalance6Column;
                this.getDataUrl = ItemBalanceApi.SixColumnChildItemsBalance;
                break;
              case BalanceFormatType.Balance8Column:
                this.entityName = Entities.ProjectBalance8Column;
                this.getDataUrl = ItemBalanceApi.EightColumnChildItemsBalance;
                break;
            }
          }
          break;
      }

      this.viewId = ViewName[this.entityTypeName];   
      this.getDataUrl = String.Format(this.getDataUrl, this.selectedEntityId.key, accountId);
    }
    
  }

  changeFormatType() {

    if (this.selectedEntityId) {
      switch (this.selectedEntityId.key) {
        case "6":
          {
            switch (this.formatSelected) {
              case BalanceFormatType.Balance2Column:
                this.entityName = Entities.DetailAccountBalance2Column;
                break;
              case BalanceFormatType.Balance4Column:
                this.entityName = Entities.DetailAccountBalance4Column;
                break;
              case BalanceFormatType.Balance6Column:
                this.entityName = Entities.DetailAccountBalance6Column;
                break;
              case BalanceFormatType.Balance8Column:
                this.entityName = Entities.DetailAccountBalance8Column;
                break;
            }
          }
          break;
        case "7":
          {
            switch (this.formatSelected) {
              case BalanceFormatType.Balance2Column:
                this.entityName = Entities.CostCenterBalance2Column;
                break;
              case BalanceFormatType.Balance4Column:
                this.entityName = Entities.CostCenterBalance4Column;
                break;
              case BalanceFormatType.Balance6Column:
                this.entityName = Entities.CostCenterBalance6Column;
                break;
              case BalanceFormatType.Balance8Column:
                this.entityName = Entities.CostCenterBalance8Column;
                break;
            }
          }
          break;
        case "8":
          {
            switch (this.formatSelected) {
              case BalanceFormatType.Balance2Column:
                this.entityName = Entities.ProjectBalance2Column;
                break;
              case BalanceFormatType.Balance4Column:
                this.entityName = Entities.ProjectBalance4Column;
                break;
              case BalanceFormatType.Balance6Column:
                this.entityName = Entities.ProjectBalance6Column;
                break;
              case BalanceFormatType.Balance8Column:
                this.entityName = Entities.ProjectBalance8Column;
                break;
            }
          }
          break;
      }
      this.viewId = ViewName[this.entityName];
      this.changeParam();
    }
  }

  showFilterByRefChange() {
    if (this.isAccess(Entities.ItemBalance, ItemBalancePermissions.FilterByRef)) {
      if (!this.showFilterByRef) {
        this.filterByRef = "";
      }
    }
    else {
      setTimeout(() => {
        this.showFilterByRef = false;
        this.filterByRef = "";
      });      
      this.showMessage(this.getText('App.AccessDenied'), MessageType.Warning);
    }    
  }

  showFilterComponent() {

    if (!this.isAccess(Entities.ItemBalance, ItemBalancePermissions.Filter)) {
      this.showMessage(this.getText('App.AccessDenied'), MessageType.Warning);
      return;
    }

    this.dialogRef = this.dialogService.open({
      content: AdvanceFilterComponent,
      title: this.getText('AdvanceFilter.Title')
    });

    var filterDialogModel = <AdvanceFilterComponent>this.dialogRef.content.instance;
    if (this.advanceFilterList) {
      filterDialogModel.filters = this.advanceFilterList;    
      filterDialogModel.gFilterSelected = this.selectedGroupFilter;
    }
    filterDialogModel.viewId = ViewName[this.entityTypeName];    
    this.dialogRef.content.instance.cancel.subscribe((res) => {
      this.dialogRef.close();
    });

    this.dialogRef.content.instance.result.subscribe((res) => {
      this.advanceFilters = res.filters;
      this.advanceFilterList = res.filterList;     
      this.selectedGroupFilter = res.gFilterSelected;
      this.dialogRef.close();
      this.getReportData();
    });
  }

  showAccountBook() {

    if (!this.isAccess(Entities.AccountBook, AccountBookPermissions.View)) {
      this.showMessage(this.getText('App.AccessDenied'), MessageType.Warning);
      return;
    }

    if (this.itemBalanceType == '1') {
      this.clickedRowItem = this.selectedRows[0];
      if (this.clickedRowItem) {
        this.dialogRef = this.dialogService.open({
          content: AccountBookComponent,
          title: this.getText('Entity.AccountBook'),
          height: screen.availHeight
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
        this.dialogModel.selectedBookType = this.bookType.filter(f=>f.key === this.clickedRowItem.accountLevel)[0];
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

  getReportData(accountId?: any, clearbreadCrumb: boolean = true) {


    var displayTypeLevel = this.displayType.filter(f => f.id === this.displayTypeSelected)[0].level;
    var isDetail = this.displayType.filter(f => f.id === this.displayTypeSelected)[0].isDetail;

    if (isDetail && !this.selectedModel) {
      this.showMessage(this.getText('Balance.PlaeseSelectAnAccount'), MessageType.Warning);
      return;
    }

    this.changeParam(clearbreadCrumb);

    this.defaultFilter = [];
    this.quickFilter = [];

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

    if (this.showFilterByRef) {
      var filterByRefValue = '';
      if (this.filterByRef) {
        filterByRefValue = this.filterByRef;
        this.quickFilter.push(new Filter("VoucherReference", filterByRefValue, " == {0}", "System.String"));
      }
      else {
        this.quickFilter.push(new Filter("VoucherReference", filterByRefValue, " == null", "System.String"));
      }
    }

    if (this.branchScopeSelected == "1") {
      this.quickFilter.push(new Filter("BranchId", this.BranchId.toString(), " == {0}", "System.Int32"));
    }

    if (!accountId) {
      this.setApiUrl(undefined);
           
      

      if (this.itemBalanceType == BalanceType.ByDate)
        this.getDataUrl += "?from=" + this.fromDate + "&to=" + this.toDate;

      if (this.itemBalanceType == BalanceType.ByVoucher)
        this.getDataUrl += "?from=" + this.fromVoucher + "&to=" + this.toVoucher;

      this.getDataUrl += "&byBranch=" + this.selectedBranchSeparation;

      var options = (this.useClosingTempVoucher ? BalanceOptions.UseClosingTempVoucher : 0) |
        (this.useClosingVoucher ? BalanceOptions.UseClosingVoucher : 0) |
        (this.showZeroBalanceItems ? BalanceOptions.ShowZeroBalanceItems : 0) |
        (this.openingVoucherAsInitBalance ? BalanceOptions.OpeningVoucherAsInitBalance : 0);

      this.getDataUrl += "&options=" + options;

      this.reloadGrid();
    }
    else {

      this.setApiUrl(accountId);

      

      if (this.itemBalanceType == BalanceType.ByDate)
        this.getDataUrl += "?from=" + this.fromDate + "&to=" + this.toDate;

      if (this.itemBalanceType == BalanceType.ByVoucher)
        this.getDataUrl += "?from=" + this.fromVoucher + "&to=" + this.toVoucher;

      this.getDataUrl += "&byBranch=" + this.selectedBranchSeparation;

      var options = (this.useClosingTempVoucher ? BalanceOptions.UseClosingTempVoucher : 0) |
        (this.useClosingVoucher ? BalanceOptions.UseClosingVoucher : 0) |
        (this.showZeroBalanceItems ? BalanceOptions.ShowZeroBalanceItems : 0) |
        (this.openingVoucherAsInitBalance ? BalanceOptions.OpeningVoucherAsInitBalance : 0);

      this.getDataUrl += "&options=" + options;

      this.reloadGrid();
    }
  } 

  selectBreadCrumb(item) {
    this.displayTypeSelected = item.displayType.id;
    this.getReportData(item.accountId, false);
    this.displayTypeName = item.displayType.name;

    if (item.accountId) {
      var account = new AccountInfo();
      account.fullCode = item.accountFullCode;
      account.name = item.accountName;
      account.id = item.accountId;
      this.selectedModel = account;
      this.disableAccountLookup = false;
    }
    else {
      this.selectedModel = undefined;
      this.disableAccountLookup = true;
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

  updateBreadCrumb(acc?: any) {
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

  onDataBind(resData: any) {
    this.isDefaultBtn = true;
    this.endBalanceCredit = resData.total.endBalanceCredit;
    this.endBalanceDebit = resData.total.endBalanceDebit;

    this.startBalanceCredit = resData.total.startBalanceCredit;
    this.startBalanceDebit = resData.total.startBalanceDebit;

    this.turnoverCredit = resData.total.turnoverCredit;
    this.turnoverDebit = resData.total.turnoverDebit;

    this.operationSumCredit = resData.total.operationSumCredit;
    this.operationSumDebit = resData.total.operationSumDebit;
    this.cdref.detectChanges();
    
  }
  //#endregion

  //#region Events

  onCellClick(e) {
    this.clickedRowItem = e.dataItem;
  }

  onDblClick(event) {
    if (this.clickedRowItem) {
      var currentLevel = this.displayType.filter(dt => dt.id === this.displayTypeSelected)[0].level
      var upperLevelExist = this.displayType.filter((dt: ItemBalanceModeInfo) => dt.level > currentLevel && dt.isDetail == true).length > 0;
      if (upperLevelExist) {
        var accountId = 0;
        var accFullCode = 0;
        var accName = '';

        if (this.selectedEntityId.key == '6') {
          accountId = this.clickedRowItem.detailAccountId;
          accName = this.clickedRowItem.detailAccountName;
          accFullCode = this.clickedRowItem.detailAccountFullCode;
        }

        if (this.selectedEntityId.key == '7') {
          accountId = this.clickedRowItem.costCenterId;
          accName = this.clickedRowItem.costCenterName;
          accFullCode = this.clickedRowItem.costCenterFullCode;
        }

        if (this.selectedEntityId.key == '8') {
          accountId = this.clickedRowItem.projectId;
          accName = this.clickedRowItem.projectName;
          accFullCode = this.clickedRowItem.projectFullCode;
        }

        //change accountid for breadcumb code & lookup update
        this.clickedRowItem.accountId = accountId;
        this.clickedRowItem.accountFullCode = accFullCode;
        this.clickedRowItem.accountName = accName;        
        //change accountid for breadcumb code & lookup update

        this.getReportData(accountId);
        this.changeDisplayTypeByAccId();     
        this.updateBreadCrumb(this.clickedRowItem);

        this.selectedRows = new Array<any>();
      }
      else {
        this.showMessage(this.getText('Balance.LevelIsNotExists'), MessageType.Warning);
      }
    }
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

  dateValueChange(event: any) {
    this.fromDate = event.fromDate;
    this.toDate = event.toDate;
    this.isDefaultBtn = false;
  }

  onAdvanceFilterOk(): any {
    this.getReportData();
  }

  ngOnInit() {
    this.entityName = Entities.DetailAccountBalance6Column;
    this.viewId = ViewName[this.entityTypeName];    

    this.translate.get('ItemBalance.DetailAccount').subscribe(res => {
      this.baseModelTitle = res;
      this.selectedModelTitle = this.baseModelTitle;
    })

    this.loadEntity();  
    
    this.getFirstAndLastVoucherNo();
    this.showloadingMessage = false;

    this.settingService.getSettingById(SettingKey.TestBalanceConfig).subscribe((res) => {
      if (res) {
        this.openingVoucherAsInitBalance = res.values.addOpeningVoucherToInitBalance;
      }
    });

    if (this.isAccess(Entities.ItemBalance, ItemBalancePermissions.ByBranch)) {
      this.isApplyBranchSeparation = true;
      this.prepareColumns();
    }

    this.loadStates();
    this.changeEntityTitle();
  }
  //#endregion

  //#region Reporting
  public showReport() {

    if (!this.isAccess(Entities.ItemBalance, ItemBalancePermissions.Print)) {
      this.showMessage(this.getText('App.AccessDenied'), MessageType.Warning);
      return;
    }   
   
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

    if (!this.isAccess(Entities.ItemBalance, ItemBalancePermissions.Print)) {
      this.showMessage(this.getText('App.AccessDenied'), MessageType.Warning);
      return;
    }
    
    if (this.validateReport()) {
      this.reportSetting.showReportSetting(this.gridColumns, this.entityTypeName, this.viewId, this.reportManager);
    }    
  }
  //#endregion Reporting

  saveStates() {

    var state = {
      formatSelected: this.formatSelected,
      branchScopeSelected: this.branchScopeSelected,
      voucherStatusSelected: this.voucherStatusSelected,
      selectedBranchSeparation: this.selectedBranchSeparation,
      useClosingVoucher: this.useClosingVoucher,
      useClosingTempVoucher: this.useClosingTempVoucher,
      showZeroBalanceItems: this.showZeroBalanceItems,
      selectedEntityId: this.selectedEntityId
    };

    this.bStorageService.setSession(SessionKeys.ItemBalance, state);
  }

  loadStates() {

    var state = this.bStorageService.getSession(SessionKeys.ItemBalance);
    if (state) {
      this.formatSelected = state.formatSelected;
      this.branchScopeSelected = state.branchScopeSelected;
      this.voucherStatusSelected = state.voucherStatusSelected;

      if (!this.isAccess(Entities.ItemBalance, ItemBalancePermissions.ByBranch)) {
        this.selectedBranchSeparation = false;
        this.isApplyBranchSeparation = false;
      }
      else
        this.selectedBranchSeparation = state.selectedBranchSeparation;

      this.useClosingVoucher = state.useClosingVoucher;
      this.useClosingTempVoucher = state.useClosingTempVoucher;
      this.showZeroBalanceItems = state.showZeroBalanceItems;
      this.selectedEntityId = state.selectedEntityId;

      this.changeFormatType();
    }
  }

  
}


  

