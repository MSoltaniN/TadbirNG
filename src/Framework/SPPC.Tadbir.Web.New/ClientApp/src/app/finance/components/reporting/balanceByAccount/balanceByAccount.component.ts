import { Component, OnInit, OnDestroy, ChangeDetectorRef, Renderer2, NgZone, ViewChild } from '@angular/core';
import { String, AutoGeneratedGridComponent, Filter, FilterExpressionOperator, FilterExpression, GridOrderBy } from '@sppc/shared/class';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { MetaDataService, BrowserStorageService, ReportingService, GridService, SessionKeys, LookupService } from '@sppc/shared/services';
import { SettingService } from '@sppc/config/service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DialogService, DialogRef } from '@progress/kendo-angular-dialog';
import { Entities, MessageType, Layout } from '@sppc/env/environment';
import { RTL } from '@progress/kendo-angular-l10n';
import { ViewName, BalanceByAccountPermissions } from '@sppc/shared/security';
import { VoucherApi, BalanceByAccountApi } from '@sppc/finance/service/api';
import { VoucherStatusResource, BranchScopeResource } from '@sppc/finance/enum';
import { Item } from '@sppc/shared/models';
import { LookupApi } from '@sppc/shared/services/api';
import { SelectFormComponent, SppcDateRangeSelector } from '@sppc/shared/controls';
import { DetailAccount, CostCenter, Project, Account } from '@sppc/finance/models';
import { SettingKey, ReloadStatusType } from '@sppc/shared/enum';
import { BalanceOptions } from '@sppc/finance/enum/balance';
import { VoucherService, AccountInfo, DetailAccountInfo, CostCenterInfo, ProjectInfo, AccountService } from '@sppc/finance/service';
import { AccountBookComponent } from '../accountBook/accountBook.component';
import { ReportManagementComponent } from '@sppc/shared/components/reportManagement/reportManagement.component';
import { QuickReportSettingComponent } from '@sppc/shared/components/reportManagement/QuickReport-Setting.component';
import { GridComponent, RowArgs } from '@progress/kendo-angular-grid';
import { ViewIdentifierComponent, ReportViewerComponent } from '@sppc/shared/components';
import { ReloadOption } from '@sppc/shared/class/reload-option';
import { ShareDataService } from '@sppc/shared/services/share-data.service';
import * as moment from 'jalali-moment';


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'sppc-balance-account',
  templateUrl: './balanceByAccount.component.html',
  styleUrls: ['./balanceByAccount.component.css'],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class BalanceByAccountComponent extends AutoGeneratedGridComponent implements OnInit, OnDestroy  {

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

  gridColumnsRow: any[] = [];
  fromDate: Date;
  toDate: Date;
  initializeDate: boolean = true;

  fromVoucher: string;
  toVoucher: string;
  isDefaultBtn: boolean = true;
  reportType: string = '1';
  reportBy: Array<Item> = [];

  selectedReportBy: string = ViewName.Account.toString();

  selectedVoucherStatus: string = '2';
  selectedBranchScope: string = '1';
  isApplyBranchSeparation: boolean = false;
  selectedBranchSeparation: boolean = false;

  showZeroBalanceItems: boolean;
  useClosingVoucher: boolean;
  useClosingTempVoucher: boolean;
  openingAsFirstVoucher: boolean;
  startTurnoverAsInitBalance: boolean;

  referenceValues: string[];
  selectedReferences: string[];


  chbAccount: boolean = true;
  chbDetailAccount: boolean = false;
  chbCCenter: boolean = false;
  chbProject: boolean = false;
  accountLevelList: Array<any> = [];
  detailAccountLevelList: Array<any> = [];
  cCenterLevelList: Array<any> = [];
  projectLevelList: Array<any> = [];
  selectedAccount: Account;
  selectedDetailAccount: DetailAccount;
  selectedCCenter: CostCenter;
  selectedProject: Project;
  selectedAccountName: string;
  selectedDetailAccountName: string;
  selectedCCenterName: string;
  selectedProjectName: string;
  selectedAccountLevel: number;
  selectedDetailAccountLevel: number;
  selectedCCenterLevel: number;
  selectedProjectLevel: number;
  selectedAccountInit: boolean;

  dialogRef: DialogRef;
  dialogModel: any;

  parameters: any;

  totalRow: any;

  //allAccountTitle: string;
  //allDetailAccountTitle: string;
  //all

  private docClickSubscription: any;

  @ViewChild(GridComponent) grid: GridComponent;
  @ViewChild(ViewIdentifierComponent) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportViewerComponent) viewer: ReportViewerComponent;
  @ViewChild(ReportManagementComponent) reportManager: ReportManagementComponent;
  @ViewChild(QuickReportSettingComponent) reportSetting: QuickReportSettingComponent;
  @ViewChild(SppcDateRangeSelector) dateRange: SppcDateRangeSelector;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public dialogService: DialogService, public gridService: GridService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, public bStorageService: BrowserStorageService, public voucherService: VoucherService,
    public settingService: SettingService, public reportingService: ReportingService, public ngZone: NgZone, public formBuilder: FormBuilder, public lookupService: LookupService,
    public accountService: AccountService, private shareDataService: ShareDataService) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, bStorageService, cdref, ngZone);

    if (this.shareDataService.sharingData)
      this.initializeDate = false;
  }

  ngOnInit() {
    this.entityName = Entities.BalanceByAccount;
    this.viewId = ViewName[this.entityTypeName];

    var balanceAccountParam = this.shareDataService.sharingData;  
    var isTestBalanceParams = balanceAccountParam &&
      (balanceAccountParam.viewId == ViewName[Entities.TestBalance6Column] ||
        balanceAccountParam.viewId == ViewName[Entities.TestBalance2Column] ||
        balanceAccountParam.viewId == ViewName[Entities.TestBalance4Column] ||
        balanceAccountParam.viewId == ViewName[Entities.TestBalance8Column]);
    

    var promise = new Promise((resolve) => {
      this.showloadingMessage = false;
      this.useReloadParameter = true;
      this.getFirstAndLastVoucherNo();
      this.getReportByItems();
      this.getAccountItemLevels(true, ViewName.Account);

      if (isTestBalanceParams) {
        this.getAccountItemLevels(true, ViewName.DetailAccount);
        this.getAccountItemLevels(true, ViewName.CostCenter);
        this.getAccountItemLevels(true, ViewName.Project);
      }

    this.settingService.getSettingById(SettingKey.FinanceReportConfig).subscribe((res) => {
      if (res) {
        this.openingAsFirstVoucher = res.values.openingAsFirstVoucher;
        this.startTurnoverAsInitBalance = res.values.startTurnoverAsInitBalance;
      }
    });

      this.fillReferences();
      resolve();
    });       

    promise.then(() => {     

      this.fromDate = this.FiscalPeriodStartDate;
      this.toDate = this.FiscalPeriodEndDate;     
        
      if (isTestBalanceParams) {

        this.fillInputParameters(balanceAccountParam);

        //this.chbCCenter = true;
        //this.chbDetailAccount = true;
        //this.chbProject = true;        
        //this.getAccountItemLevels(true, 8);
        //this.getAccountItemLevels(true, 7);
        //this.getAccountItemLevels(true, 6);
        //this.changeParam();

        this.accountService.getAccountById(balanceAccountParam.account.accountId).subscribe((account) => {
          
          this.selectedAccount = account;
          setTimeout(() => {
            this.getReportData();
          }
            , 200)
        });

        this.shareDataService.sharingData = null;
      }      
     
    })

    this.cdref.detectChanges();
    
    

    //this.docClickSubscription = this.renderer.listen('document', 'click', this.onDocumentClick.bind(this));    
    
  }

  fillInputParameters(balanceAccountParam) {
    if (this.dateRange) {
      if (balanceAccountParam.filterType == "1") {
        this.dateRange.InitializeDate = false;
        this.dateRange.fromDatePicker.isDisplayDate = false;
        this.dateRange.toDatePicker.isDisplayDate = false;

        var from = new Date(balanceAccountParam.fromDate);
        var to = new Date(balanceAccountParam.toDate);

        this.dateRange.fromDatePicker.dateObject = moment(from);
        this.dateRange.toDatePicker.dateObject = moment(to);
        this.fromDate = from;
        this.toDate = to;
        this.dateRange.setInitialDates(from, to);
      }
      else {
        this.fromVoucher = balanceAccountParam.fromVoucher;
        this.fromVoucher = balanceAccountParam.toVoucher;
      }

    }

    this.chbAccount = true;
    this.selectedAccountInit = true;
    this.selectedAccountLevel = balanceAccountParam.level - 1;
    this.selectedAccountName = balanceAccountParam.account.accountName;
    this.selectedBranchSeparation = balanceAccountParam.selectedBranchSeparation;
    this.selectedVoucherStatus = balanceAccountParam.voucherStatusSelected;
    this.selectedBranchScope = balanceAccountParam.branchScopeSelected;
    this.useClosingVoucher = balanceAccountParam.useClosingVoucher;
    this.selectedReferences = balanceAccountParam.selectedReferences;
    this.useClosingTempVoucher = balanceAccountParam.useClosingTempVoucher;
    this.reportType = balanceAccountParam.filterType;
    
  }

  ngAfterViewInit()
  {
    if (this.dateRange) {
      this.dateRange.InitializeDate = false;
    }
  }


  ngOnDestroy() {
    this.bStorageService.removeSessionStorage("BalanceAccountParam");
  }

  getReportByItems() {
    this.settingService.getAll(LookupApi.TreeViews).subscribe(res => {
      this.reportBy = res.body;
    })
  }

  onChangeReportBy() {
    var viewId = parseInt(this.selectedReportBy);

    this.getAccountItemLevels(true, viewId);

    this.sortingGridColumns();
  }

  fillReferences() {
    this.lookupService.getAll(LookupApi.VoucherReferences).subscribe(res => {
      var refs = <string[]>res.body;
      this.referenceValues = refs;
    })
  }

  sortingGridColumns() {   
    this.showColumn(parseInt(this.selectedReportBy));

    let columnList: Array<any> = Object.assign(this.gridColumns);

    columnList.forEach(item => {
      var column = this.gridColumnsRow.find(f => f.name == item.name);
      if (column) {
        var columnIndex = columnList.findIndex(f => f.name == item.name);
        if (columnIndex > -1)
          column.displayIndex = columnIndex;
      }
    })

    var fullCodeColumn;
    var nameColumn;

    switch (parseInt(this.selectedReportBy)) {
      case ViewName.Account: {
        fullCodeColumn = this.gridColumnsRow.find(f => f.name.toLowerCase() == "accountfullcode")
        nameColumn = this.gridColumnsRow.find(f => f.name.toLowerCase() == "accountname");
        break;
      }
      case ViewName.DetailAccount: {
        fullCodeColumn = this.gridColumnsRow.find(f => f.name.toLowerCase() == "detailaccountfullcode")
        nameColumn = this.gridColumnsRow.find(f => f.name.toLowerCase() == "detailaccountname");
        break;
      }
      case ViewName.CostCenter: {
        fullCodeColumn = this.gridColumnsRow.find(f => f.name.toLowerCase() == "costcenterfullcode")
        nameColumn = this.gridColumnsRow.find(f => f.name.toLowerCase() == "costcentername");
        break;
      }
      case ViewName.Project: {
        fullCodeColumn = this.gridColumnsRow.find(f => f.name.toLowerCase() == "projectfullcode")
        nameColumn = this.gridColumnsRow.find(f => f.name.toLowerCase() == "projectname");
        break;
      }
      default:
    }

    fullCodeColumn.displayIndex = 0.5;
    nameColumn.displayIndex = 0.6;

    this.gridColumnsRow.sort((a, b) => a.displayIndex > b.displayIndex ? 1 : -1);
  }

  openSelectForm(viewId: number) {
    this.dialogRef = this.dialogService.open({
      content: SelectFormComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;

    this.dialogModel.viewID = viewId;
    this.dialogModel.isDisableEntities = true;

    this.dialogRef.content.instance.cancel.subscribe((res) => {
      switch (viewId) {
        case ViewName.Account: {
          this.selectedAccount = undefined;
          this.selectedAccountName = this.getText('BalanceByAccount.AllAccount');
          break;
        }
        case ViewName.DetailAccount: {
          this.selectedDetailAccount = undefined;
          this.selectedDetailAccountName = this.getText('BalanceByAccount.AllDetailAccount');
          break;
        }
        case ViewName.CostCenter: {
          this.selectedCCenter = undefined;
          this.selectedCCenterName = this.getText('BalanceByAccount.AllCostCenter');
          break;
        }
        case ViewName.Project: {
          this.selectedProject = undefined;
          this.selectedProjectName = this.getText('BalanceByAccount.AllProject');
          break;
        }
        default:
      }
      this.changeParam();
      this.dialogRef.close();
    });

    this.dialogRef.content.instance.result.subscribe((res) => {
      switch (res.viewId) {
        case ViewName.Account: {
          this.selectedAccount = res.dataItem;
          this.selectedAccountName = this.selectedAccount.name;
          break;
        }
        case ViewName.DetailAccount: {
          this.selectedDetailAccount = res.dataItem;
          this.selectedDetailAccountName = this.selectedDetailAccount.name;
          break;
        }
        case ViewName.CostCenter: {
          this.selectedCCenter = res.dataItem;
          this.selectedCCenterName = this.selectedCCenter.name;
          break;
        }
        case ViewName.Project: {
          this.selectedProject = res.dataItem;
          this.selectedProjectName = this.selectedProject.name;
          break;
        }
        default:
      }
      this.changeParam();
      this.dialogRef.close();
    });

  }

  getAccountItemLevels(isChecked: boolean, viewId: number) {
    if (isChecked) {
      this.gridService.getModels(String.Format(LookupApi.AccountBookLevels, viewId)).subscribe(res => {
        switch (viewId) {
          case ViewName.Account: {
            this.accountLevelList = res;
            this.chbAccount = true;
            
            if (!this.selectedAccountInit) {
              this.selectedAccountLevel = this.accountLevelList[0].level;
              this.selectedAccountName = this.getText('BalanceByAccount.AllAccount');
            }
            break;
          }
          case ViewName.DetailAccount: {
            this.detailAccountLevelList = res;
            this.chbDetailAccount = true;
            this.selectedDetailAccountLevel = this.detailAccountLevelList[0].level;
            this.selectedDetailAccountName = this.getText('BalanceByAccount.AllDetailAccount');
            break;
          }
          case ViewName.CostCenter: {
            this.cCenterLevelList = res;
            this.chbCCenter = true;
            this.selectedCCenterLevel = this.cCenterLevelList[0].level;
            this.selectedCCenterName = this.getText('BalanceByAccount.AllCostCenter');
            break;
          }
          case ViewName.Project: {
            this.projectLevelList = res;
            this.chbProject = true;
            this.selectedProjectLevel = this.projectLevelList[0].level;
            this.selectedProjectName = this.getText('BalanceByAccount.AllProject');
            break;
          }
          default:
        }

        this.showColumn(viewId);
      })
    }
    else {
      switch (viewId) {
        case ViewName.Account: {
          this.accountLevelList = [];
          this.selectedAccount = undefined;
          this.selectedAccountLevel = undefined;
          this.selectedAccountName = undefined;
          break;
        }
        case ViewName.DetailAccount: {
          this.detailAccountLevelList = [];
          this.selectedDetailAccount = undefined;
          this.selectedDetailAccountLevel = undefined;
          this.selectedDetailAccountName = undefined;
          break;
        }
        case ViewName.CostCenter: {
          this.cCenterLevelList = [];
          this.selectedCCenter = undefined;
          this.selectedCCenterLevel = undefined;
          this.selectedCCenterName = undefined;
          break;
        }
        case ViewName.Project: {
          this.projectLevelList = [];
          this.selectedProject = undefined;
          this.selectedProjectLevel = undefined;
          this.selectedProjectName = undefined;
          break;
        }
        default:
      }

      this.hideColumn(viewId);
    }
  }

  getColumns(e: any) {
    this.gridColumns = e;
    if (!this.selectedBranchSeparation) {
      this.gridColumnsRow = this.gridColumns.filter(f => f.name != "BranchName" &&
        f.name != "AccountFullCode" &&
        f.name != "AccountName" &&
        f.name != "DetailAccountFullCode" &&
        f.name != "DetailAccountName" &&
        f.name != "CostCenterFullCode" &&
        f.name != "CostCenterName" &&
        f.name != "ProjectFullCode" &&
        f.name != "ProjectName");
    }
    else {
      this.gridColumnsRow = this.gridColumns;
    }

    this.showColumn(parseInt(this.selectedReportBy));
  }

  dateValueChange(event: any) {
    this.fromDate = event.fromDate;
    this.toDate = event.toDate;
    this.isDefaultBtn = false;
  }

  getFirstAndLastVoucherNo() {
    this.gridService.getModels(VoucherApi.EnvironmentItemRange).subscribe(res => {
      this.fromVoucher = res.firstNo.toString();
      this.toVoucher = res.lastNo.toString();
    })
  }

  changeBranchSeparation() {
    if (this.isAccess(Entities.BalanceByAccount, BalanceByAccountPermissions.ViewByBranch)) {
      this.isApplyBranchSeparation = true;
      if (!this.selectedBranchSeparation) {
        this.gridColumnsRow = this.gridColumnsRow.filter(f => f.name != "BranchName");
      }
      else {
        var branchColumn = this.gridColumns.find(f => f.name == "BranchName");
        this.gridColumnsRow.push(branchColumn);
        this.gridColumnsRow.sort((a, b) => a.displayIndex > b.displayIndex ? 1 : -1);
      }
      this.changeParam();
    }
    else {
      this.isApplyBranchSeparation = false;
      this.showMessage(this.getText('App.AccessDenied'), MessageType.Warning);
    }
  }

  changeParam() {
    this.isDefaultBtn = false;

    //this.creditSum = 0;
    //this.debitSum = 0;
    this.selectedRows = [];
    this.pageIndex = 0;
    this.showloadingMessage = false;
    this.totalRecords = 0;
    this.rowData = undefined;
  }

  onChangeFilterByRef(event) {
    if (!this.isAccess(Entities.BalanceByAccount, BalanceByAccountPermissions.FilterByRef)) {
      setTimeout(() => {
        this.selectedReferences = [];
      });
      this.showMessage(this.getText('App.AccessDenied'), MessageType.Warning);
    }
  }


  getReportData() {
    var errorMsg;    
    switch (this.selectedReportBy) {
      case ViewName.Account.toString(): {
        if (this.selectedAccount && this.selectedAccountLevel < this.selectedAccount.level)
          errorMsg = String.Format(this.getText('BalanceByAccount.SelectLevelErrorMsg'), this.getText('Entity.Account'));
        break
      }
      case ViewName.DetailAccount.toString(): {
        if (this.selectedDetailAccount && this.selectedDetailAccountLevel < this.selectedDetailAccount.level)
          errorMsg = String.Format(this.getText('BalanceByAccount.SelectLevelErrorMsg'), this.getText('Entity.DetailAccount'));
        break
      }
      case ViewName.CostCenter.toString(): {
        if (this.selectedCCenter && this.selectedCCenterLevel < this.selectedCCenter.level)
          errorMsg = String.Format(this.getText('BalanceByAccount.SelectLevelErrorMsg'), this.getText('Entity.CostCenter'));
        break
      }
      case ViewName.Project.toString(): {
        if (this.selectedProject && this.selectedProjectLevel < this.selectedProject.level)
          errorMsg = String.Format(this.getText('BalanceByAccount.SelectLevelErrorMsg'), this.getText('Entity.Project'));
        break
      }
      default:
    }

    if (!errorMsg &&
      this.selectedAccount &&
      this.selectedReportBy != ViewName.Account.toString() &&
      this.selectedAccountLevel < this.selectedAccount.level) {
      errorMsg = String.Format(this.getText('BalanceByAccount.SelectLevelErrorMsg'), this.getText('Entity.Account'));
    }

    if (!errorMsg &&
      this.selectedDetailAccount &&
      this.selectedReportBy != ViewName.DetailAccount.toString() &&
      this.selectedDetailAccountLevel < this.selectedDetailAccount.level) {
      errorMsg = String.Format(this.getText('BalanceByAccount.SelectLevelErrorMsg'), this.getText('Entity.DetailAccount'));
    }

    if (!errorMsg &&
      this.selectedCCenter &&
      this.selectedReportBy != ViewName.CostCenter.toString() &&
      this.selectedCCenterLevel < this.selectedCCenter.level) {
      errorMsg = String.Format(this.getText('BalanceByAccount.SelectLevelErrorMsg'), this.getText('Entity.CostCenter'));
    }

    if (!errorMsg &&
      this.selectedProject &&
      this.selectedReportBy != ViewName.Project.toString() &&
      this.selectedProjectLevel < this.selectedProject.level) {
      errorMsg = String.Format(this.getText('BalanceByAccount.SelectLevelErrorMsg'), this.getText('Entity.Project'));
    }
   
    if (errorMsg) {
      this.showMessage(errorMsg, MessageType.Warning);
    }
    else {
      this.parameters = {
        viewId: this.selectedReportBy,
        //isByDate: this.reportType == "1" ? true : false,
        fromDate: this.reportType == "1" ? this.fromDate : null,
        toDate: this.reportType == "1" ? this.toDate : null,
        fromNo: this.reportType == "2" ? this.fromVoucher : null,
        toNo: this.reportType == "2" ? this.toVoucher : null,
        isByBranch: this.selectedBranchSeparation,
        accountLevel: this.selectedAccountLevel,
        accountId: this.selectedAccount ? this.selectedAccount.id : null,
        detailAccountLevel: this.selectedDetailAccountLevel,
        detailAccountId: this.selectedDetailAccount ? this.selectedDetailAccount.id : null,
        costCenterLevel: this.selectedCCenterLevel,
        costCenterId: this.selectedCCenter ? this.selectedCCenter.id : null,
        projectLevel: this.selectedProjectLevel,
        projectId: this.selectedProject ? this.selectedProject.id : null,
        IsSelectedAccount: this.chbAccount,
        IsSelectedDetailAccount: this.chbDetailAccount,
        IsSelectedCostCenter: this.chbCCenter,
        IsSelectedProject: this.chbProject,
        options: this.generateOptionNumber()
      }

      //added by nouri
      this.getDataUrl = BalanceByAccountApi.BalanceByAccount;
      this.generateGridOptions();
      //added by nouri

      var options = new ReloadOption();
      options.Parameter = this.parameters;
      this.reloadGrid(options);
    }
  }

  generateGridOptions() {
    this.defaultFilter = [];
    this.quickFilter = [];

    switch (this.selectedVoucherStatus) {
      case "2": {
        this.quickFilter.push(new Filter("VoucherStatusId", this.selectedVoucherStatus, " >= {0}", "System.Int32"));
        break;
      }
      case "3": {
        this.quickFilter.push(new Filter("VoucherStatusId", this.selectedVoucherStatus, " == {0}", "System.Int32"));
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

    if (this.selectedBranchScope == "1") {
      this.quickFilter.push(new Filter("BranchId", this.BranchId.toString(), " == {0}", "System.Int32"));
    }    

  }

  generateOptionNumber() {
    var options = (this.useClosingTempVoucher ? BalanceOptions.UseClosingTempVoucher : 0) |
      (this.useClosingVoucher ? BalanceOptions.UseClosingVoucher : 0) |
      (this.showZeroBalanceItems ? BalanceOptions.ShowZeroBalanceItems : 0) |
      (this.startTurnoverAsInitBalance ? BalanceOptions.StartTurnoverAsInitBalance : 0) |
      (this.openingAsFirstVoucher ? BalanceOptions.OpeningAsFirstVoucher : 0);

    return options;
  }

  onCellClick(e: any) {

  }

  rowDoubleClickHandler() {

  }

  public onGenerateParameters(): any {     
      return this.parameters;
  }

  onAdvanceFilterOk(): any {
    this.getReportData();
  }

  public onDataBind(res: any) {
    this.totalRow = res.total;
  }

  onChangeVoucherStatus() {
    this.changeParam();
    let statusFilterExp: FilterExpression = undefined;
    var statusFilter = this.voucherService.getStatusFilter(this.selectedVoucherStatus);

    if (statusFilter.filter.length > 0) {
      statusFilter.filter.forEach(item => {
        statusFilterExp = this.addFilterToFilterExpression(statusFilterExp,
          item, FilterExpressionOperator.And);
      })

      this.voucherService.getVoucherNumberByStatus(VoucherApi.VoucherCountByStatus, statusFilterExp).subscribe(res => {
        if (res > 0)
          this.showMessage(String.Format(this.getText('Messages.VoucherNumberByStatus'), res.toString(), this.getText(statusFilter.key),statusFilter.url), MessageType.Info);
      })
    }

  }

  showColumn(viewId: number) {

    this.viewId = viewId;
    if (this.gridColumns.length) {
      var fullCodeColumn;
      var nameColumn;

      switch (viewId) {
        case ViewName.Account: {
          fullCodeColumn = this.gridColumns.find(f => f.name.toLowerCase() == "accountfullcode");
          nameColumn = this.gridColumns.find(f => f.name.toLowerCase() == "accountname");
          break;
        }
        case ViewName.DetailAccount: {
          fullCodeColumn = this.gridColumns.find(f => f.name.toLowerCase() == "detailaccountfullcode");
          nameColumn = this.gridColumns.find(f => f.name.toLowerCase() == "detailaccountname");
          break;
        }
        case ViewName.CostCenter: {
          fullCodeColumn = this.gridColumns.find(f => f.name.toLowerCase() == "costcenterfullcode");
          nameColumn = this.gridColumns.find(f => f.name.toLowerCase() == "costcentername");
          break;
        }
        case ViewName.Project: {
          fullCodeColumn = this.gridColumns.find(f => f.name.toLowerCase() == "projectfullcode");
          nameColumn = this.gridColumns.find(f => f.name.toLowerCase() == "projectname");
          break;
        }
        default:
      }

      if (!this.gridColumnsRow.find(f => f.name == fullCodeColumn.name)) {
        this.gridColumnsRow.push(fullCodeColumn);
      }

      if (!this.gridColumnsRow.find(f => f.name == nameColumn.name)) {
        this.gridColumnsRow.push(nameColumn);
      }

      this.gridColumnsRow.sort((a, b) => a.displayIndex > b.displayIndex ? 1 : -1);
    }

    
  }

  hideColumn(viewId) {

    var fullCodeColumn;
    var nameColumn;

    switch (viewId) {
      case ViewName.Account: {
        fullCodeColumn = "accountfullcode";
        nameColumn = "accountname";
        break;
      }
      case ViewName.DetailAccount: {
        fullCodeColumn = "detailaccountfullcode";
        nameColumn = "detailaccountname";
        break;
      }
      case ViewName.CostCenter: {
        fullCodeColumn = "costcenterfullcode";
        nameColumn = "costcentername";
        break;
      }
      case ViewName.Project: {
        fullCodeColumn = "projectfullcode";
        nameColumn = "projectname";
        break;
      }
      default:
    }

    this.gridColumnsRow = this.gridColumnsRow.filter(f => f.name.toLowerCase() != fullCodeColumn && f.name.toLowerCase() != nameColumn);

    this.gridColumnsRow.sort((a, b) => a.displayIndex > b.displayIndex ? 1 : -1);

  }

  showAccountBook() {    
    var selectedRow = this.selectedRows[0];

    if (selectedRow) {
      this.dialogRef = this.dialogService.open({
        content: AccountBookComponent,
        title: this.getText('Entity.AccountBook'),
        height: screen.availHeight
      });

      let item: any;
      let selectedLevel: number;

      switch (parseInt(this.selectedReportBy)) {
        case ViewName.Account: {

          var account = new AccountInfo();
          account.fullCode = selectedRow.accountFullCode;
          account.name = selectedRow.accountName;
          account.id = selectedRow.accountId;
          item = account;

          selectedLevel = selectedRow.accountLevel;
          break;
        }
        case ViewName.DetailAccount: {
          var detailAccount = new DetailAccountInfo();
          detailAccount.fullCode = selectedRow.detailAccountFullCode;
          detailAccount.name = selectedRow.detailAccountName;
          detailAccount.id = selectedRow.detailAccountId;
          item = detailAccount;

          selectedLevel = selectedRow.detailAccountLevel;
          break;
        }
        case ViewName.CostCenter: {
          var costCenter = new CostCenterInfo();
          costCenter.fullCode = selectedRow.costCenterFullCode;
          costCenter.name = selectedRow.costCenterName;
          costCenter.id = selectedRow.costCenterId;
          item = costCenter;

          selectedLevel = selectedRow.costCenterLevel;
          break;
        }
        case ViewName.Project: {
          var project = new ProjectInfo();
          project.fullCode = selectedRow.projectFullCode;
          project.name = selectedRow.projectName;
          project.id = selectedRow.projectId;
          item = project;
          selectedLevel = selectedRow.projectLevel;
          break;
        }
        default:
      }
            

      this.dialogModel = this.dialogRef.content.instance;
      this.dialogModel.selectedModel = item;
      this.dialogModel.disableAccountLookup = true;
      this.dialogModel.fromDate = this.fromDate;
      this.dialogModel.toDate = this.toDate;
      this.dialogModel.selectedBookType = selectedLevel;
      this.dialogModel.selectedEntityId = this.selectedReportBy;
      this.dialogModel.selectedViewId = parseInt(this.selectedReportBy);
      this.dialogModel.getReportData();

    }
    else {
      this.showMessage(this.getText('Balance.PleaseSelectAccount'), MessageType.Warning);
    }
  }

  selectionKey(context: RowArgs): any {
    return context.dataItem;
  }

}




