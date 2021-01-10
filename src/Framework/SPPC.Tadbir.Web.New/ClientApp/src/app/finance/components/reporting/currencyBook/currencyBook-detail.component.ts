import { Component, OnInit, Renderer2, ChangeDetectorRef, NgZone, Input, OnDestroy } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { DialogService, DialogRef } from '@progress/kendo-angular-dialog';
import { Layout, Entities, MessageType } from '@sppc/env/environment';
import { MetaDataService, GridService, BrowserStorageService } from '@sppc/shared/services';
import { Currency, DetailAccount, CostCenter, Project } from '@sppc/finance/models';
import { VoucherLineService, VoucherService } from '@sppc/finance/service';
import { CurrencyBookApi, VoucherApi } from '@sppc/finance/service/api';
import { SettingService } from '@sppc/config/service';
import { String, AutoGeneratedGridComponent, Filter, FilterExpressionOperator, FilterExpression } from '@sppc/shared/class';
import { ViewName, VoucherPermissions, CurrencyBookPermissions } from '@sppc/shared/security';
import { Router } from '@angular/router';
import { ArticleTypesResourceKey, AccountBookDisplayType } from '@sppc/finance/enum';
import { FormGroup, FormBuilder } from '@angular/forms';
import { VoucherEditorComponent } from '@sppc/finance/components/operational/voucher/voucher-editor.component';




export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

const matches = (el, selector) => (el.matches || el.msMatchesSelector).call(el, selector);

@Component({
  selector: 'currency-book-detail',
  templateUrl: './currencyBook-detail.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class CurrencyBookDetailComponent extends AutoGeneratedGridComponent implements OnInit, OnDestroy {

  @Input() currencyName: string;
  @Input() currencyId: number;
  @Input() displayType: string;
  @Input() selectedBranchScope: string;
  @Input() selectedBranchSeparation: number;
  @Input() voucherStatus: string;
  @Input() articleType: string;
  @Input() selectedAccount: Account;
  @Input() selectedDetailAccount: DetailAccount;
  @Input() selectedCostCenter: CostCenter;
  @Input() selectedProject: Project;
  @Input() fromDate: Date;
  @Input() toDate: Date;

  creditSum: number = 0;
  debitSum: number = 0;
  balance: number = 0;
  baseCurrencyCreditSum: number = 0;
  baseCurrencyDebitSum: number = 0;
  baseCurrencyBalance: number = 0;

  gridColumnsRow: any[] = [];

  defaultFilter: Array<Filter> = [];
  quickFilter: Filter[];

  public dialogRef: DialogRef;
  public dialogModel: any;


  clickedRowItem: any = undefined;

  public formGroup: FormGroup;
  private editedRowIndex: number;
  private docClickSubscription: any;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public gridService: GridService, public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, public formBuilder: FormBuilder, public dialogService: DialogService,
    public settingService: SettingService, public ngZone: NgZone, public router: Router, public voucherService: VoucherService, public voucherLineService: VoucherLineService) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, bStorageService, cdref, ngZone);
  }

  ngOnInit() {
    this.listChanged = false;
    this.handleReportParams();    
    //comment : duplicate use of reload method
    //this.reloadGrid();

    this.docClickSubscription = this.renderer.listen('document', 'click', this.onDocumentClick.bind(this));
    this.cdref.detectChanges();
  }

  public ngOnDestroy(): void {
    this.docClickSubscription();
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

  handleReportParams() {
    this.defaultFilter = [];
    this.quickFilter = [];

    switch (this.voucherStatus) {
      case "2": {
        this.quickFilter.push(new Filter("VoucherStatusId", this.voucherStatus, ">= {0}", "System.Int32"));
        break;
      }
      case "3": {
        this.quickFilter.push(new Filter("VoucherStatusId", this.voucherStatus, "== {0}", "System.Int32"));
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

    if (this.selectedBranchScope == "1") {
      this.quickFilter.push(new Filter("BranchId", this.BranchId.toString(), "== {0}", "System.Int32"));
    }

    switch (this.articleType) {
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

    if (this.currencyId)
      this.quickFilter.push(new Filter("CurrencyId", this.currencyId.toString(), "== {0}", "System.Int32"));
    else
      this.quickFilter.push(new Filter("CurrencyId", "", "== null", ""));



    if (this.fromDate && this.toDate) {

      switch (parseInt(this.displayType)) {
        case AccountBookDisplayType.ByRow: {
          this.entityName = Entities.CurrencyBookSingle;
          this.viewId = ViewName[Entities.CurrencyBookSingle];
          this.getDataUrl = CurrencyBookApi.CurrencyBookByRow;
          break
        }
        case AccountBookDisplayType.VoucherSum: {
          this.entityName = Entities.CurrencyBookSingleSummary;
          this.viewId = ViewName[Entities.CurrencyBookSingleSummary];
          this.getDataUrl = CurrencyBookApi.CurrencyBookVoucherSum;
          break
        }
        case AccountBookDisplayType.DailySum: {
          this.entityName = Entities.CurrencyBookSummary;
          this.viewId = ViewName[Entities.CurrencyBookSummary];
          this.getDataUrl = CurrencyBookApi.CurrencyBookDailySum;
          break
        }
        case AccountBookDisplayType.MonthlySum: {
          this.entityName = Entities.CurrencyBookSummary;
          this.viewId = ViewName[Entities.CurrencyBookSummary];
          this.getDataUrl = CurrencyBookApi.CurrencyBookMonthlySum;
          break
        }
        default:
      }

      this.getDataUrl = String.Format(this.getDataUrl + "?from={1}&to={2}", this.selectedBranchSeparation, this.fromDate, this.toDate);



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

  //reloadGrid(insertedModel?: Currency) {
  //  this.grid.loading = true;
  //  if (this.totalRecords == this.skip && this.totalRecords != 0) {
  //    this.skip = this.skip - this.pageSize;
  //  }

  //  if (insertedModel)
  //    this.goToLastPage(this.totalRecords);

  //  var currentFilter = this.currentFilter;
  //  this.defaultFilter.forEach(item => {
  //    currentFilter = this.addFilterToFilterExpression(currentFilter,
  //      item, FilterExpressionOperator.And);
  //  })

  //  var quickFilterExp: FilterExpression;
  //  this.quickFilter.forEach(item => {
  //    quickFilterExp = this.addFilterToFilterExpression(quickFilterExp,
  //      item, FilterExpressionOperator.And);
  //  });

  //  var filter = currentFilter;

  //  this.gridService.getAll(this.getDataUrl, this.pageIndex, this.pageSize, this.sort, filter, quickFilterExp).subscribe((res) => {      

  //    var resData = res.body;
  //    var totalCount = 0;

  //    if (res.headers != null) {
  //      var headers = res.headers != undefined ? res.headers : null;
  //      if (headers != null) {
  //        var retheader = headers.get('X-Total-Count');
  //        if (retheader != null)
  //          totalCount = parseInt(retheader.toString());
  //      }
  //    }
  //    this.rowData = {
  //      data: resData.items,
  //      total: totalCount
  //    }

  //    this.creditSum = resData.creditSum;
  //    this.debitSum = resData.debitSum;
  //    this.balance = resData.balance;
  //    this.baseCurrencyCreditSum = resData.baseCurrencyCreditSum;
  //    this.baseCurrencyDebitSum = resData.baseCurrencyDebitSum;
  //    this.baseCurrencyBalance = resData.baseCurrencyBalance;

  //    this.showloadingMessage = !(resData.items.length == 0);
  //    this.totalRecords = totalCount;

  //    this.grid.loading = false;
  //  })

  //}

  onDataBind(resData) {
    this.creditSum = resData.creditSum;
    this.debitSum = resData.debitSum;
    this.balance = resData.balance;
    this.baseCurrencyCreditSum = resData.baseCurrencyCreditSum;
    this.baseCurrencyDebitSum = resData.baseCurrencyDebitSum;
    this.baseCurrencyBalance = resData.baseCurrencyBalance;
  }

  onCellClick({ rowIndex, column, dataItem, isEdited }) {
    if (column && column.field != 'mark')
      this.clickedRowItem = dataItem;

    if (this.isAccess(Entities.CurrencyBook, CurrencyBookPermissions.Mark)) {

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
    if (this.selectedRows.length == 1 && (this.displayType == AccountBookDisplayType.ByRow.toString() || this.displayType == AccountBookDisplayType.VoucherSum.toString()))
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
    if (!matches(e.target, 'currency-book-detail .k-grid-content tbody *'))
      if (this.formGroup && this.formGroup.valid) {
        this.saveCurrentMark();
      }
      else {
        this.closeEditor();
      }
  }

  rowDoubleClickHandler() {
    if (this.clickedRowItem) {

      if (this.displayType == AccountBookDisplayType.ByRow.toString() || this.displayType == AccountBookDisplayType.VoucherSum.toString()) {

        if (this.isAccess(Entities.Voucher, VoucherPermissions.Edit)) {
          var voucherNo = this.clickedRowItem.voucherNo;

          if (voucherNo > 0) {
            this.voucherService.getModels(String.Format(VoucherApi.VoucherByNo, voucherNo)).subscribe(res => {

              var voucherModel = res;
              if (voucherModel) {
                const dialogRef = this.dialogService.open({
                  title: this.getText('Buttons.Edit'),
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
    if (this.displayType == AccountBookDisplayType.ByRow.toString() || this.displayType == AccountBookDisplayType.VoucherSum.toString()) {
      this.clickedRowItem = this.selectedRows[0];
      this.rowDoubleClickHandler();
    }
  }

}
