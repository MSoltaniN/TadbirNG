import { Component, OnInit, Renderer2, ViewChild } from '@angular/core';
import { OperationLogService, SettingService } from '../../service/index';
import { OperationLog } from '../../model/index';
import { ToastrService } from 'ngx-toastr';
import { GridDataResult, PageChangeEvent, RowArgs, GridComponent } from '@progress/kendo-angular-grid';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { SortDescriptor } from '@progress/kendo-data-query';
import { DefaultComponent } from "../../class/default.component";
import { Layout, Entities } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { SystemApi } from '../../service/api/index';
import { FilterExpression } from '../../class/filterExpression';
import { ViewName } from '../../security/viewName';
import { BrowserStorageService } from '../../service/browserStorage.service';


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'operationLogs',
  templateUrl: './operationLogs.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }, DefaultComponent]
})


export class OperationLogsComponent extends DefaultComponent implements OnInit {

  //#region Fields

  @ViewChild(GridComponent) grid: GridComponent;

  public rowData: GridDataResult;
  public totalRecords: number;

  currentFilter: FilterExpression;
  currentOrder: string = "";

  showloadingMessage: boolean = true;

  detailDataItem?: OperationLog = undefined;
  //#endregion

  //#region Events
  ngOnInit() {
    this.reloadGrid();
  }

  selectionKey(context: RowArgs): string {
    if (context.dataItem == undefined) return "";
    return context.dataItem.id + " " + context.index;
  }

  //dataStateChange(state: DataStateChangeEvent): void {


  //  debugger;

  //  //this.currentFilter = this.getFilters(state.filter);
  //  if (state.sort)
  //    if (state.sort.length > 0)
  //      this.currentOrder = state.sort[0].field + " " + state.sort[0].dir;
  //  this.state = state;
  //  this.skip = state.skip;
  //  this.reloadGrid();
  //}

  sortChange(sort: SortDescriptor[]): void {

    this.sort = sort.filter(f => f.dir != undefined);

    this.reloadGrid();
  }

  pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
    this.reloadGrid();
  }

  public editHandler(arg: any) {
    this.detailDataItem = arg.dataItem;
  }

  public cancelHandler() {
    this.detailDataItem = undefined;
  }

  //#endregion

  //#region Constructor
  constructor(public toastrService: ToastrService, public translate: TranslateService, public bStorageService: BrowserStorageService,
    private operationLogService: OperationLogService, public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService) {
    super(toastrService, translate, bStorageService, renderer, metadata, settingService, Entities.OperationLog, ViewName.OperationLog);
  }
  //#endregion

  //#region Methods
  reloadGrid(insertedModel?: OperationLog) {

    this.grid.loading = true;
    var filter = this.currentFilter;
    var order = this.currentOrder;
    if (this.totalRecords == this.skip && this.totalRecords != 0) {
      this.skip = this.skip - this.pageSize;
    }
    this.operationLogService.getAll(SystemApi.AllOperationLogs, this.pageIndex, this.pageSize, this.sort, filter).subscribe((res) => {
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
        data: resData,
        total: totalCount
      }
      this.showloadingMessage = !(resData.length == 0);
      this.totalRecords = totalCount;
      this.grid.loading = false;
    })
  }
  //#endregion
}


