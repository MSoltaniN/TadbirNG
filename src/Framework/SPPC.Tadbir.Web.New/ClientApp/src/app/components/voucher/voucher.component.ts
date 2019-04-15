import { Component, OnInit, Input, Renderer2, ChangeDetectorRef, ViewChild, ComponentRef } from '@angular/core';
import { VoucherService, VoucherInfo, FiscalPeriodService, SettingService } from '../../service/index';
import { Voucher } from '../../model/index';
import { ToastrService } from 'ngx-toastr';
import { GridDataResult, PageChangeEvent, RowArgs, SelectAllCheckboxState, GridComponent, ColumnComponent } from '@progress/kendo-angular-grid';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { String } from '../../class/source';
import { State, CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { SortDescriptor, orderBy } from '@progress/kendo-data-query';
import { DefaultComponent } from "../../class/default.component";
import { MessageType, Layout, Entities, Metadatas, environment } from "../../../environments/environment";
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

import * as moment from 'jalali-moment';
import { ReportApi } from '../../service/api/reportApi';
import { Report } from '../../model/report';
import { ReportingService, QuickReportColumnInfo, QuickReportViewInfo } from '../../service/report/reporting.service';
import { ReportManagementComponent } from '../reportManagement/reportManagement.component';
import { DialogService, DialogRef, DialogCloseResult } from '@progress/kendo-angular-dialog';
import { VoucherFormComponent } from '../../components/voucher/voucher-form.component';
import { ViewIdentifierComponent } from '../viewIdentifier/view-identifier.component';



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
  @ViewChild(ViewIdentifierComponent) viewIdentity: ViewIdentifierComponent;
  @ViewChild(ReportManagementComponent) reportManager: ReportManagementComponent;


  public rowData: GridDataResult;
  public selectedRows: number[] = [];
  public totalRecords: number;

  //permission flag
  viewAccess: boolean;

  //for add in delete messageText
  deleteConfirm: boolean;
  deleteModelId: number;

  startDate:any;
  endDate:any;

  currentFilter: FilterExpression;

  showloadingMessage: boolean = true;

  clickedRowItem: Voucher = undefined;
  editDataItem?: Voucher = undefined;

  groupDelete: boolean = false;


  private dialogRef: DialogRef;
  private dialogModel: any;
  //#endregion

  //#region Events
  ngOnInit() {
    this.viewAccess = this.isAccess(SecureEntity.Voucher, VoucherPermissions.View);
    this.reloadGrid();
  }

  /**
   * باز کردن و مقداردهی اولیه به فرم ویرایشگر
   */
  openEditorDialog(isNew: boolean) {

    this.dialogRef = this.dialogService.open({
      title: this.getText(isNew ? 'Buttons.New' : 'Buttons.Edit'),
      content: VoucherFormComponent,
    });

    this.dialogRef.dialog.location.nativeElement.classList.add(isNew ? 'new-dialog' : 'edit-dialog');

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.editModel = this.editDataItem;
    this.dialogModel.errorMessage = undefined;
    this.dialogModel.isNew = isNew;

    this.dialogRef.content.instance.save.subscribe((res) => {
      this.saveHandler(res, isNew);
    });

    const closeForm = this.dialogRef.content.instance.cancel.subscribe((res) => {
      this.dialogRef.close();

      this.dialogModel.errorMessage = undefined;
      this.dialogModel.editModel = undefined;
    });


    this.dialogRef.content.instance.changeMode.subscribe((res) => {

      this.dialogRef.close();
      this.editDataItem = new VoucherInfo();

      this.openEditorDialog(true);

    })

    this.dialogRef.content.instance.setFocus.subscribe((res) => {
      debugger;
      //this.dialogRef.dialog.instance.focus();
    });
  }


  onCellClick(e) {
    this.clickedRowItem = e.dataItem;
  }

  public rowDoubleClickHandler() {

    var recordId = this.selectedRows[0];
    this.grid.loading = true;
    this.voucherService.getById(String.Format(VoucherApi.Voucher, this.clickedRowItem.id)).subscribe(res => {
      this.editDataItem = res;

      this.openEditorDialog(false);

      this.grid.loading = false;
    })
  }


  selectionKey(context: RowArgs): string {
    if (context.dataItem == undefined) return "";
    return context.dataItem.id;
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

    this.sort = sort.filter(f => f.dir != undefined);

    this.reloadGrid();
  }

  removeHandler(arg: any) {
    this.deleteConfirm = true;
    if (!this.groupDelete) {
      var recordId = this.selectedRows[0];
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
    var recordId = this.selectedRows[0];
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
  }

  dateValueChange(event : any)
  {
      this.startDate = event.fromDate;
      this.endDate = event.toDate;
  }


  gridColumnResize(event:any)
  {
      
  }

  showQReport()
  {
    var columns : Array<QuickReportColumnInfo> = new Array<QuickReportColumnInfo>();
    this.grid.leafColumns.forEach(function(item)
    {
        //item.width
        var qr : QuickReportColumnInfo = new QuickReportColumnInfo();
        var column = item as ColumnComponent;
        if(column.field)
        {
          qr.name = column.field;
          qr.index = column.orderIndex;
          qr.visible = true;
          qr.width = column.width;
          qr.userText = column.displayTitle;   
          qr.sortOrder = 0;
          qr.sortMode = 0;
          qr.dataType = 1;
          qr.defaultText = column.displayTitle;
          qr.enabled = true;
          qr.order = column.orderIndex;
                

          columns.push(qr)
        }
    });    

    var dpi_x = document.getElementById('dpi').offsetWidth;    

    var viewInfo  = new QuickReportViewInfo();
    viewInfo.columns = columns;
    viewInfo.inchValue = dpi_x;
    viewInfo.reportTitle = "گزارش فوری";
    viewInfo.row = this.rowData.data[0];

    this.reporingService.putEnvironmentUserQuickReport(ReportApi.EnvironmentQuickReport,viewInfo)
    .subscribe((response : any) => {
      
      var design = response.designJson;
      var id = this.viewIdentity.ViewID;
      var params = null;
      if(this.viewIdentity.params.length > 0)
        params = this.viewIdentity.params.toArray();

      var rows = this.rowData.data;
      // var rows =
      //    [
      //      {
      //         no:"1",         
      //         statusName : "ss",         
      //         description : ""
      //     }
      //   ]
      

      this.reportManager.showQuickReport(id,params,this.currentFilter,this.sort,design,rows);

    });
    
  }

  public showReportManagement()
  {
      var id = this.viewIdentity.ViewID;
      var params = null;
      if(this.viewIdentity.params.length > 0)
        params = this.viewIdentity.params.toArray();

      this.reportManager.showDialog(id,params,this.currentFilter,this.sort);
  }

  public saveHandler(model: Voucher, isNew: boolean) {

    this.grid.loading = true;
    if (!isNew) {
      this.voucherService.edit<Voucher>(String.Format(VoucherApi.Voucher, model.id), model)
        .subscribe(response => {
          this.editDataItem = undefined;
          this.showMessage(this.updateMsg, MessageType.Succes);

          this.dialogRef.close();
          this.dialogModel.parent = undefined;
          this.dialogModel.errorMessage = undefined;
          this.dialogModel.model = undefined;

          this.reloadGrid();
        }, (error => {
          this.grid.loading = false;
          this.editDataItem = model;
          this.dialogModel.errorMessage = error;
        }));
    }
    else {
      this.voucherService.insert<Voucher>(VoucherApi.EnvironmentVouchers, model)
        .subscribe((response: any) => {
          this.editDataItem = undefined;
          this.showMessage(this.insertMsg, MessageType.Succes);
          var insertedModel = response;

          this.selectedRows = [];

          this.dialogRef.close();
          this.dialogModel.parent = undefined;
          this.dialogModel.errorMessage = undefined;
          this.dialogModel.model = undefined;

          this.reloadGrid(insertedModel);
        }, (error => {
          this.grid.loading = false;
          this.dialogModel.errorMessage = error;
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
  constructor(public toastrService: ToastrService, public translate: TranslateService, public dialogService: DialogService,
    public sppcLoading: SppcLoadingService, private cdref: ChangeDetectorRef,
    private voucherService: VoucherService, public renderer: Renderer2,
    public metadata: MetaDataService, public settingService: SettingService,
    public reporingService: ReportingService) {
    super(toastrService, translate, renderer, metadata, settingService, Entities.Voucher, Metadatas.Voucher);
  }
  //#endregion

  //#region Methods

  reloadGridEvent() {
    this.reloadGrid();
  }

  reloadGrid(insertedModel?: Voucher) {
    if (this.viewAccess) {
      this.grid.loading = true;
      var filter = this.currentFilter;
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
      if (this.groupDelete) {
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
    this.editDataItem = new VoucherInfo();

    this.openEditorDialog(true);
  }

  //#endregion
}


