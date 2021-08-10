import { Component, OnInit, OnDestroy, Renderer2, NgZone, ChangeDetectorRef, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { DialogService, DialogRef } from '@progress/kendo-angular-dialog';
import { ColumnBase, GridComponent, RowArgs } from '@progress/kendo-angular-grid';
import { FormGroup, FormBuilder } from '@angular/forms';
import { String, AutoGeneratedGridComponent, Filter, FilterExpressionOperator, FilterExpression } from '@sppc/shared/class';
import { VoucherService, VoucherLineService } from '@sppc/finance/service';
import { AccountBookApi, VoucherApi } from '@sppc/finance/service/api';
import { MetaDataService, ReportingService, GridService, BrowserStorageService } from '@sppc/shared/services';
import { SettingService } from '@sppc/config/service';
import { Layout, Entities, MessageType } from '@sppc/env/environment';
import { VoucherEditorComponent } from '../../operational/voucher/voucher-editor.component';
import { VoucherStatusResource, BranchScopeResource, AccountBookDisplayTypeResource, AccountBookDisplayType, ArticleTypesResource, ArticleTypesResourceKey } from '@sppc/finance/enum';
import { SelectFormComponent, SppcDateRangeSelector } from '@sppc/shared/controls';
import { Item } from '@sppc/shared/models';
import { ViewName, AccountBookPermissions, VoucherPermissions } from '@sppc/shared/security';
import { LookupApi } from '@sppc/shared/services/api';
import { ReportManagementComponent } from '@sppc/shared/components/reportManagement/reportManagement.component';
import { QuickReportSettingComponent } from '@sppc/shared/components/reportManagement/QuickReport-Setting.component';
import { ViewIdentifierComponent, ReportViewerComponent } from '@sppc/shared/components';
import * as moment from 'jalali-moment';
import { OperationId } from '@sppc/shared/enum/operationId';



export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

const matches = (el, selector) => (el.matches || el.msMatchesSelector).call(el, selector);

@Component({
  selector: 'sppc-accountBook',
  templateUrl: './accountBook.component.html',
  styles: [`
.section-account button { margin: 0 2px; }
.section-account .acc-name{ width: calc(100% - 102px); }
.section-account .acc-code{ width: calc(100% - 133px); position: absolute; top: -5px; }
.section-account .acc-code-rtl { left: 16px; }
.section-account .acc-code-ltr { right: 16px; }

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
`],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class AccountBookComponent extends AutoGeneratedGridComponent implements OnInit, OnDestroy {


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

  bookType: Array<any> = [];

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

  selectedBookType: number = 0;
  displayTypeSelected: string = '1';
  branchScopeSelected: string = '1';
  voucherStatusSelected: string = '2';
  articleTypeSelected: string = '1';
  selectedBranchSeparation: boolean = false;
  gridColumnsRow: any[] = [];
  ddlEntites: Array<Item> = [];
  selectedEntityId: string = '1';

  fromDate: Date;
  toDate: Date;
  initializeDate: boolean = false;

  creditSum: number = 0;
  debitSum: number = 0;
  balance: number = 0;


  isDefaultBtn: boolean = true;
  isApplyBranchSeparation: boolean = false;
  tempViewId: number;

  dialogRef: DialogRef;
  dialogModel: any;

  disableAccountLookup: boolean = false;

  selectedModel: any;
  selectedViewId: number;
  selectedModelTitle: string;
  baseModelTitle: string;
  modelUrl: string;

  clickedRowItem: any = undefined;
  quickFilter: Filter[];

  public formGroup: FormGroup;
  private editedRowIndex: number;
  private docClickSubscription: any;

  @ViewChild(GridComponent) grid: GridComponent;
  @ViewChild(ViewIdentifierComponent) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportViewerComponent) viewer: ReportViewerComponent;
  @ViewChild(ReportManagementComponent) reportManager: ReportManagementComponent;
  @ViewChild(QuickReportSettingComponent) reportSetting: QuickReportSettingComponent;
  @ViewChild(SppcDateRangeSelector) dateRange: SppcDateRangeSelector;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public dialogService: DialogService, public gridService: GridService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, public voucherService: VoucherService, public voucherLineService: VoucherLineService,
    public settingService: SettingService, public reporingService: ReportingService, public ngZone: NgZone, public formBuilder: FormBuilder, public bStorageService: BrowserStorageService) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, bStorageService, cdref, ngZone);

  }

  ngOnInit() {
    this.translate.get('Entity.Account').subscribe(res => {
      this.baseModelTitle = res;
      this.selectedModelTitle = this.baseModelTitle;
    })

    this.entityName = Entities.AccountBookSingle;
    this.viewId = ViewName[this.entityTypeName];
    this.tempViewId = this.viewId;

    this.loadEntity();

    if (this.selectedModel) { 
      var initLevel = this.selectedModel.level ? true : false;
      this.getAccountBookTypes(initLevel);
    }
    else
      this.getAccountBookTypes();


    if (!this.disableAccountLookup)
      this.openSelectForm();
    else
      this.disableAccountLookup = false;

    this.showloadingMessage = false;

    this.docClickSubscription = this.renderer.listen('document', 'click', this.onDocumentClick.bind(this));

    
    if (this.dateRange && this.fromDate && this.toDate) {      
      this.dateRange.InitializeDate = false;
      this.dateRange.fromDatePicker.isDisplayDate = false;
      this.dateRange.toDatePicker.isDisplayDate = false;

      var from = new Date(this.fromDate);
      var to = new Date(this.toDate);

      this.dateRange.fromDatePicker.dateObject = moment(from);
      this.dateRange.toDatePicker.dateObject = moment(to);
      this.dateRange.setInitialDates(from, to);

    }
    else if (this.dateRange) {
      this.dateRange.initDate();
    }
    else {
      this.initializeDate = true;
    }

    this.cdref.detectChanges();
  }

  public ngOnDestroy(): void {
    this.docClickSubscription();
  }

  loadEntity() {
    this.settingService.getAll(LookupApi.TreeViews).subscribe(res => {
      this.ddlEntites = res.body;
    })
  }

  onchangeEntity() {
    this.selectedViewId = parseInt(this.selectedEntityId);
    this.getAccountBookTypes(true);
    this.selectedModel = undefined;
  }

  getAccountBookTypes(isInitValue?: boolean) {
    this.gridService.getModels(String.Format(LookupApi.AccountBookLevels, this.selectedEntityId)).subscribe(res => {
      this.bookType = res;
      if (isInitValue) {
        this.initValue();
      }
    })
  }

  changeBookType() {
    this.selectedModel = undefined;
    this.changeParam();
    var model = this.bookType.find(f => f.key == this.selectedBookType);
    this.selectedViewId = model.viewId;

    this.selectedModelTitle = this.baseModelTitle;
    if (this.selectedViewId != 1)
      this.selectedModelTitle = model.title;

    this.openSelectForm();
  }

  dateValueChange(event: any) {   
    this.fromDate = event.fromDate;
    this.toDate = event.toDate;
    this.isDefaultBtn = false;   
  }

  changeType() {

    switch (parseInt(this.displayTypeSelected)) {
      case AccountBookDisplayType.ByRow:
        {
          this.tempViewId = ViewName[Entities.AccountBookSingle];
          break;
        }
      case AccountBookDisplayType.VoucherSum:
        {
          this.tempViewId = ViewName[Entities.AccountBookSingleSummary];
          break;
        }
      case AccountBookDisplayType.DailySum:
        {
          this.tempViewId = ViewName[Entities.AccountBookSummary];
          break;
        }
      case AccountBookDisplayType.MonthlySum:
        {
          this.tempViewId = ViewName[Entities.AccountBookSummary];
          break;
        }
      default:
    }

    this.changeParam();
  }

  onAdvanceFilterOk(): any {
    this.enableViewListChanged(this.viewId);
    this.operationId = OperationId.Filter;
    this.getReportData();
  }

  public getReportData() {

    if (this.selectedModel) {

      this.changeParam();

      this.defaultFilter = [];
      this.quickFilter = [];

      switch (this.voucherStatusSelected) {
        case "2": {
          this.quickFilter.push(new Filter("VoucherStatusId", this.voucherStatusSelected, ">= {0}", "System.Int32"));
          break;
        }
        case "3": {
          this.quickFilter.push(new Filter("VoucherStatusId", this.voucherStatusSelected, "== {0}", "System.Int32"));
          break;
        }
        case "4": {
          this.quickFilter.push(new Filter("VoucherConfirmedById", "", "!= null", ""));
          break;
        }
        case "5": {
          this.quickFilter.push(new Filter("VoucherApprovedById", "", "!= null", ""));
          break;
        }
        default:
      }

      if (this.branchScopeSelected == "1") {
        this.quickFilter.push(new Filter("BranchId", this.BranchId.toString(), "== {0}", "System.Int32"));
      }

      switch (this.articleTypeSelected) {
        case ArticleTypesResourceKey.MarkedVoucherLines: {
          this.quickFilter.push(new Filter("Mark", "", "!= null", ""));
          break;
        }
        case ArticleTypesResourceKey.UncheckedVoucherLines: {
          this.quickFilter.push(new Filter("Mark", "", "== null", ""));
          break;
        }
        default:
      }

      if (!this.isApplyBranchSeparation)
        this.selectedBranchSeparation = false;

      if (this.fromDate && this.toDate) {

        switch (this.selectedViewId) {
          case ViewName.Account: {

            switch (parseInt(this.displayTypeSelected)) {
              case AccountBookDisplayType.ByRow: {
                this.entityName = Entities.AccountBookSingle;
                this.getDataUrl = this.selectedBranchSeparation
                  ? AccountBookApi.AccountBookByRowByBranch
                  : AccountBookApi.AccountBookByRow;
                break
              }
              case AccountBookDisplayType.VoucherSum: {
                this.entityName = Entities.AccountBookSingleSummary;
                this.getDataUrl = this.selectedBranchSeparation
                  ? AccountBookApi.AccountBookVoucherSumByBranch
                  : AccountBookApi.AccountBookVoucherSum;
                break
              }
              case AccountBookDisplayType.DailySum: {
                this.entityName = Entities.AccountBookSummary;
                this.getDataUrl = this.selectedBranchSeparation
                  ? AccountBookApi.AccountBookDailySumByBranch
                  : AccountBookApi.AccountBookDailySum;
                break
              }
              case AccountBookDisplayType.MonthlySum: {
                this.entityName = Entities.AccountBookSummary;
                this.getDataUrl = this.selectedBranchSeparation
                  ? AccountBookApi.AccountBookMonthlySumByBranch
                  : AccountBookApi.AccountBookMonthlySum;
                break
              }
              default:
            }

            break;
          }
          case ViewName.DetailAccount: {

            switch (parseInt(this.displayTypeSelected)) {
              case AccountBookDisplayType.ByRow: {
                this.entityName = Entities.AccountBookSingle;
                this.getDataUrl = this.selectedBranchSeparation
                  ? AccountBookApi.DetailAccountBookByRowByBranch
                  : AccountBookApi.DetailAccountBookByRow;
                break
              }
              case AccountBookDisplayType.VoucherSum: {
                this.entityName = Entities.AccountBookSingleSummary;
                this.getDataUrl = this.selectedBranchSeparation
                  ? AccountBookApi.DetailAccountBookVoucherSumByBranch
                  : AccountBookApi.DetailAccountBookVoucherSum;
                break
              }
              case AccountBookDisplayType.DailySum: {
                this.entityName = Entities.AccountBookSummary;
                this.getDataUrl = this.selectedBranchSeparation
                  ? AccountBookApi.DetailAccountBookDailySumByBranch
                  : AccountBookApi.DetailAccountBookDailySum;
                break
              }
              case AccountBookDisplayType.MonthlySum: {
                this.entityName = Entities.AccountBookSummary;
                this.getDataUrl = this.selectedBranchSeparation
                  ? AccountBookApi.DetailAccountBookMonthlySumByBranch
                  : AccountBookApi.DetailAccountBookMonthlySum;
                break
              }
              default:
            }

            break;
          }
          case ViewName.CostCenter: {

            switch (parseInt(this.displayTypeSelected)) {
              case AccountBookDisplayType.ByRow: {
                this.entityName = Entities.AccountBookSingle;
                this.getDataUrl = this.selectedBranchSeparation
                  ? AccountBookApi.CostCenterBookByRowByBranch
                  : AccountBookApi.CostCenterBookByRow;
                break
              }
              case AccountBookDisplayType.VoucherSum: {
                this.entityName = Entities.AccountBookSingleSummary;
                this.getDataUrl = this.selectedBranchSeparation
                  ? AccountBookApi.CostCenterBookVoucherSumByBranch
                  : AccountBookApi.CostCenterBookVoucherSum;
                break
              }
              case AccountBookDisplayType.DailySum: {
                this.entityName = Entities.AccountBookSummary;
                this.getDataUrl = this.selectedBranchSeparation
                  ? AccountBookApi.CostCenterBookDailySumByBranch
                  : AccountBookApi.CostCenterBookDailySum;
                break
              }
              case AccountBookDisplayType.MonthlySum: {
                this.entityName = Entities.AccountBookSummary;
                this.getDataUrl = this.selectedBranchSeparation
                  ? AccountBookApi.CostCenterBookMonthlySumByBranch
                  : AccountBookApi.CostCenterBookMonthlySum;
                break
              }
              default:
            }

            break;
          }
          case ViewName.Project: {

            switch (parseInt(this.displayTypeSelected)) {
              case AccountBookDisplayType.ByRow: {
                this.entityName = Entities.AccountBookSingle;
                this.getDataUrl = this.selectedBranchSeparation
                  ? AccountBookApi.ProjectBookByRowByBranch
                  : AccountBookApi.ProjectBookByRow;
                break
              }
              case AccountBookDisplayType.VoucherSum: {
                this.entityName = Entities.AccountBookSingleSummary;
                this.getDataUrl = this.selectedBranchSeparation
                  ? AccountBookApi.ProjectBookVoucherSumByBranch
                  : AccountBookApi.ProjectBookVoucherSum;
                break
              }
              case AccountBookDisplayType.DailySum: {
                this.entityName = Entities.AccountBookSummary;
                this.getDataUrl = this.selectedBranchSeparation
                  ? AccountBookApi.ProjectBookDailySumByBranch
                  : AccountBookApi.ProjectBookDailySum;
                break
              }
              case AccountBookDisplayType.MonthlySum: {
                this.entityName = Entities.AccountBookSummary;
                this.getDataUrl = this.selectedBranchSeparation
                  ? AccountBookApi.ProjectBookMonthlySumByBranch
                  : AccountBookApi.ProjectBookMonthlySum;
                break
              }
              default:
            }

            break;
          }
          default:
        }

        this.tempViewId = ViewName[this.entityTypeName]

        this.getDataUrl = String.Format(this.getDataUrl, this.selectedModel.id) + "?from=" + this.fromDate + "&to=" + this.toDate;
        this.reloadGrid();
      }

    }
    else {
      var bookType = this.bookType.find(f => f.key == this.selectedBookType);
      if (bookType) {
        var entityName = ViewName[bookType.viewId];

        this.showMessage(String.Format(this.getText('AccountBook.ModelNotValid'), this.getText("Entity." + entityName)), MessageType.Info);
      }
    }
  }

  onDataBind(res: any) {
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
    this.creditSum = res.creditSum;
    this.debitSum = res.debitSum;
    this.balance = res.balance;
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
  }

  changeBranchSeparation() {
    if (this.isAccess(Entities.AccountBook, AccountBookPermissions.ByBranch)) {
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
    if (column.field != 'mark')
      this.clickedRowItem = dataItem;

    if (this.isAccess(Entities.AccountBook, AccountBookPermissions.Mark)) {

      if (dataItem.voucherNo == 0) {
        this.saveCurrentMark();
        return;
      }

      if (isEdited || (this.formGroup && !this.formGroup.valid) || (column.field != 'mark' && this.editedRowIndex == rowIndex)) {
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
    if (this.selectedRows.length == 1 && (this.displayTypeSelected == AccountBookDisplayType.ByRow.toString() || this.displayTypeSelected == AccountBookDisplayType.VoucherSum.toString()))
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

      if (dataModel.mark != null && dataModel.mark.replace(/\s/g, "").length == 0)
        dataModel.mark = null;

      this.voucherLineService.putArticleMark(dataModel.id, dataModel.mark).subscribe(res => {
        var item = this.rowData.data.find(f => f.id == dataModel.id);
        item.mark = dataModel.mark;
      })

      this.closeEditor();
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

      if (this.selectedViewId.toString() != this.selectedEntityId) {
        this.selectedEntityId = this.selectedViewId.toString();
        this.getAccountBookTypes(true);
      }
      else {
        this.initValue();
      }

      this.dialogRef.close();
    });

  }

  initValue() {
    this.selectedModelTitle = this.baseModelTitle;
    let model: any;
    if (this.selectedModel) {
      model = this.bookType.find(f => f.viewId == this.selectedViewId && f.level == this.selectedModel.level);
    }
    else {
      model = this.bookType.find(f => f.viewId == this.selectedViewId);
    }

    if (model) {
      this.selectedBookType = model.key;

      if (this.selectedViewId != 1)
        this.selectedModelTitle = model.title;
    }
  }

  nextModel() {
    if (this.selectedModel)
      this.modelUrl = String.Format(AccountBookApi.NextEnvironmentItem, this.selectedViewId, this.selectedModel.id);

    this.getModel();
  }

  previousModel() {
    if (this.selectedModel) {
      this.modelUrl = String.Format(AccountBookApi.PreviousEnvironmentItem, this.selectedViewId, this.selectedModel.id);
      this.getModel();
    }
    
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

  onChangeVoucherStatus() {
    this.changeParam();
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

  selectionKey(context: RowArgs): any {
    return context.dataItem;
  }

}


