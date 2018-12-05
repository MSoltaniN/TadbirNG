import { Component, OnInit, Input, Renderer2, ViewChild } from '@angular/core';
import { VoucherLineInfo, VoucherLineService, FiscalPeriodService, SettingService } from '../../service/index';
import { VoucherLine, Voucher } from '../../model/index';
import { ToastrService } from 'ngx-toastr';
import { GridDataResult, DataStateChangeEvent, PageChangeEvent, RowArgs, SelectAllCheckboxState, GridComponent } from '@progress/kendo-angular-grid';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { String } from '../../class/source';
import { State, CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { SortDescriptor, orderBy } from '@progress/kendo-data-query';
import { DefaultComponent } from "../../class/default.component";
import { MessageType, Entities, Metadatas } from "../../../environments/environment";
import { Filter } from "../../class/filter";
import { MetaDataService } from '../../service/metadata/metadata.service';
import { SppcLoadingService } from '../../controls/sppcLoading/index';
import { VoucherApi, VoucherReportApi } from '../../service/api/index';
import { FilterExpression } from '../../class/filterExpression';
import { DocumentStatusValue } from '../../enum/documentStatusValue';
import { VoucherReportingService } from '../../service/report/voucher-reporting.service';
import { ReportViewerComponent } from '../reportViewer/reportViewer.component';
import * as moment from 'jalali-moment';


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
`]
})


export class VoucherLineComponent extends DefaultComponent implements OnInit {

  //#region Fields
  @ViewChild(GridComponent) grid: GridComponent;
  @ViewChild(ReportViewerComponent) viewer: ReportViewerComponent;

  public rowData: GridDataResult;
  public selectedRows: string[] = [];
  public totalRecords: number;

  public debitSum: number;
  public creditSum: number;
  public balance: number;

  //for add in delete messageText
  deleteConfirm: boolean;
  deleteModelId: number;

  currentFilter: FilterExpression;
  currentOrder: string = "";
  public sort: SortDescriptor[] = [];

  showloadingMessage: boolean = true;

  editDataItem?: VoucherLine = undefined;

  isNew: boolean;
  isNewBalance: boolean;
  errorMessage: string;
  groupDelete: boolean = false;

  @Input() voucherId: number;
  @Input() documentStatus: number;
  voucherModel: Voucher;
  documentStatusValue: any;
  //#endregion

  //#region Events
  ngOnInit() {
    this.documentStatusValue = DocumentStatusValue;
    this.getVoucher();
    var test = this.voucherId;
    this.reloadGrid();
  }

  selectionKey(context: RowArgs): string {
    if (context.dataItem == undefined) return "";
    return context.dataItem.id + " " + context.index;
  }

  onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
    if (this.selectedRows.length > 1)
      this.groupDelete = true;
    else
      this.groupDelete = false;
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
    if (sort)
      this.currentOrder = sort[0].field + " " + sort[0].dir;
    this.reloadGrid();
  }

  removeHandler(arg: any) {
    this.prepareDeleteConfirm(arg.dataItem.name);
    this.deleteModelId = arg.dataItem.id;
    this.deleteConfirm = true;
  }

  pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
    this.reloadGrid();
  }

  public editHandler(arg: any) {
    this.grid.loading = true;
    this.voucherLineService.getById(String.Format(VoucherApi.VoucherArticle, arg.dataItem.id)).subscribe(res => {
      this.editDataItem = res;
      this.grid.loading = false;
    })
    this.isNew = false;
    this.errorMessage = '';
  }

  public cancelHandler() {
    this.editDataItem = undefined;
    this.isNew = false;
    this.isNewBalance = false;
    this.errorMessage = '';
  }

  public saveHandler(viewModel: any) {
    this.isNewBalance = false;
    //this.balance = this.debitSum - this.creditSum;
    var model = viewModel.model;
    var isOpen = viewModel.isOpen;

    model.branchId = this.voucherModel.branchId;
    model.fiscalPeriodId = this.voucherModel.fiscalPeriodId;
    model.voucherId = this.voucherModel.id;

    this.grid.loading = true;
    if (!this.isNew) {
      this.isNew = false;
      this.voucherLineService.edit<VoucherLine>(String.Format(VoucherApi.VoucherArticle, model.id), model)
        .subscribe(response => {
          this.editDataItem = undefined;
          this.showMessage(this.updateMsg, MessageType.Succes);
          this.reloadGrid();

          if (isOpen) {
            setTimeout(() => {
              this.addNew();
            });
          }
        }, (error => {
          //this.editDataItem = voucherLine;
          this.errorMessage = error;
          this.grid.loading = false;
        }));
    }
    else {     
      this.voucherLineService.insert<VoucherLine>(String.Format(VoucherApi.VoucherArticles, this.voucherId), model)
        .subscribe((response: any) => {
          this.isNew = false;
          this.editDataItem = undefined;
          this.showMessage(this.insertMsg, MessageType.Succes);
          var insertedModel = response;
          this.reloadGrid(insertedModel);

          if (isOpen) {
            setTimeout(() => { 
              this.addNew();
            });           
          }
        }, (error => {
          this.isNew = true;
          this.errorMessage = error;
          this.grid.loading = false;
        }));
    }
  }

  //#endregion

  //#region Constructor
  constructor(public toastrService: ToastrService, 
    public translate: TranslateService,
     public sppcLoading: SppcLoadingService,
    private voucherLineService: VoucherLineService,
     public renderer: Renderer2,
     public metadata: MetaDataService,
      public settingService: SettingService,
      public reporingService:VoucherReportingService) {
    super(toastrService, translate, renderer, metadata, settingService, Entities.VoucherLine, Metadatas.VoucherArticles);
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

  reloadGridEvent() {
    this.reloadGrid();
  }

  reloadGrid(insertedModel?: VoucherLine) {
    this.grid.loading = true;
    var filter = this.currentFilter;
    var order = this.currentOrder;
    if (this.totalRecords == this.skip && this.totalRecords != 0) {
      this.skip = this.skip - this.pageSize;
    }

    if (insertedModel)
      this.goToLastPage(this.totalRecords);
    this.voucherLineService.getAll(String.Format(VoucherApi.VoucherArticles, this.voucherId), this.pageIndex, this.pageSize, order, filter).subscribe((res) => {
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

      this.debitSum = res.debitSum;
      this.creditSum = res.creditSum;

      this.balance = this.debitSum - this.creditSum;

      this.grid.loading = false;
    })
  }

  public showReport()
  {
      var url = String.Format(VoucherReportApi.VoucherStdFormReport, this.voucherId);

      this.reporingService.getAll(url,
        this.currentOrder,this.currentFilter).subscribe((response: any) => {

           const m = moment();
           var dateStr : string;
           m.locale('fa'); 
           if (m.isValid())
              dateStr = m.format('jYYYY/jMM/jDD');  

          var reportData = {rows : response.body , currentDate: dateStr};
          this.viewer.showVoucherStdFormReport('reports/voucher/voucher.stdform.mrt',reportData);
        });
      
  }

  getVoucher() {
    this.voucherLineService.getById(String.Format(VoucherApi.Voucher, this.voucherId)).subscribe(res => {

      this.voucherModel = res;

    })
  }

  deleteModel(confirm: boolean) {
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

  public addNew() {
    this.isNew = true;
    this.errorMessage = '';
    this.editDataItem = new VoucherLineInfo();
  }

  public addNewWithBalance() {
    this.isNewBalance = true;
    this.addNew();
  }
  //#endregion
}


