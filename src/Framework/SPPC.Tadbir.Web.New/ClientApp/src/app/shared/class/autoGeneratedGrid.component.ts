import { Injectable, ViewChild, Renderer2, Optional, Inject, ChangeDetectorRef, NgZone, EventEmitter, Output, OnDestroy } from "@angular/core";
import { GridComponent, GridDataResult, PageChangeEvent, SelectAllCheckboxState, RowArgs, ColumnBase } from "@progress/kendo-angular-grid";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from "@ngx-translate/core";
import { SortDescriptor, CompositeFilterDescriptor } from "@progress/kendo-data-query";
import { take } from 'rxjs/operators';
import { String } from '@sppc/shared/class/source';
import { FilterExpression } from '@sppc/shared/class/filterExpression';
import { Filter } from '@sppc/shared/class/filter';
import { Property } from '@sppc/shared/class/metadata/property';
import { FilterExpressionOperator } from '@sppc/shared/class/filterExpressionOperator';
import { DefaultComponent } from '@sppc/shared/class/default.component';
import { BrowserStorageService, SessionKeys } from "@sppc/shared/services/browserStorage.service";
import { SettingService } from "@sppc/config/service/settings.service";
import { ViewName } from "../security";
import { ColumnViewConfig, ListFormViewConfig, FilterRow, GroupFilter } from "@sppc/shared/models";
import { MetaDataService } from "@sppc/shared/services/metadata.service";
import { GridService } from "@sppc/shared/services/grid.service";



@Injectable()
export class AutoGeneratedGridComponent extends DefaultComponent implements OnDestroy {
   

  /**این تابع بعد از فراخوانی سرویس فراخوانی و دیتای مربوطه را برمیگرداند*/
  public onDataBind(res: any){
    console.log('base ondatabind')
  }
    
  @ViewChild(GridComponent) grid: GridComponent;
  public viewId: number;
  tempViewId: number;
  public metadataKey: string;
  public rowData: GridDataResult;
  public selectedRows: any[] = [];
  public totalRecords: number;
  public reportFilter: FilterExpression;
  viewAccess: boolean;
  //allSelectedRows: any[] = [];

  deleteConfirm: boolean;
  deleteModelId: number;
  currentFilter: FilterExpression;
  showloadingMessage: boolean = true;
  groupOperation: boolean = false;

  gridColumns: Array<any> = [];

  /**نام موجودیت*/
  entityTypeName: string;

  /**آدرس خواندن اطلاعات از سرویس*/
  getDataUrl: string;
  /**آرایه ای از فیلترهای پیش فرض*/
  defaultFilter: Array<Filter> = [];

  quickFilter: Filter[];

  advanceFilters: FilterExpression;
  advanceFilterList: Array<FilterRow>;
  //advanceGroupFilterRow: Array<Filter>;
  selectedGroupFilter: number; 

  /** این فلگ برای زمانی میباشید که میخواهیم فیلتری از کلاس مشتق شده با مجموعه فیلتر ها اور کنیم */
  public useOrFilterExpression: boolean = false;
  public orDefaultFilter: Filter;

  /** این فلگ برای زمانی میباشید که میخواهیم عمل لاگ انجام شود یا انجام نشود */
  public listChanged: boolean = true;


  public set entityName(name: string) {
    this.entityTypeName = name;
    this.localizeMsg(name);
    this.getPageSize();
  }

  constructor(public toastrService: ToastrService, public translate: TranslateService, public gridService: GridService,
    public renderer: Renderer2, public metadataService: MetaDataService, public settingService: SettingService, public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef, public ngZone: NgZone) {
    super(toastrService, translate, bStorageService, renderer, metadataService, settingService, '', undefined);
  }

  ngOnDestroy(): void {
    this.bStorageService.removeSessionStorage('unSaveFilter' + this.viewId);
  }

  getRowValue(columnName: string, dataItem: any, dataType: string): any {
    var colName = columnName.charAt(0).toLowerCase() + columnName.slice(1);
    var res = dataItem[colName];
    var result = res;

    switch (dataType.toLowerCase()) {
      case "date": {
        result = res;
        break;
      }
      case "boolean": {
        if (res == true) {
          result = "فعال"
        }
        else {
          result = "غیرفعال"
        }
        break;
      }
      case "money": {
        result = res;
        break;
      }
      default:
    }

    return result;
  }

  getColumns(e: any) {
    this.gridColumns = e;
  }

  public getMetaData(name: string): Property | undefined {

    this.metadataKey = String.Format(SessionKeys.MetadataKey, this.viewId ? this.viewId.toString() : '', this.currentlang);
    var viewId = ViewName[this.entityTypeName];

    if (viewId) {
      var item: string | null;
      item = this.bStorageService.getMetadata(this.metadataKey);

      if (!item) {
        this.metadataService.getMetaDataById(viewId).finally(() => {
          if (!this.properties.get(this.metadataKey)) return undefined;
          var result = this.properties.get(this.metadataKey).find(p => p.name.toLowerCase() == name.toLowerCase());
          return result;
        }).subscribe((res1: any) => {
          this.properties.set(this.metadataKey, res1.columns);
          this.bStorageService.setMetadata(this.metadataKey, res1.columns);
          var result = this.properties.get(this.metadataKey).find(p => p.name.toLowerCase() == name.toLowerCase());
          return result;
        });
      }
      else {
        if (!this.properties) this.properties = new Map<string, Array<Property>>();
        var arr = JSON.parse(item != null ? item.toString() : "");
        this.properties.set(this.metadataKey, arr);
        if (!this.properties.get(this.metadataKey)) return undefined;
        var result = this.properties.get(this.metadataKey).find(p => p.name.toLowerCase() == name.toLowerCase());
        return result;
      }

    }
  }

  public getColumnTitle(item: any): string {
    let setting: ColumnViewConfig;
    setting = JSON.parse(item.settings);
    var size = this.screenSize;
    var screenSetting = setting[size];
    return screenSetting.title;
  }

  reloadGrid(insertedModel?: any) {
    if (this.getDataUrl) {

      this.grid.loading = true;

      //if (this.selectedRows) {
      //  this.selectedRows.forEach((it) => {
      //    if (this.allSelectedRows.findIndex(item => item === it) == -1)
      //      this.allSelectedRows.push(it);
      //  });
      //}

      if (this.totalRecords == this.skip && this.totalRecords != 0) {
        this.skip = this.skip - this.pageSize;
      }

      if (insertedModel)
        this.goToLastPage(this.totalRecords);
      
      var currentFilter = this.currentFilter;      
      if (this.defaultFilter) {
        this.defaultFilter.forEach(item => {
          currentFilter = this.addFilterToFilterExpression(currentFilter,
            item, FilterExpressionOperator.And);
        })

        if (this.useOrFilterExpression) {
          currentFilter = this.addFilterToFilterExpression(currentFilter,
          this.orDefaultFilter, FilterExpressionOperator.Or);
        }
      }

      var filterExp: FilterExpression;
      if (this.quickFilter) {
        this.quickFilter.forEach(item => {
          filterExp = this.addFilterToFilterExpression(filterExp,
            item, FilterExpressionOperator.And);
        })
      }

      //this code for concat filters to advanceFilters
      if (this.advanceFilters)
        currentFilter = this.andFilterToFilterExpression(currentFilter,
          this.advanceFilters);

      var filter = currentFilter;
      this.reportFilter = filter;

      this.gridService.getAll(this.getDataUrl, this.pageIndex, this.pageSize, this.sort, filter, filterExp,this.listChanged).subscribe((res) => {        
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

        if (resData.items) {
          this.rowData = {
            data: resData.items,
            total: totalCount
          }

          this.showloadingMessage = !(resData.items.length == 0);
        }
        else {
          this.rowData = {
            data: resData,
            total: totalCount
          }

          this.showloadingMessage = !(resData.length == 0);
          
        }

        
        this.totalRecords = totalCount;
        this.grid.loading = false;
        
        this.listChanged = true;
        this.onDataBind(resData);
        //this.selectedRows = this.allSelectedRows;
      })

    }
    this.cdref.detectChanges();
  }

  reloadGridEvent() {
    this.reloadGrid();
  }

  deleteModel(confirm: boolean) {

  }

  public addNew() {

  }

  selectionKey(context: RowArgs): any {
    return context.dataItem;
  }

  onSelectedKeysChange(checkedState: SelectAllCheckboxState) {
    if (this.selectedRows.length > 1)
      this.groupOperation = true;
    else
      this.groupOperation = false;
  }

  filterChange(filter: CompositeFilterDescriptor): void {

    this.listChanged = false;
    var isReload: boolean = false;
    if (this.currentFilter && this.currentFilter.children.length > filter.filters.length)
      isReload = true;

    this.currentFilter = this.getFilters(filter);
    if (isReload) {
      this.reloadGrid();
    }
  }

  public sortChange(sort: SortDescriptor[]): void {

    this.listChanged = false;
    this.sort = sort.filter(f => f.dir != undefined);

    this.reloadGrid();
  }

  removeHandler(arg: any) {

  }

  public pageChange(event: PageChangeEvent): void {
    this.listChanged = false;
    this.skip = event.skip;
    this.pageSize = event.take;
    this.reloadGrid();
    this.setPageSizeByViewId();
  }

  public editHandler(arg: any) {

  }

  public saveHandler(model: any, isNew: boolean) {

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

  public fitColumns(fcolumns: Array<ColumnBase>): void {
    if (fcolumns.length > 0) {
      this.ngZone.onStable.asObservable().pipe(take(1)).subscribe(() => {
        this.grid.autoFitColumns(fcolumns);
      });
    }
  }


  setPageSizeByViewId() {
    var settingsJson = this.bStorageService.getUserSettings(this.UserId);
    if (settingsJson) {
      var settings: Array<ListFormViewConfig> = JSON.parse(settingsJson);

      if (settings) {
        var findIndex = settings.findIndex(s => s.viewId == this.viewId);

        if (findIndex > -1) {
          settings[findIndex].pageSize = this.pageSize;
          this.bStorageService.setUserSetting(settings, this.UserId);
        }
      }
    }
  }

  getPageSize() {
    var viewId = ViewName[this.entityTypeName];
    var settingsJson = this.bStorageService.getUserSettings(this.UserId);
    if (settingsJson) {
      var settings: Array<ListFormViewConfig> = JSON.parse(settingsJson);

      if (settings) {
        var item = settings.find(s => s.viewId == viewId);
        if (item) {
          this.pageSize = item.pageSize;
        }
      }
    }
  }

 
  
}
