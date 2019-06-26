import { Component, OnInit, Input, Renderer2, ChangeDetectorRef, ViewChild } from '@angular/core';
import { VoucherService, VoucherInfo, SettingService } from '../../service/index';
import { Voucher } from '../../model/index';
import { ToastrService } from 'ngx-toastr';
import { GridDataResult, PageChangeEvent, RowArgs, SelectAllCheckboxState, GridComponent } from '@progress/kendo-angular-grid';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { String } from '../../class/source';
import { CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { SortDescriptor } from '@progress/kendo-data-query';
import { DefaultComponent } from "../../class/default.component";
import { MessageType, Layout, Entities, Metadatas } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { SppcLoadingService } from '../../controls/sppcLoading/index';
import { VoucherApi } from '../../service/api/index';
import { SecureEntity } from '../../security/secureEntity';
import { VoucherPermissions } from '../../security/permissions';
import { FilterExpression } from '../../class/filterExpression';
import { ReportViewerComponent } from '../reportViewer/reportViewer.component';
import { ReportingService } from '../../service/report/reporting.service';
import { ReportManagementComponent } from '../reportManagement/reportManagement.component';
import { DialogService, DialogRef, DialogCloseResult } from '@progress/kendo-angular-dialog';
import { ViewIdentifierComponent } from '../viewIdentifier/view-identifier.component';
import { Filter } from '../../class/filter';
import { FilterExpressionOperator } from '../../class/filterExpressionOperator';
import { VoucherEditorComponent } from './voucher-editor.component';
import { ViewName } from '../../security/viewName';
import { DocumentStatusValue } from '../../enum/documentStatusValue';



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

  @ViewChild(GridComponent) grid: GridComponent;
  @ViewChild(ReportViewerComponent) viewer: ReportViewerComponent;
  @ViewChild(ViewIdentifierComponent) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportManagementComponent) reportManager: ReportManagementComponent;
  

  public rowData: GridDataResult;
  public selectedRows: Voucher[] = [];
  public totalRecords: number;
  firstLoad: boolean = true;
  dateFilter: Array<Filter> = [];
  metadataType : string = Metadatas.Voucher;
  //permission flag
  viewAccess: boolean;

  //for add in delete messageText
  deleteConfirm: boolean;
  deleteModelId: number;

  startDate: any;
  endDate: any;

  currentFilter: FilterExpression;

  showloadingMessage: boolean = true;

  clickedRowItem: Voucher = undefined;
  editDataItem?: Voucher = undefined;

  groupOperation: boolean = false;


  private dialogRef: DialogRef;
  private dialogModel: any;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public dialogService: DialogService,
    public sppcLoading: SppcLoadingService, private cdref: ChangeDetectorRef,
    private voucherService: VoucherService, public renderer: Renderer2,
    public metadata: MetaDataService, public settingService: SettingService,
    public reporingService: ReportingService) {
    super(toastrService, translate, renderer, metadata, settingService, Entities.Voucher, ViewName.Voucher);
  }

  ngOnInit() {
    this.viewAccess = this.isAccess(SecureEntity.Voucher, VoucherPermissions.View);
    //this.reloadGrid();
  }

  /**
   * باز کردن و مقداردهی اولیه به فرم ویرایشگر
   */
  openEditorDialog(isNew: boolean) {

    this.dialogRef = this.dialogService.open({
      title: this.getText(isNew ? 'Buttons.New' : 'Buttons.Edit'),
      content: VoucherEditorComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.voucherItem = this.editDataItem;
    this.editDataItem = undefined;

    this.dialogRef.result.subscribe((result) => {
      if (result instanceof DialogCloseResult) {
        this.reloadGrid();
      }
    });

  }


  onCellClick(e) {
    this.clickedRowItem = e.dataItem;
  }

  public rowDoubleClickHandler() {
    if (this.clickedRowItem) {
      this.grid.loading = true;
      this.voucherService.getById(String.Format(VoucherApi.Voucher, this.clickedRowItem.id)).subscribe(res => {
        this.editDataItem = res;

        this.openEditorDialog(false);

        this.grid.loading = false;
      })
    }
    
  }

  selectionKey(context: RowArgs): string {
    if (context.dataItem == undefined) return "";
    return context.dataItem;
  }

  onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
    if (this.selectedRows.length > 1)
      this.groupOperation = true;
    else
      this.groupOperation = false;
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

  removeHandler(arg: any) {
    this.deleteConfirm = true;
    if (!this.groupOperation) {
      var recordId = this.selectedRows[0].id;
      var record = this.rowData.data.find(f => f.id == recordId);

      this.prepareDeleteConfirm(record.name);
      this.deleteModelId = recordId;
    }
  }

  pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
    this.reloadGrid();
  }

  public editHandler(arg: any) {
    var recordId = this.selectedRows[0].id;
    this.grid.loading = true;
    this.voucherService.getById(String.Format(VoucherApi.Voucher, recordId)).subscribe(res => {
      this.editDataItem = res;
      this.openEditorDialog(false);

      this.grid.loading = false;
    })
  }

  public showReport() {
    /*
    var url = String.Format(ReportApi.DefaultSystemReport, this.viewer.baseId);

    this.reporingService.getAll(url).subscribe((res: Response) => {    

      var report: Report = <any>res.body;
      var serviceUrl = environment.BaseUrl + "/" + report.serviceUrl;    

      this.reporingService.getAll(serviceUrl,
        this.sort, this.currentFilter).subscribe((response: any) => {

          var fdate = moment(this.FiscalPeriodStartDate, 'YYYY-M-D HH:mm:ss')
            .locale(this.CurrentLanguage)
            .format('YYYY/M/D');

          var tdate = moment(this.FiscalPeriodEndDate, 'YYYY-M-D HH:mm:ss')
            .locale(this.CurrentLanguage)
            .format('YYYY/M/D');


          var reportData = {
            rows: response.body, fromDate: fdate,
            toDate: tdate
          };
          //'/assets/reports/voucher/voucher.summary.mrt'
          this.viewer.showVoucherReport(report, reportData);

        });
    });

    */

    this.reportManager.DecisionMakingForShowReport();
  }

  dateValueChange(event: any) {
    this.startDate = event.fromDate;
    this.endDate = event.toDate;

    this.dateFilter = [];
    this.dateFilter.push(new Filter("Date", this.startDate, " >= {0} ", "System.DateTime"),
      new Filter("Date", this.endDate, " <= {0} ", "System.DateTime"));    

    if (this.firstLoad && this.startDate && this.endDate) {
      this.reloadGrid();
    }
  }

  getVouchers() {
    this.pageIndex = 0;
    this.reloadGrid();
  }


  gridColumnResize(event: any) {

  }

  showQReport() {
    
    // this.reportManager.showReportManager(this.grid,this.viewIdentity,
    //   this.currentFilter,this.sort,this.rowData.data[0]);
  }

  public showReportManagement() {
    //var id = this.viewIdentity.ViewID;
    //var params = null;
    //if (this.viewIdentity.params.length > 0)
      //params = this.viewIdentity.params.toArray();
    //this.reportManager.showDialog(id, params, this.currentFilter, this.sort,);
  }

  reloadGridEvent() {
    this.reloadGrid();
  }

  reloadGrid(insertedModel?: Voucher) {
    this.firstLoad = false;


    if (this.viewAccess) {
      this.grid.loading = true;

      var filter = this.currentFilter;
      this.dateFilter.forEach(item => {
        filter = this.addFilterToFilterExpression(filter,
          item, FilterExpressionOperator.And);
      })

      if (this.totalRecords == this.skip && this.totalRecords != 0) {
        this.skip = this.skip - this.pageSize;
      }

      if (insertedModel)
        this.goToLastPage(this.totalRecords);

      this.voucherService.getAll(VoucherApi.EnvironmentVouchers, this.pageIndex, this.pageSize, this.sort, filter).subscribe((res) => {
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
      if (this.groupOperation) {
        //حذف گروهی
      }
      else {

        this.grid.loading = true;
        this.voucherService.delete(String.Format(VoucherApi.Voucher, this.deleteModelId)).subscribe(response => {
          this.deleteModelId = 0;
          this.showMessage(this.deleteMsg, MessageType.Info);

          if (this.rowData.data.length == 1 && this.pageIndex > 1)
            this.pageIndex = ((this.pageIndex - 1) * this.pageSize) - this.pageSize;

          this.selectedRows = [];
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

  public addNew() {
    this.openEditorDialog(true);
  }

  isDisabledCheckBtn = () => {
    if (this.selectedRows.length == 0)
      return true;
    else {
      if (this.selectedRows.length == 1) {
        if (this.selectedRows[0].statusId == DocumentStatusValue.Draft)
          return false;
        else
          return true;
      }
      else
        return false;
    }
  }

  isDisabledUnCheckBtn = () => {
    if (this.selectedRows.length == 0)
      return true;
    else {
      if (this.selectedRows.length == 1) {
        if (this.selectedRows[0].statusId == DocumentStatusValue.NormalCheck)
          return false;
        else
          return true;
      }
      else
        return false;
    }
  }

  isDisabledFinalizeBtn = () => {
    if (this.selectedRows.length == 0)
      return true;
    else {
      if (this.selectedRows.length == 1) {
        if (this.selectedRows[0].statusId == DocumentStatusValue.NormalCheck)
          return false;
        else
          return true;
      }
      else
        return false;
    }
  }

  onCheckHandler() {
    if (this.groupOperation) {
      //ثبت گروهی
    }
    else {
      //ثبت تکی
      this.voucherService.changeVoucherStatus(String.Format(VoucherApi.CheckVoucher, this.selectedRows[0].id)).subscribe(res => {
        this.reloadGrid();
        this.selectedRows = [];
      }, (error => {
        var message = error.message ? error.message : error;
        this.showMessage(message, MessageType.Warning);
      }));
    }
  }

  onUnCheckHandler() {
    if (this.groupOperation) {
      //برگشت سند گروهی
    }
    else {
      //برگشت سند تکی
      this.voucherService.changeVoucherStatus(String.Format(VoucherApi.UndoCheckVoucher, this.selectedRows[0].id)).subscribe(res => {
        this.reloadGrid();
        this.selectedRows = [];
      }, (error => {
        var message = error.message ? error.message : error;
        this.showMessage(message, MessageType.Warning);
      }));
    }
  }

  onFinalizeHandler() {
    if (this.groupOperation) {
      //ثبت قطعی گروهی
    }
    else {
      //ثبت قطعی تکی
      this.voucherService.changeVoucherStatus(String.Format(VoucherApi.FinalizeVoucher, this.selectedRows[0].id)).subscribe(res => {
        this.reloadGrid();
        this.selectedRows = [];
      }, (error => {
        var message = error.message ? error.message : error;
        this.showMessage(message, MessageType.Warning);
      }));
    }
  }
}


