import { Component, OnInit, Input, Renderer2, ChangeDetectorRef, ViewChild, ComponentRef } from '@angular/core';
import { VoucherService, VoucherInfo, FiscalPeriodService, SettingService } from '../../service/index';
import { Voucher } from '../../model/index';
import { ToastrService } from 'ngx-toastr';
import { GridDataResult, DataStateChangeEvent, PageChangeEvent, RowArgs, SelectAllCheckboxState, GridComponent } from '@progress/kendo-angular-grid';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { String } from '../../class/source';
import { State, CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { SortDescriptor, orderBy } from '@progress/kendo-data-query';
import { DefaultComponent } from "../../class/default.component";
import { MessageType, Layout, Entities, Metadatas } from "../../../environments/environment";
import { Filter } from "../../class/filter";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { SppcLoadingService } from '../../controls/sppcLoading/index';
import { VoucherApi, VoucherReportApi } from '../../service/api/index';
import { SecureEntity } from '../../security/secureEntity';
import { VoucherPermissions } from '../../security/permissions';
import { FilterExpression } from '../../class/filterExpression';
import { DocumentStatusValue } from '../../enum/documentStatusValue';
import { Http } from '@angular/http';
import { ReportViewerComponent } from '../reportViewer/reportViewer.component';
import { VoucherReportingService } from '../../service/report/voucher-reporting.service';
import * as moment from 'jalali-moment';


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

declare var Stimulsoft: any;

@Component({
  selector: 'voucher',
  templateUrl: './voucher.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class VoucherComponent extends DefaultComponent implements OnInit {

  //#region Fields
  @ViewChild(GridComponent) grid: GridComponent;
  @ViewChild(ReportViewerComponent) viewer: ReportViewerComponent;

  public rowData: GridDataResult;
  public selectedRows: string[] = [];
  public totalRecords: number;

  //permission flag
  viewAccess: boolean;

  //for add in delete messageText
  deleteConfirm: boolean;
  deleteModelId: number;

  currentFilter: FilterExpression;
  currentOrder: string = "";
  public sort: SortDescriptor[] = [];

  showloadingMessage: boolean = true;

  editDataItem?: Voucher = undefined;
  isNew: boolean;
  errorMessage: string;
  groupDelete: boolean = false;
  
  //#endregion

  //#region Events
  ngOnInit() {
    this.viewAccess = this.isAccess(SecureEntity.Voucher, VoucherPermissions.View);
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
    this.voucherService.getById(String.Format(VoucherApi.Voucher, arg.dataItem.id)).subscribe(res => {
      this.editDataItem = res;
      this.grid.loading = false;
    })
    this.isNew = false;
    this.errorMessage = '';
  }

  public cancelHandler() {
    this.editDataItem = undefined;
    this.isNew = false;
    this.errorMessage = '';

    this.reloadGrid();
  }

  public showReport()
  {
      this.reporingService.getAll(VoucherReportApi.VoucherSumReport,
        this.currentOrder,this.currentFilter).subscribe((response: any) => {
          //c = moment.from(this.FiscalPeriodStartDate.toDateString(),'en', 'YYYY/M/D').format('YYYY/M/D');
        
          //moment.locale('en'); // default locale is en
          //var m = moment(this.FiscalPeriodStartDate.toDateString(), 'YYYY/M/D');
          var fdate = moment(this.FiscalPeriodStartDate, 'YYYY-M-D HH:mm:ss')
          .locale('fa')
          .format('YYYY/M/D');

          var tdate = moment(this.FiscalPeriodEndDate, 'YYYY-M-D HH:mm:ss')
          .locale('fa')
          .format('YYYY/M/D');


          var reportData = {rows : response.body , fromDate: fdate ,
             toDate : tdate};
          this.viewer.showVoucherReport('/assets/reports/voucher/voucher.summary.mrt',reportData);
        });
      
  }

  public saveHandler(model: Voucher) {

    this.grid.loading = true;
    if (!this.isNew) {
      this.voucherService.edit<Voucher>(String.Format(VoucherApi.Voucher, model.id), model)
        .subscribe(response => {
          this.isNew = false;
          this.editDataItem = undefined;
          this.showMessage(this.updateMsg, MessageType.Succes);
          this.reloadGrid();
        }, (error => {
          this.errorMessage = error;
          this.grid.loading = false;
        }));
    }
    else {
      model.branchId = this.BranchId;
      model.fiscalPeriodId = this.FiscalPeriodId;

      this.voucherService.insert<Voucher>(VoucherApi.EnvironmentVouchers, model)
        .subscribe((response: any) => {
          this.isNew = false;
          this.editDataItem = undefined;
          this.showMessage(this.insertMsg, MessageType.Succes);
          var insertedModel = response;
          this.reloadGrid(insertedModel);
        }, (error => {
          this.isNew = true;
          this.errorMessage = error;
          this.grid.loading = false;
        }));
    }

  }

  //public checkHandler(voucherId: number, statusId: DocumentStatusValue) {
  //  debugger;
  //  if (statusId == DocumentStatusValue.Draft) {
  //    //check
  //    this.grid.loading = true;
  //    this.voucherService.changeVoucherStatus(String.Format(VoucherApi.CheckVoucher, voucherId)).subscribe(res => {

  //      this.showMessage(this.updateMsg, MessageType.Succes);
  //      this.reloadGrid();

  //    }, (error => {
  //      this.grid.loading = false;
  //      var message = error.message ? error.message : error;
  //      this.showMessage(message, MessageType.Warning);
  //    }));

  //  }
  //  else {
  //    //uncheck
  //    this.voucherService.changeVoucherStatus(String.Format(VoucherApi.UncheckVoucher, voucherId)).subscribe(res => {

  //      this.showMessage(this.updateMsg, MessageType.Succes);
  //      this.reloadGrid();

  //    }, (error => {
  //      this.grid.loading = false;
  //      var message = error.message ? error.message : error;
  //      this.showMessage(message, MessageType.Warning);
  //    }));
  //  }


  //}

  //#endregion

  //#region Constructor
  constructor(public toastrService: ToastrService, public translate: TranslateService,
     public sppcLoading: SppcLoadingService, private cdref: ChangeDetectorRef,
    private voucherService: VoucherService, public renderer: Renderer2,
     public metadata: MetaDataService, public settingService: SettingService,
     public reporingService:VoucherReportingService) {
    super(toastrService, translate, renderer, metadata, settingService, Entities.Voucher, Metadatas.Voucher);
  }
  //#endregion

  //#region Methods
  deleteModels() {
    ////this.sppcLoading.show();
    //this.voucherService.groupDelete(VoucherApi.Vouchers, this.selectedRows).subscribe(res => {
    //    this.showMessage(this.deleteMsg, MessageType.Info);
    //    this.selectedRows = [];
    //    this.reloadGrid();
    //}, (error => {
    //    //this.sppcLoading.hide();
    //    this.showMessage(error, MessageType.Warning);
    //}));
  }

  

  reloadGridEvent() {    
    this.reloadGrid();
  }

  

  reloadGrid(insertedModel?: Voucher) {
    if (this.viewAccess) {
      this.grid.loading = true;
      var filter = this.currentFilter;
      var order = this.currentOrder;
      if (this.totalRecords == this.skip && this.totalRecords != 0) {
        this.skip = this.skip - this.pageSize;
      }

      if (insertedModel)
        this.goToLastPage(this.totalRecords);

      this.voucherService.getAll(VoucherApi.EnvironmentVouchers, this.pageIndex, this.pageSize, order, filter).subscribe((res) => {
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
        this.grid.loading = false;
      })
    }
    else {
      this.rowData = {
        data: [],
        total: 0
      }
    }

    this.cdref.detectChanges();

  }

  deleteModel(confirm: boolean) {
    if (confirm) {
      this.grid.loading = true;
      this.voucherService.delete(String.Format(VoucherApi.Voucher, this.deleteModelId)).subscribe(response => {
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
    this.editDataItem = new VoucherInfo();
    this.errorMessage = '';
  }

  //#endregion
}


