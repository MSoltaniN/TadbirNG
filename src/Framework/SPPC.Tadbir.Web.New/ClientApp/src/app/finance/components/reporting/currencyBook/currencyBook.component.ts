import { Component, OnInit, OnDestroy, Renderer2, NgZone, ChangeDetectorRef } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { DialogService, DialogRef } from '@progress/kendo-angular-dialog';
import { ColumnBase } from '@progress/kendo-angular-grid';
import { FormGroup, FormBuilder } from '@angular/forms';
import { String, AutoGeneratedGridComponent, Filter, FilterExpressionOperator, FilterExpression, FilterExpressionBuilder } from '@sppc/shared/class';
import { VoucherService, VoucherLineService } from '@sppc/finance/service';
import { VoucherApi, CurrencyBookApi } from '@sppc/finance/service/api';
import { MetaDataService, ReportingService, GridService, BrowserStorageService, LookupService } from '@sppc/shared/services';
import { SettingService } from '@sppc/config/service';
import { Layout, Entities, MessageType } from '@sppc/env/environment';
import { VoucherEditorComponent } from '@sppc/finance/components/operational/voucher/voucher-editor.component';
import { VoucherStatusResource, BranchScopeResource, AccountBookDisplayTypeResource, AccountBookDisplayType, ArticleTypesResource, ArticleTypesResourceKey } from '@sppc/finance/enum';
import { SelectFormComponent } from '@sppc/shared/controls';
import { Item } from '@sppc/shared/models';
import { ViewName, VoucherPermissions, CurrencyBookPermissions } from '@sppc/shared/security';
import { Account, DetailAccount, CostCenter, Project, CurrencyBookDefaultInfo } from '@sppc/finance/models';



export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

const matches = (el, selector) => (el.matches || el.msMatchesSelector).call(el, selector);

@Component({
  selector: 'sppc-currencyBook',
  templateUrl: './currencyBook.component.html',
  styles: [`
.section-account button { margin: 5px 2px 0 }
.section-account .acc-name{ width: calc(100% - 34px); margin-top: 5px; }
.section-account .acc-code{ width: calc(100% - 131px); position: absolute; top: -5px; }
.section-account .acc-code-rtl { left: 16px; }
.section-account .acc-code-ltr { right: 16px; }

.section-option { margin-top: 15px; background-color: #f6f6f6; border: solid 1px #dadde2; padding: 15px 15px 0; }
.section-option label,input[type=text] { width:100% } /deep/.section-option kendo-dropdownlist { width:100% }
/deep/ .k-switch-on .k-switch-handle { left: -8px !important; }
/deep/ .k-switch-off .k-switch-handle { left: -4px !important; }
/deep/ .k-switch[dir="rtl"] .k-switch-label-on { right: -22px; }
/deep/ .k-switch[dir="rtl"] .k-switch-label-off { left: -18px; }
/deep/ .k-switch-label-on,/deep/ .k-switch-label-off { overflow: initial; }
.journal-type { margin:0 15px 10px; } .journal-type label { margin-top:10px; }
/deep/.k-footer-template { background-color: #b3b3b3; color: #000;}
.btn-compute-default {margin-top: 25px; border: 2px solid #337ab7; color: #337ab7; padding: 5px 25px;}
.btn-compute { color: #337ab7; transition: All 0.3s 0.1s ease-out;}
.btn-compute-selectable{ color: #fff; background-image: linear-gradient(#c1e3ff, #337ab7);}
`],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class CurrencyBookComponent extends AutoGeneratedGridComponent implements OnInit, OnDestroy {


  branchScope: Array<Item> = [
    { value: BranchScopeResource.CurrentBranch, key: "1" },
    { value: BranchScopeResource.CurrentBranchAndSubsets, key: "2" },
  ]

  voucherStatus: Array<Item> = [
    { value: VoucherStatusResource.Committed, key: "2" },
    { value: VoucherStatusResource.Finalized, key: "3" },
    { value: VoucherStatusResource.Confirmed, key: "4" },
    { value: VoucherStatusResource.Approved, key: "5" },
    { value: VoucherStatusResource.AllVouchers, key: "0" }
  ]

  displayType: Array<Item> = [
    { value: AccountBookDisplayTypeResource.ByRow, key: AccountBookDisplayType.ByRow.toString() },
    { value: AccountBookDisplayTypeResource.VoucherSum, key: AccountBookDisplayType.VoucherSum.toString() },
    { value: AccountBookDisplayTypeResource.DailySum, key: AccountBookDisplayType.DailySum.toString() },
    { value: AccountBookDisplayTypeResource.MonthlySum, key: AccountBookDisplayType.MonthlySum.toString() }
  ]

  articleType: Array<Item> = [
    { value: ArticleTypesResource.AllVoucherLines, key: ArticleTypesResourceKey.AllVoucherLines },
    { value: ArticleTypesResource.MarkedVoucherLines, key: ArticleTypesResourceKey.MarkedVoucherLines },
    { value: ArticleTypesResource.UncheckedVoucherLines, key: ArticleTypesResourceKey.UncheckedVoucherLines },
  ]

  selectedCurrencyValue: string = null;
  currenciesRows: Array<Item> = [];
  filteredCurrencies: Array<Item>;

  displayTypeSelected: string = '1';
  branchScopeSelected: string = '1';
  voucherStatusSelected: string = '2';
  articleTypeSelected: string = '1';
  selectedBranchSeparation: boolean = false;
  gridColumnsRow: any[] = [];

  fromDate: Date;
  toDate: Date;

  creditSum: number = 0;
  debitSum: number = 0;
  balance: number = 0;

  baseCurrencyCreditSum: number = 0;
  baseCurrencyDebitSum: number = 0;
  baseCurrencyBalance: number = 0;

  isDefaultBtn: boolean = true;
  isApplyBranchSeparation: boolean = false;
  tempViewId: number;

  dialogRef: DialogRef;
  dialogModel: any;

  chbAccount: boolean = false;
  chbDetailAccount: boolean = false;
  chbCostCenter: boolean = false;
  chbProject: boolean = false;
  selectedAccount: Account;
  selectedDetailAccount: DetailAccount;
  selectedCostCenter: CostCenter;
  selectedProject: Project;
  currencyFree: boolean = false;

  modelUrl: string;

  clickedRowItem: any = undefined;

  public formGroup: FormGroup;
  private editedRowIndex: number;
  private docClickSubscription: any;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public dialogService: DialogService, public gridService: GridService, public lookupService: LookupService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, public voucherService: VoucherService, public voucherLineService: VoucherLineService,
    public settingService: SettingService, public reporingService: ReportingService, public ngZone: NgZone, public formBuilder: FormBuilder, public bStorageService: BrowserStorageService) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, bStorageService, cdref, ngZone);

  }

  ngOnInit() {
    this.entityName = Entities.CurrencyBook;
    this.viewId = ViewName[this.entityTypeName];

    this.getCurrencies();

    this.showloadingMessage = false;

    this.getOptions();

    this.docClickSubscription = this.renderer.listen('document', 'click', this.onDocumentClick.bind(this));

    this.cdref.detectChanges();
  }

  public ngOnDestroy(): void {
    this.docClickSubscription();
  }

  dateValueChange(event: any) {
    this.fromDate = event.fromDate;
    this.toDate = event.toDate;
    this.isDefaultBtn = false;
  }

  getCurrencies() {
    this.lookupService.GetCurrenciesLookup().subscribe(res => {
      this.currenciesRows.push({ key: null, value: this.getText('CurrencyBook.AllCurrencies') });
      this.currenciesRows.push(...res);
      this.filteredCurrencies = this.currenciesRows;
    })
  }

  getOptions() {
    var selectedOptions = this.bStorageService.getCurrencyBookDefault();
    if (selectedOptions) {
      this.articleTypeSelected = selectedOptions.articleType;
      this.branchScopeSelected = selectedOptions.branchScope;
      this.selectedBranchSeparation = selectedOptions.branchSeparation;
      this.currencyFree = selectedOptions.currencyFreeRows;
      this.selectedCurrencyValue = selectedOptions.currencyId;
      this.displayTypeSelected = selectedOptions.displayType;
      this.voucherStatusSelected = selectedOptions.voucherStatus;
    }
  }

  setOptions() {
    var selectedOptions = new CurrencyBookDefaultInfo();

    selectedOptions.articleType = this.articleTypeSelected;
    selectedOptions.branchScope = this.branchScopeSelected;
    selectedOptions.branchSeparation = this.selectedBranchSeparation;
    selectedOptions.currencyFreeRows = this.currencyFree;
    selectedOptions.currencyId = this.selectedCurrencyValue;
    selectedOptions.displayType = this.displayTypeSelected;
    selectedOptions.voucherStatus = this.voucherStatusSelected;

    this.bStorageService.setCurrencyBookDefault(selectedOptions);
  }

  changeType() {

    switch (parseInt(this.displayTypeSelected)) {
      case AccountBookDisplayType.ByRow:
        {
          this.tempViewId = ViewName[Entities.CurrencyBookSingle];
          break;
        }
      case AccountBookDisplayType.VoucherSum:
        {
          this.tempViewId = ViewName[Entities.CurrencyBookSingleSummary];
          break;
        }
      case AccountBookDisplayType.DailySum:
        {
          this.tempViewId = ViewName[Entities.CurrencyBookSummary];
          break;
        }
      case AccountBookDisplayType.MonthlySum:
        {
          this.tempViewId = ViewName[Entities.CurrencyBookSummary];
          break;
        }
      default:
    }

    this.changeParam();
  }

  getReportData() {

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

    switch (this.articleTypeSelected) {
      case ArticleTypesResourceKey.MarkedVoucherLines: {
        this.defaultFilter.push(new Filter("Mark", "", "!= null", ""));
        break;
      }
      case ArticleTypesResourceKey.UncheckedVoucherLines: {
        this.defaultFilter.push(new Filter("Mark", "", "== null", ""));
        break;
      }
      default:
    }

    if (this.selectedCurrencyValue) {
      this.defaultFilter.push(new Filter("CurrencyId", this.selectedCurrencyValue.toString(), "== {0}", "System.Int32"));
    }

    if (!this.isApplyBranchSeparation)
      this.selectedBranchSeparation = false;

    if (this.fromDate && this.toDate) {
      if (!this.selectedCurrencyValue) {
        this.entityName = Entities.CurrencyBook;
        this.tempViewId = ViewName[this.entityTypeName];
        this.getDataUrl = CurrencyBookApi.CurrencyBookAllCurrencies;

        this.getDataUrl = String.Format(this.getDataUrl + "?from={1}&to={2}", this.currencyFree, this.fromDate, this.toDate);
      }
      else {
        switch (parseInt(this.displayTypeSelected)) {
          case AccountBookDisplayType.ByRow: {
            this.entityName = Entities.CurrencyBookSingle;
            this.getDataUrl = CurrencyBookApi.CurrencyBookByRow;
            break
          }
          case AccountBookDisplayType.VoucherSum: {
            this.entityName = Entities.CurrencyBookSingleSummary;
            this.getDataUrl = CurrencyBookApi.CurrencyBookVoucherSum;
            break
          }
          case AccountBookDisplayType.DailySum: {
            this.entityName = Entities.CurrencyBookSummary;
            this.getDataUrl = CurrencyBookApi.CurrencyBookDailySum;
            break
          }
          case AccountBookDisplayType.MonthlySum: {
            this.entityName = Entities.CurrencyBookSummary;
            this.getDataUrl = CurrencyBookApi.CurrencyBookMonthlySum;
            break
          }
          default:
        }

        this.getDataUrl = String.Format(this.getDataUrl + "?from={1}&to={2}", this.selectedBranchSeparation, this.fromDate, this.toDate);
      }


      if (this.selectedAccount)
        this.getDataUrl += "&accountId=" + this.selectedAccount.id;

      if (this.selectedDetailAccount)
        this.getDataUrl += "&faccount=" + this.selectedDetailAccount.id;

      if (this.selectedCostCenter)
        this.getDataUrl += "&ccenterId=" + this.selectedCostCenter.id;

      if (this.selectedProject)
        this.getDataUrl += "&projectId=" + this.selectedProject.id;

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
        this.balance = resData.balance;

        this.baseCurrencyCreditSum = resData.baseCurrencyCreditSum;
        this.baseCurrencyDebitSum = resData.baseCurrencyDebitSum;
        this.baseCurrencyBalance = resData.baseCurrencyBalance;

        this.showloadingMessage = !(resData.items.length == 0);
        this.totalRecords = totalCount;
        this.grid.loading = false;
      })
    }
    this.cdref.detectChanges();
  }

  changeParam() {
    this.isDefaultBtn = false;

    this.creditSum = 0;
    this.debitSum = 0;
    this.balance = 0;
    this.selectedRows = [];
    this.pageIndex = 0;
    this.showloadingMessage = false;
    this.totalRecords = 0;
    this.rowData = undefined;

    this.setOptions();
  }

  changeBranchSeparation() {
    if (this.isAccess(Entities.CurrencyBook, CurrencyBookPermissions.ByBranch)) {
      this.isApplyBranchSeparation = true;
      if (!this.selectedBranchSeparation) {
        this.gridColumnsRow = this.gridColumns.filter(f => f.name != "BranchName");
      }
      else {
        this.gridColumnsRow = this.gridColumns;
      }
      this.changeParam();
    }
    else {
      this.isApplyBranchSeparation = false;
      this.showMessage(this.getText('App.AccessDenied'), MessageType.Warning);
    }
  }

  getColumns(e: any) {
    this.gridColumns = e;
    if (!this.selectedBranchSeparation) {
      this.gridColumnsRow = this.gridColumns.filter(f => f.name != "BranchName");
    }
    else {
      this.gridColumnsRow = this.gridColumns;
    }

  }

  onCellClick({ rowIndex, column, dataItem, isEdited }) {
    if (column && column.field != 'mark')
      this.clickedRowItem = dataItem;

    if (this.isAccess(Entities.CurrencyBook, CurrencyBookPermissions.Mark)) {

      if (dataItem.voucherNo == 0) {
        this.saveCurrentMark();
        return;
      }

      if (isEdited || (this.formGroup && !this.formGroup.valid) || (column && column.field != 'mark' && this.editedRowIndex == rowIndex)) {
        return;
      }

      this.saveCurrentMark();
      this.formGroup = this.createFormGroup(dataItem);
      this.editedRowIndex = rowIndex;
      this.grid.editRow(this.editedRowIndex, this.formGroup);
    }
  }

  closeEditor(): void {
    this.grid.closeRow(this.editedRowIndex);
    this.editedRowIndex = undefined;
    this.formGroup = undefined;
  }

  isEnableVoucherInfoBtn = () => {
    if (!this.selectedCurrencyValue) {
      return true;
    }
    if (this.selectedRows.length == 1 &&
      (this.displayTypeSelected == AccountBookDisplayType.ByRow.toString() || this.displayTypeSelected == AccountBookDisplayType.VoucherSum.toString()))
      return false;
    else
      return true;
  }

  createFormGroup(dataItem: any): FormGroup {
    return this.formBuilder.group({
      'id': dataItem.id,
      'mark': dataItem.mark
    });
  }

  saveCurrentMark(): void {
    if (this.formGroup) {
      var dataModel = this.formGroup.value;
      if (dataModel && dataModel.id) {
        if (dataModel.mark != null && dataModel.mark.replace(/\s/g, "").length == 0)
          dataModel.mark = null;

        this.voucherLineService.putArticleMark(dataModel.id, dataModel.mark).subscribe(res => {
          var item = this.rowData.data.find(f => f.id == dataModel.id);
          item.mark = dataModel.mark;
        })

        this.closeEditor();
      }
    }

  }

  onDocumentClick(e: any): void {
    if (!matches(e.target, '.k-grid-content tbody *'))
      if (this.formGroup && this.formGroup.valid) {
        this.saveCurrentMark();
      }
      else {
        this.closeEditor();
      }
  }

  rowDoubleClickHandler() {
    if (this.clickedRowItem) {

      if (this.displayTypeSelected == AccountBookDisplayType.ByRow.toString() || this.displayTypeSelected == AccountBookDisplayType.VoucherSum.toString()) {
        if (this.isAccess(Entities.Voucher, VoucherPermissions.Edit)) {
          var voucherNo = this.clickedRowItem.voucherNo;

          if (voucherNo > 0) {
            this.voucherService.getModels(String.Format(VoucherApi.VoucherByNo, voucherNo)).subscribe(res => {

              var voucherModel = res;
              if (voucherModel) {
                const dialogRef = this.dialogService.open({
                  title: this.getText('Voucher.VoucherDetail'),
                  content: VoucherEditorComponent,
                });

                const dialogModel = dialogRef.content.instance;
                dialogModel.voucherItem = voucherModel;
              }

              this.clickedRowItem = undefined;
            })
          }
        }
        else
          this.showMessage(this.getText('App.AccessDenied'), MessageType.Warning);

      }
    }

  }

  onVoucherHandler() {
    if (this.displayTypeSelected == AccountBookDisplayType.ByRow.toString() || this.displayTypeSelected == AccountBookDisplayType.VoucherSum.toString()) {
      this.clickedRowItem = this.selectedRows[0];
      this.rowDoubleClickHandler();
    }
  }

  openSelectForm(mode: string) {

    var viewId = 0;
    switch (mode) {
      case 'account': {
        viewId = ViewName.Account;
        break;
      }
      case 'detailAccount': {
        viewId = ViewName.DetailAccount;
        break;
      }
      case 'costCenter': {
        viewId = ViewName.CostCenter;
        break;
      }
      case 'project': {
        viewId = ViewName.Project;
        break;
      }
      default:
    }

    this.dialogRef = this.dialogService.open({
      content: SelectFormComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;

    this.dialogModel.viewID = viewId;
    this.dialogModel.isDisableEntities = true;

    this.dialogRef.content.instance.cancel.subscribe((res) => {
      this.dialogRef.close();
    });

    this.dialogRef.content.instance.result.subscribe((res) => {
      this.changeParam();
      switch (mode) {
        case 'account': {
          this.selectedAccount = res.dataItem;
          break;
        }
        case 'detailAccount': {
          this.selectedDetailAccount = res.dataItem;
          break;
        }
        case 'costCenter': {
          this.selectedCostCenter = res.dataItem;
          break;
        }
        case 'project': {
          this.selectedProject = res.dataItem;
          break;
        }
        default:
      }

      this.dialogRef.close();
    });

  }

  onChangeCheckboxFullAccount(event: any, mode: string) {
    if (!event) {
      switch (mode) {
        case 'account': {
          this.selectedAccount = undefined;
          break;
        }
        case 'detailAccount': {
          this.selectedDetailAccount = undefined;
          break;
        }
        case 'costCenter': {
          this.selectedCostCenter = undefined;
          break;
        }
        case 'project': {
          this.selectedProject = undefined;
          break;
        }
        default:
      }
    }
  }

  onChangeCurrency() {

    if (this.selectedCurrencyValue) {
      this.currencyFree = false;
    }

    this.changeParam();
  }

  public isShowDetailGrid(dataItem: any): boolean {
    return dataItem.hasChild;
  }

  handleFilter(value: any) {
    this.filteredCurrencies = this.currenciesRows.filter((s) => s.value.toLowerCase().indexOf(value.toLowerCase()) !== -1);
  }

  onChangeCurrencyFree() {
    this.changeParam();
  }

  onChangeVoucherStatus() {
    this.changeParam();
    let statusFilter: Filter[] = [];
    let statusFilterExp: FilterExpression = undefined;
    var statusKey = "";

    switch (this.voucherStatusSelected) {
      case "2": {
        statusFilter.push(new Filter("StatusId", "1", "== {0}", "System.Int32"));
        statusKey = VoucherStatusResource.NotCommitted;
        break;
      }
      case "3": {
        statusFilter.push(new Filter("StatusId", "2", "== {0}", "System.Int32"));
        statusKey = VoucherStatusResource.NotFinalized;
        break;
      }
      case "4": {
        statusFilter.push(new Filter("ConfirmedById", "", "== null", ""));
        statusKey = VoucherStatusResource.NotConfirmed;
        break;
      }
      case "5": {
        statusFilter.push(new Filter("ConfirmedById", "", "!= null", ""));
        statusFilter.push(new Filter("ApprovedById", "", "== null", ""));
        statusKey = VoucherStatusResource.NotApproved;
        break;
      }
      default:
    }
    if (statusFilter.length > 0) {
      statusFilter.forEach(item => {
        statusFilterExp = this.addFilterToFilterExpression(statusFilterExp,
          item, FilterExpressionOperator.And);
      })

      this.voucherService.getVoucherNumberByStatus(VoucherApi.VoucherCountByStatus, statusFilterExp).subscribe(res => {
        this.showMessage(String.Format(this.getText('Messages.VoucherNumberByStatus'), res.toString(), this.getText(statusKey)), MessageType.Info);
      })
    }

  }
}


