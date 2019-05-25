import { Component, OnInit, Input, Renderer2, ViewChild, Output, EventEmitter, ChangeDetectorRef, NgZone } from '@angular/core';
import { VoucherLineInfo, VoucherLineService, SettingService, GridService } from '../../service/index';
import { VoucherLine, Voucher } from '../../model/index';
import { ToastrService } from 'ngx-toastr';
import { GridDataResult, PageChangeEvent, RowArgs, SelectAllCheckboxState, GridComponent } from '@progress/kendo-angular-grid';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { String } from '../../class/source';
import { CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { SortDescriptor } from '@progress/kendo-data-query';
import { MessageType, Entities, Metadatas } from "../../../environments/environment";
import { MetaDataService } from '../../service/metadata/metadata.service';
import { VoucherApi } from '../../service/api/index';
import { FilterExpression } from '../../class/filterExpression';
import { DocumentStatusValue } from '../../enum/documentStatusValue';
import { ReportViewerComponent } from '../reportViewer/reportViewer.component';
import * as moment from 'jalali-moment';
import { ReportingService } from '../../service/report/reporting.service';
import { DialogService, DialogRef, DialogCloseResult } from '@progress/kendo-angular-dialog';
import { VoucherLineFormComponent } from '../../components/voucherLine/voucherLine-form.component';
import { ViewIdentifierComponent } from '../viewIdentifier/view-identifier.component';
import { ReportManagementComponent } from '../reportManagement/reportManagement.component';
import { AutoGeneratedGridComponent } from '../../class/autoGeneratedGrid.component';
import { ViewName } from '../../security/viewName';



@Component({
  selector: 'voucherLine',
  templateUrl: './voucherLine.component.html',
  styles: [`/deep/ .panel-primary { border-color: #989898; }
.voucher-balance{text-align: center; display: block; }
.voucher-balance > .color-red { color: red; } .voucher-balance > .color-green { color: green; }
.voucher-balance > .balance-value { direction: ltr; display: inline-block; }
.detail-info { margin:5px 0; } .detail-info > span { padding-left: 15px; }
.nowrap { white-space: nowrap; overflow: hidden; text-overflow: ellipsis; width: 450px; display: block; }
/deep/.k-footer-template { background-color: #b3b3b3; }


input[type=text],textarea,.article-description input[type=text] { width: 100%; }
.article-status-item { display: inline; margin: 0 10px; } .article-status-item  input[type=text] { width:200px; }
`]
})


export class VoucherLineComponent extends AutoGeneratedGridComponent implements OnInit {

  //#region Fields
  @ViewChild(GridComponent) grid: GridComponent;
  @ViewChild(ViewIdentifierComponent) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportViewerComponent) viewer: ReportViewerComponent;
  @ViewChild(ReportManagementComponent) reportManager: ReportManagementComponent;

  public rowData: GridDataResult;
  public selectedRows: any[] = [];
  public totalRecords: number;

  public debitSum: number;
  public creditSum: number;
  public balance: number;

  //for add in delete messageText
  deleteConfirm: boolean;
  deleteModelId: number;

  currentFilter: FilterExpression;

  showloadingMessage: boolean = true;

  editDataItem?: VoucherLine = undefined;
  selectedModel: VoucherLine;

  isNewBalance: boolean;
  groupDelete: boolean = false;
  voucherId: number;

  isIssued: boolean = false;
  isConfirmed: boolean = false;
  isApproved: boolean = false;
  isDisplayCurrency: boolean = false;


  @Input() set voucherID(id: number) {
    this.voucherId = id;
    this.getVoucher();
    this.reloadGrid();
  }

  @Input() documentStatus: number;

  @Output() setFocus: EventEmitter<any> = new EventEmitter();
  @Output() isBalanced: EventEmitter<any> = new EventEmitter();

  voucherModel: Voucher;
  documentStatusValue: any;

  private dialogRef: DialogRef;
  private dialogModel: any;
  //#endregion

  //#region Events
  ngOnInit() {
    this.entityTypeName = Entities.VoucherLine;
    this.metadataType = Metadatas.VoucherArticles;
    this.viewId = ViewName[this.entityTypeName];

    this.documentStatusValue = DocumentStatusValue;
    //this.getVoucher();
    //this.reloadGrid();
  }

  /**
   * باز کردن و مقداردهی اولیه به فرم ویرایشگر
   */
  openEditorDialog(isNew: boolean) {

    this.dialogRef = this.dialogService.open({
      title: this.getText(isNew ? 'Buttons.New' : 'Buttons.Edit'),
      content: VoucherLineFormComponent,
    });

    this.dialogRef.dialog.location.nativeElement.classList.add('dialog-style');

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.model = this.editDataItem;
    this.dialogModel.errorMessage = undefined;
    this.dialogModel.isNew = isNew;
    this.dialogModel.isNewBalance = this.isNewBalance;
    this.dialogModel.balance = this.balance;
    this.dialogModel.isDisplayCurrency = this.isDisplayCurrency;

    this.dialogRef.content.instance.save.subscribe((res) => {
      this.saveHandler(res, isNew);
    });

    const closeForm = this.dialogRef.content.instance.cancel.subscribe((res) => {
      this.dialogRef.close();

      this.dialogModel.errorMessage = undefined;
      this.dialogModel.model = undefined;

      this.setFocus.emit();
    });


    this.dialogRef.result.subscribe((result) => {
      if (result instanceof DialogCloseResult) {
        this.setFocus.emit();
      }
    });

    this.dialogRef.content.instance.setFocus.subscribe((res) => {
      debugger;
      //this.dialogRef.dialog.instance.focus();
    });

  }

  selectionKey(context: RowArgs): any {
    if (context.dataItem == undefined) return "";
    //return context.dataItem.id + " " + context.index;
    return context.dataItem;
  }

  onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
    if (this.selectedRows.length > 1)
      this.groupDelete = true;
    else
      this.groupDelete = false;

    this.selectedModel = undefined;
    if (this.selectedRows.length == 1) {
      this.selectedModel = this.selectedRows[0];
    }
  }

  filterChange(filter: CompositeFilterDescriptor): void {
    var isReload: boolean = false;
    if (this.currentFilter && this.currentFilter.children.length > filter.filters.length)
      isReload = true;

    this.currentFilter = this.getFilters(filter);
    if (isReload) {
      this.reloadGrid();
    }
  }

  public sortChange(sort: SortDescriptor[]): void {

    this.sort = sort.filter(f => f.dir != undefined);

    this.reloadGrid();
  }

  removeHandler() {
    this.deleteConfirm = true;
    if (!this.groupDelete) {
      var record = this.selectedRows[0];

      this.prepareDeleteConfirm("");
      this.deleteModelId = record.id;
    }
    else {
      this.prepareDeleteConfirm(this.getText('Messages.SelectedItems'));
    }
  }

  pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
    this.reloadGrid();
  }

  public editHandler() {
    var record = this.selectedRows[0];
    this.grid.loading = true;
    this.voucherLineService.getById(String.Format(VoucherApi.VoucherArticle, record.id)).subscribe(res => {

      this.editDataItem = res;
      this.openEditorDialog(false);

      this.grid.loading = false;
    })
  }

  public saveHandler(viewModel: any, isNew: boolean) {
    this.isNewBalance = false;
    //this.balance = this.debitSum - this.creditSum;
    var model = viewModel.model;
    var isOpen = viewModel.isOpen;

    model.branchId = this.BranchId;
    model.fiscalPeriodId = this.voucherModel.fiscalPeriodId;
    model.voucherId = this.voucherModel.id;

    this.grid.loading = true;
    if (!isNew) {
      this.voucherLineService.edit<VoucherLine>(String.Format(VoucherApi.VoucherArticle, model.id), model)
        .subscribe(response => {
          this.editDataItem = undefined;
          this.showMessage(this.updateMsg, MessageType.Succes);

          this.dialogRef.close();
          this.dialogModel.parent = undefined;
          this.dialogModel.errorMessage = undefined;
          this.dialogModel.model = undefined;

          this.reloadGrid();

          if (isOpen) {
            setTimeout(() => {
              this.addNew();
            });
          }
        }, (error => {
          this.editDataItem = model;
          this.dialogModel.errorMessage = error;
          this.grid.loading = false;
        }));
    }
    else {
      this.voucherLineService.insert<VoucherLine>(String.Format(VoucherApi.VoucherArticles, this.voucherId), model)
        .subscribe((response: any) => {
          this.editDataItem = undefined;
          this.showMessage(this.insertMsg, MessageType.Succes);
          var insertedModel = response;

          this.dialogRef.close();
          this.dialogModel.parent = undefined;
          this.dialogModel.errorMessage = undefined;
          this.dialogModel.model = undefined;

          this.reloadGrid(insertedModel);

          if (isOpen) {
            setTimeout(() => {
              this.addNew();
            });
          }
        }, (error => {
          this.dialogModel.errorMessage = error;
          this.grid.loading = false;
        }));
    }
  }

  //#endregion

  //#region Constructor

  constructor(public toastrService: ToastrService, public translate: TranslateService, public dialogService: DialogService, public gridService: GridService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, public voucherLineService: VoucherLineService,
    public settingService: SettingService, public reporingService: ReportingService, public ngZone: NgZone) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, cdref, ngZone);
  }

  //#endregion

  //#region Methods
  deleteModels() {
    //    this.transactionLineService.deleteTransactions(this.selectedRows).subscribe(res => {
    //        this.showMessage(this.deleteMsg, MessageType.Info);
    //        this.selectedRows = [];
    //        this.reloadGrid();
    //    }, (error => {
    //        this.showMessage(error, MessageType.Warning);
    //    }));
  }

  deleteModelOld(confirm: boolean) {
    if (confirm) {
      this.grid.loading = true;
      this.voucherLineService.delete(String.Format(VoucherApi.VoucherArticle, this.deleteModelId)).subscribe(response => {
        this.deleteModelId = 0;
        this.showMessage(this.deleteMsg, MessageType.Info);
        if (this.rowData.data.length == 1 && this.pageIndex > 1)
          this.pageIndex = ((this.pageIndex - 1) * this.pageSize) - this.pageSize;

        this.reloadGrid();
      }, (error => {
        this.grid.loading = false;
        var message = error.message ? error.message : error;
        this.showMessage(message, MessageType.Warning);
      }));
    }

    //hide confirm dialog
    this.deleteConfirm = false;
  }


  deleteModel(confirm: boolean) {
    if (confirm) {
      //حذف گروهی از گرید
      if (this.groupDelete) {
      }
      else {
        //حذف یک سطر از گرید
        this.grid.loading = true;
        this.voucherLineService.delete(String.Format(VoucherApi.VoucherArticle, this.deleteModelId)).subscribe(response => {
          this.deleteModelId = 0;
          this.showMessage(this.deleteMsg, MessageType.Info);
          if (this.rowData.data.length == 1 && this.pageIndex > 1)
            this.pageIndex = ((this.pageIndex - 1) * this.pageSize) - this.pageSize;

          this.reloadGrid();
        }, (error => {
          this.grid.loading = false;
          var message = error.message ? error.message : error;
          this.showMessage(message, MessageType.Warning);
        }));
      }
    }

    //hide confirm dialog
    this.deleteConfirm = false;
  }


  reloadGridEvent() {
    this.reloadGrid();
  }

  reloadGrid(insertedModel?: VoucherLine) {
    this.grid.loading = true;
    var filter = this.currentFilter;
    if (this.totalRecords == this.skip && this.totalRecords != 0) {
      this.skip = this.skip - this.pageSize;
    }

    if (insertedModel)
      this.goToLastPage(this.totalRecords);
    this.voucherLineService.getAll(String.Format(VoucherApi.VoucherArticles, this.voucherId), this.pageIndex, this.pageSize, this.sort, filter).subscribe((res) => {
      var resData = res.body;
      this.properties = resData.properties;
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
        data: resData,
        total: totalCount
      }
      this.showloadingMessage = !(resData.length == 0);
      this.totalRecords = totalCount;
    })

    this.voucherLineService.getVoucherInfo(this.voucherId).subscribe(res => {

      this.voucherModel = res;

      this.debitSum = res.debitSum;
      this.creditSum = res.creditSum;

      this.balance = this.debitSum - this.creditSum;

      this.emitBalanced();

      this.grid.loading = false;
    })
  }

  //report methods
  public showReport() {
    this.reportManager.DecisionMakingForShowReport();
  }

  onReportDataBind(arg: any) {
    var reportData = arg.data;
    var report = arg.report;

    //set data in report
    report.regData("Vouchers", "VouchersStdForm", reportData.rows.lines);

    moment.locale('en');
    var momentDate = moment(new Date()).locale('fa').format("YYYY/MM/DD");

    //set parameters in report
    report.dictionary.variables.getByName("currentDate").valueObject = momentDate;
    report.dictionary.variables.getByName("date").valueObject = reportData.rows.date;
    report.dictionary.variables.getByName("id").valueObject = reportData.rows.id;
    report.dictionary.variables.getByName("description").valueObject = reportData.rows.description;
    report.dictionary.variables.getByName("no").valueObject = reportData.rows.no;

    arg.viewer.showDesginedReportViewer(arg.data, report);
  }
  //report methods

  getVoucher() {
    this.voucherLineService.getById(String.Format(VoucherApi.Voucher, this.voucherId)).subscribe(res => {
      this.voucherModel = res;
      this.isIssued = this.voucherModel.issuedById ? true : false;
      this.isApproved = this.voucherModel.approvedById ? true : false;
      this.isConfirmed = this.voucherModel.confirmedById ? true : false;
    })
  }
  

  addNew() {
    this.editDataItem = new VoucherLineInfo();

    this.openEditorDialog(true);
  }

  addNewWithBalance() {
    this.isNewBalance = true;
    this.addNew();
  }

  emitBalanced() {
    if (this.voucherModel.isBalanced) 
      this.isBalanced.emit(true);
    else
      this.isBalanced.emit(false);
  }

  changeDisplayCurrency() {
    debugger;
  }
  //#endregion
}


