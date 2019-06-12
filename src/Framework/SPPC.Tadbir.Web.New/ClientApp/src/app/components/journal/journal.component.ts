import { Component, OnInit, OnDestroy, Renderer2, NgZone, ChangeDetectorRef, ViewChild, AfterViewInit } from '@angular/core';
import { SettingService, GridService, VoucherService, VoucherLineService } from '../../service/index';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { Layout, Metadatas, Entities, MessageType } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { DialogService } from '@progress/kendo-angular-dialog';
import { AutoGeneratedGridComponent } from '../../class/autoGeneratedGrid.component';
import { Filter } from '../../class/filter';
import { FilterExpressionOperator } from '../../class/filterExpressionOperator';
import { ReportApi } from '../../service/api/reportApi';
import { Item } from '../../model/index';
import { String } from '../../class/source';
import { JournalType, JournalDisplayType, JournalDisplayTypeResource, VoucherStatusResource, BranchScopeResource, ArticleTypesResource, ArticleTypesResourceKey } from '../../enum/journal';
import { ViewName } from '../../security/viewName';
import { ReportingService } from '../../service/report/reporting.service';
import { ViewIdentifierComponent } from '../viewIdentifier/view-identifier.component';
import { GridComponent, ColumnBase } from '@progress/kendo-angular-grid';
import { ReportViewerComponent } from '../reportViewer/reportViewer.component';
import { ReportManagementComponent } from '../reportManagement/reportManagement.component';
import { JournalApi, VoucherApi } from '../../service/api/index';
import { VoucherEditorComponent } from '../voucher/voucher-editor.component';
import { FormGroup, FormBuilder } from '@angular/forms';


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

const matches = (el, selector) => (el.matches || el.msMatchesSelector).call(el, selector);

@Component({
  selector: 'sppc-journal',
  templateUrl: './journal.component.html',
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
`],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class JournalComponent extends AutoGeneratedGridComponent implements OnInit, OnDestroy {

  displayType: Array<Item> = [
    { value: JournalDisplayTypeResource.VoucherRows, key: "1" },
    { value: JournalDisplayTypeResource.VoucherRowsWithFloatingAccounts, key: "2" },
    { value: JournalDisplayTypeResource.LedgerLevel, key: "3" },
    { value: JournalDisplayTypeResource.SubsidiaryLevel, key: "4" },
    { value: JournalDisplayTypeResource.BriefVoucher, key: "5" },
    { value: JournalDisplayTypeResource.BriefVoucherByDate, key: "6" },
    { value: JournalDisplayTypeResource.BriefVoucherByMonth, key: "7" },
  ]
  voucherStatus: Array<Item> = [
    { value: VoucherStatusResource.Committed, key: "2" },
    { value: VoucherStatusResource.Finalized, key: "3" },
    { value: VoucherStatusResource.Accepted, key: "4" },
    { value: VoucherStatusResource.Approved, key: "5" },
    { value: VoucherStatusResource.AllVouchers, key: "0" }
  ]
  branchScope: Array<Item> = [
    { value: BranchScopeResource.CurrentBranch, key: "1" },
    { value: BranchScopeResource.CurrentBranchAndSubsets, key: "2" },
  ]
  articleType: Array<Item> = [
    { value: ArticleTypesResource.AllVoucherLines, key: ArticleTypesResourceKey.AllVoucherLines },
    { value: ArticleTypesResource.MarkedVoucherLines, key: ArticleTypesResourceKey.MarkedVoucherLines },
    { value: ArticleTypesResource.UncheckedVoucherLines, key: ArticleTypesResourceKey.UncheckedVoucherLines },
  ]

  displayTypeSelected: string = '1';
  branchScopeSelected: string = '1';
  voucherStatusSelected: string = '2';
  articleTypeSelected: string = '1';
  selectedBranchSeparation: boolean = false;
  gridColumnsRow: any[] = [];

  @ViewChild(GridComponent) grid: GridComponent;
  @ViewChild(ViewIdentifierComponent) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportViewerComponent) viewer: ReportViewerComponent;
  @ViewChild(ReportManagementComponent) reportManager: ReportManagementComponent;

  fromDate: Date;
  toDate: Date;
  fromVoucher: string;
  toVoucher: string;
  journalType: string = '1';

  creditSum: number = 0;
  debitSum: number = 0;

  isDefaultBtn: boolean = true;
  tempViewId: number;

  clickedRowItem: any = undefined;

  public formGroup: FormGroup;
  private editedRowIndex: number;
  private docClickSubscription: any;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public dialogService: DialogService, public gridService: GridService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, public voucherService: VoucherService, public voucherLineService: VoucherLineService,
    public settingService: SettingService, public reporingService: ReportingService, public ngZone: NgZone, public formBuilder: FormBuilder) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, cdref, ngZone);
  }

  ngOnInit() {
    this.entityName = Entities.JournalByDateByRow;
    this.viewId = ViewName[this.entityTypeName];
    this.tempViewId = this.viewId;

    this.showloadingMessage = false;

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

  changedVoucherNum() {
    this.isDefaultBtn = false;
  }

  public onDataStateChange(): void {
    if (this.rowData && this.rowData.total > 0) {
      var fcolumns = new Array<ColumnBase>();
      this.grid.columns.forEach(function (column) {
        if (column.width == undefined)
          fcolumns.push(column);
      });
      this.fitColumns(fcolumns);
    }
  }

  changeType() {

    if (this.journalType == JournalType.ByDate) {

      switch (parseInt(this.displayTypeSelected)) {
        case JournalDisplayType.ByDateByRow:
          {
            this.tempViewId = ViewName[Entities.JournalByDateByRow];
            break;
          }
        case JournalDisplayType.ByDateByRowDetail:
          {
            this.tempViewId = ViewName[Entities.JournalByDateByRowDetail];
            break;
          }
        case JournalDisplayType.ByDateByLedger:
          {
            this.tempViewId = ViewName[Entities.JournalByDateByLedger];
            break;
          }
        case JournalDisplayType.ByDateBySubsidiary:
          {
            this.tempViewId = ViewName[Entities.JournalByDateBySubsidiary];
            break;
          }
        case JournalDisplayType.ByDateLedgerSummary:
          {
            this.tempViewId = ViewName[Entities.JournalByDateSummary];
            break;
          }
        case JournalDisplayType.ByDateLedgerSummaryByDate:
          {
            this.tempViewId = ViewName[Entities.JournalByDateSummaryByDate];
            break;
          }
        case JournalDisplayType.ByDateLedgerSummaryByMonth:
          {
            this.tempViewId = ViewName[Entities.JournalByDateSummaryByMonth];
            break;
          }
        default:
      }
    }

    if (this.journalType == JournalType.ByVoucher) {

      switch (parseInt(this.displayTypeSelected)) {
        case JournalDisplayType.ByDateByRow:
          {
            this.tempViewId = ViewName[Entities.JournalByNoByRow];
            break;
          }
        case JournalDisplayType.ByDateByRowDetail:
          {
            this.tempViewId = ViewName[Entities.JournalByNoByRowDetail];
            break;
          }
        case JournalDisplayType.ByDateByLedger:
          {
            this.tempViewId = ViewName[Entities.JournalByNoByLedger];
            break;
          }
        case JournalDisplayType.ByDateBySubsidiary:
          {
            this.tempViewId = ViewName[Entities.JournalByNoBySubsidiary];
            break;
          }
        case JournalDisplayType.ByDateLedgerSummary:
          {
            this.tempViewId = ViewName[Entities.JournalByNoSummary];
            break;
          }

        default:
      }
    }

    this.changeParam();
  }

  getReportData() {
    if (this.journalType) {

      this.changeParam();

      this.defaultFilter = [];
      if (this.voucherStatusSelected != "0") {
        this.defaultFilter.push(new Filter("VoucherStatusId", this.voucherStatusSelected, "== {0}", "System.Int32"));
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

      if (this.journalType == JournalType.ByDate && this.fromDate && this.toDate) {
        switch (parseInt(this.displayTypeSelected)) {
          case JournalDisplayType.ByDateByRow:
            {
              this.entityName = Entities.JournalByDateByRow;
              this.getDataUrl = this.selectedBranchSeparation ? JournalApi.JournalByDateByRowByBranch : JournalApi.JournalByDateByRow;

              break;
            }
          case JournalDisplayType.ByDateByRowDetail:
            {
              this.entityName = Entities.JournalByDateByRowDetail;
              this.getDataUrl = this.selectedBranchSeparation ? JournalApi.JournalByDateByRowDetailByBranch : JournalApi.JournalByDateByRowDetail;
              break;
            }
          case JournalDisplayType.ByDateByLedger:
            {
              this.entityName = Entities.JournalByDateByLedger;
              this.getDataUrl = this.selectedBranchSeparation ? JournalApi.JournalByDateByLedgerByBranch : JournalApi.JournalByDateByLedger;
              break;
            }
          case JournalDisplayType.ByDateBySubsidiary:
            {
              this.entityName = Entities.JournalByDateBySubsidiary;
              this.getDataUrl = this.selectedBranchSeparation ? JournalApi.JournalByDateBySubsidiaryByBranch : JournalApi.JournalByDateBySubsidiary;
              break;
            }
          case JournalDisplayType.ByDateLedgerSummary:
            {
              this.entityName = Entities.JournalByDateSummary;
              this.getDataUrl = this.selectedBranchSeparation ? JournalApi.JournalByDateLedgerSummaryByBranch : JournalApi.JournalByDateLedgerSummary;
              break;
            }
          case JournalDisplayType.ByDateLedgerSummaryByDate:
            {
              this.entityName = Entities.JournalByDateSummaryByDate;
              this.getDataUrl = this.selectedBranchSeparation ? JournalApi.JournalByDateLedgerSummaryByDateByBranch : JournalApi.JournalByDateLedgerSummaryByDate;
              break;
            }
          case JournalDisplayType.ByDateLedgerSummaryByMonth:
            {
              this.entityName = Entities.JournalByDateSummaryByMonth;
              this.getDataUrl = this.selectedBranchSeparation ? JournalApi.JournalByDateMonthlyLedgerSummaryByBranch : JournalApi.JournalByDateMonthlyLedgerSummary;
              break;
            }
          default:
        }

        this.getDataUrl += "?from=" + this.fromDate + "&to=" + this.toDate;
        this.reloadGrid();
      }

      if (this.journalType == JournalType.ByVoucher) {

        if (this.fromVoucher && this.toVoucher) {
          if (parseInt(this.fromVoucher) <= parseInt(this.toVoucher)) {
            switch (parseInt(this.displayTypeSelected)) {
              case JournalDisplayType.ByDateByRow:
                {
                  this.entityName = Entities.JournalByNoByRow;
                  this.getDataUrl = this.selectedBranchSeparation ? JournalApi.JournalByNoByRowByBranch : JournalApi.JournalByNoByRow;
                  break;
                }
              case JournalDisplayType.ByDateByRowDetail:
                {
                  this.entityName = Entities.JournalByNoByRowDetail;
                  this.getDataUrl = this.selectedBranchSeparation ? JournalApi.JournalByNoByRowDetailByBranch : JournalApi.JournalByNoByRowDetail;
                  break;
                }
              case JournalDisplayType.ByDateByLedger:
                {
                  this.entityName = Entities.JournalByNoByLedger;
                  this.getDataUrl = this.selectedBranchSeparation ? JournalApi.JournalByNoByLedgerByBranch : JournalApi.JournalByNoByLedger;
                  break;
                }
              case JournalDisplayType.ByDateBySubsidiary:
                {
                  this.entityName = Entities.JournalByNoBySubsidiary;
                  this.getDataUrl = this.selectedBranchSeparation ? JournalApi.JournalByNoBySubsidiaryByBranch : JournalApi.JournalByNoBySubsidiary;
                  break;
                }
              case JournalDisplayType.ByDateLedgerSummary:
                {
                  this.entityName = Entities.JournalByNoSummary;
                  this.getDataUrl = this.selectedBranchSeparation ? JournalApi.JournalByNoLedgerSummaryByBranch : JournalApi.JournalByNoLedgerSummary;
                  break;
                }
              case JournalDisplayType.ByDateLedgerSummaryByDate:
                {
                  this.showMessage(String.Format(this.getText('Journal.ChooseByDate'), this.getText(JournalDisplayTypeResource.BriefVoucherByDate)), MessageType.Warning);
                  this.getDataUrl = undefined;
                  break;
                }
              case JournalDisplayType.ByDateLedgerSummaryByMonth:
                {
                  this.showMessage(String.Format(this.getText('Journal.ChooseByDate'), this.getText(JournalDisplayTypeResource.BriefVoucherByMonth)), MessageType.Warning);
                  this.getDataUrl = undefined;
                  break;
                }
              default:
            }

            if (parseInt(this.displayTypeSelected) != JournalDisplayType.ByDateLedgerSummaryByDate && parseInt(this.displayTypeSelected) != JournalDisplayType.ByDateLedgerSummaryByMonth) {
              this.getDataUrl += "?from=" + this.fromVoucher + "&to=" + this.toVoucher;
              this.reloadGrid();
            }
          }
          else {
            this.showMessage(this.getText('Journal.FinalVoucherShouldLarger'), MessageType.Warning);
          }
        }
        else {
          this.showMessage(this.getText('Journal.RequiredVoucherNumber'), MessageType.Warning);
        }
      }
    }

  }

  reloadGrid(insertedModel?: any) {

    if (this.getDataUrl) {
      //if (this.viewAccess) {
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
      //}
      //else {
      //  this.rowData = {
      //    data: [],
      //    total: 0
      //  }
      //}

    }
    this.cdref.detectChanges();
  }


  public showReport() {
    this.reportManager.DecisionMakingForShowReport();
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

  changeBranchSeparation() {
    if (!this.selectedBranchSeparation) {
      this.gridColumnsRow = this.gridColumns.filter(f => f.name != "BranchName");
    }
    else {
      this.gridColumnsRow = this.gridColumns;
    }
    this.changeParam();
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

    if (isEdited || (this.formGroup && !this.formGroup.valid) || (column.field != 'mark' && this.editedRowIndex == rowIndex)) {
      return;
    }

    this.saveCurrentMark();
    this.formGroup = this.createFormGroup(dataItem);
    this.editedRowIndex = rowIndex;
    this.grid.editRow(this.editedRowIndex, this.formGroup);
  }

  closeEditor(): void {
    this.grid.closeRow(this.editedRowIndex);
    this.editedRowIndex = undefined;
    this.formGroup = undefined;
  }

  isEnableVoucherInfoBtn = () => {
    if (this.selectedRows.length == 1 && this.displayTypeSelected != JournalDisplayType.ByDateLedgerSummary.toString() && this.displayTypeSelected != JournalDisplayType.ByDateLedgerSummaryByDate.toString() &&
      this.displayTypeSelected != JournalDisplayType.ByDateLedgerSummaryByMonth.toString())
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
      if (dataModel.mark.replace(/\s/g, "").length == 0)
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
  }

  rowDoubleClickHandler() {
    if (this.clickedRowItem) {

      if (this.displayTypeSelected != JournalDisplayType.ByDateLedgerSummary.toString() && this.displayTypeSelected != JournalDisplayType.ByDateLedgerSummaryByDate.toString() &&
        this.displayTypeSelected != JournalDisplayType.ByDateLedgerSummaryByMonth.toString()) {

        var voucherNo = this.clickedRowItem.voucherNo;

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

  }

  onVoucherHandler() {
    if (this.displayTypeSelected != JournalDisplayType.ByDateLedgerSummary.toString() && this.displayTypeSelected != JournalDisplayType.ByDateLedgerSummaryByDate.toString() &&
      this.displayTypeSelected != JournalDisplayType.ByDateLedgerSummaryByMonth.toString()) {
      this.clickedRowItem = this.selectedRows[0];
      this.rowDoubleClickHandler();
    }
  }
}


