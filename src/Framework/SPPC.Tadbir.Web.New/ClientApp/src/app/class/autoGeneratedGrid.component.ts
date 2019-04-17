import { Injectable, ViewChild, Renderer2, Optional, Inject, ChangeDetectorRef } from "@angular/core";
import { DefaultComponent } from "./default.component";
import { Property } from "./metadata/property";
import { GridComponent, GridDataResult, PageChangeEvent, SelectAllCheckboxState, RowArgs } from "@progress/kendo-angular-grid";
import { FilterExpression } from "./filterExpression";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from "@ngx-translate/core";
import { MetaDataService } from "../service/metadata/metadata.service";
import { SettingService, GridService } from "../service/index";
import { SortDescriptor, CompositeFilterDescriptor } from "@progress/kendo-data-query";
import { String } from "./source";
import { Filter } from "./filter";
import { FilterExpressionOperator } from "./filterExpressionOperator";
import { ViewName } from "../security/viewName";



@Injectable()
export class AutoGeneratedGridComponent extends DefaultComponent {

  @ViewChild(GridComponent) grid: GridComponent;

  public rowData: GridDataResult;
  public selectedRows: number[] = [];
  public totalRecords: number;
  viewAccess: boolean;

  deleteConfirm: boolean;
  deleteModelId: number;
  currentFilter: FilterExpression;
  showloadingMessage: boolean = true;
  groupDelete: boolean = false;


  gridColumns: Array<any> = [];
  /**نوع متادیتا*/
  metadataType: string;
  /**نام موجودیت*/
  entityTypeName: string;
  /**نوع آدرس سرویس برای گرفتن متادیتا از سرویس*/
  metadataUrlType: number;
  /**آدرس خواندن اطلاعات از سرویس*/
  getDataUrl: string;
  /**آرایه ای از فیلترهای پیش فرض*/
  defaultFilter: Array<Filter> = [];


  constructor(public toastrService: ToastrService, public translate: TranslateService, public gridService: GridService,
    public renderer: Renderer2, public metadataService: MetaDataService, public settingService: SettingService, public cdref: ChangeDetectorRef) {
    super(toastrService, translate, renderer, metadataService, settingService, '', '');
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

  //async getMetaDataByViewIdAcync(name: string): Promise<Property> {
  //  var viewId = ViewName[this.entityTypeName];
  //  this.metaDataName = this.metadataType;
  //  if (this.metaDataName) {
  //    if (!localStorage.getItem(this.metaDataName)) {
  //      const response = await this.metadataService.getReportMetaDataById(viewId).toPromise();
  //      let res: any = response;
  //      this.properties.set(this.metaDataName, res.columns);
  //      localStorage.setItem(this.metaDataName, JSON.stringify(res.columns))
  //      var result = this.properties.get(this.metaDataName).find(p => p.name.toLowerCase() == name.toLowerCase());
  //      return result;
  //    }
  //    else {
  //      var item: string | null;
  //      item = localStorage.getItem(this.metaDataName);
  //      if (!this.properties) this.properties = new Map<string, Array<Property>>();
  //      var arr = JSON.parse(item != null ? item.toString() : "");
  //      this.properties.set(this.metaDataName, arr);
  //      if (!this.properties.get(this.metaDataName)) return undefined;
  //      var result = this.properties.get(this.metaDataName).find(p => p.name.toLowerCase() == name.toLowerCase());
  //      return result;
  //    }
  //  }

  //}

  //async getMetaDataAcync(name: string): Promise<Property> {
  //  this.metaDataName = this.metadataType;

  //  if (this.metaDataName) {

  //    if (!localStorage.getItem(this.metaDataName)) {
  //      const response = await this.metadataService.getMetaData(this.metaDataName).toPromise();
  //      let res: any = response;
  //      this.properties.set(this.metaDataName, res.columns);
  //      localStorage.setItem(this.metaDataName, JSON.stringify(res.columns))
  //      var result = this.properties.get(this.metaDataName).find(p => p.name.toLowerCase() == name.toLowerCase());
  //      return result;
  //    }
  //    else {
  //      var item: string | null;
  //      item = localStorage.getItem(this.metaDataName);
  //      if (!this.properties) this.properties = new Map<string, Array<Property>>();
  //      var arr = JSON.parse(item != null ? item.toString() : "");
  //      this.properties.set(this.metaDataName, arr);
  //      if (!this.properties.get(this.metaDataName)) return undefined;
  //      var result = this.properties.get(this.metaDataName).find(p => p.name.toLowerCase() == name.toLowerCase());
  //      return result;
  //    }

  //  }
  //}

  public getMetaData(name: string): Property | undefined {
    this.metaDataName = this.metadataType;
    var viewId = ViewName[this.entityTypeName];

    if (this.metaDataName) {

      if (!localStorage.getItem(this.metaDataName)) {

        if (this.metadataUrlType == 1) {

          this.metadataService.getMetaData(this.metaDataName).finally(() => {
            if (!this.properties.get(this.metaDataName)) return undefined;
            var result = this.properties.get(this.metaDataName).find(p => p.name.toLowerCase() == name.toLowerCase());
            return result;
          }).subscribe((res1: any) => {
            this.properties.set(this.metaDataName, res1.columns);
            localStorage.setItem(this.metaDataName, JSON.stringify(res1.columns))
            var result = this.properties.get(this.metaDataName).find(p => p.name.toLowerCase() == name.toLowerCase());
            return result;
          });
          
        }
        if (this.metadataUrlType == 2) {

          this.metadataService.getReportMetaDataById(viewId).finally(() => {
            if (!this.properties.get(this.metaDataName)) return undefined;
            var result = this.properties.get(this.metaDataName).find(p => p.name.toLowerCase() == name.toLowerCase());
            return result;
          }).subscribe((res1: any) => {
            this.properties.set(this.metaDataName, res1.columns);
            localStorage.setItem(this.metaDataName, JSON.stringify(res1.columns))
            var result = this.properties.get(this.metaDataName).find(p => p.name.toLowerCase() == name.toLowerCase());
            return result;
          });

        }        
      }
      else {
        var item: string | null;
        item = localStorage.getItem(this.metaDataName);
        if (!this.properties) this.properties = new Map<string, Array<Property>>();
        var arr = JSON.parse(item != null ? item.toString() : "");
        this.properties.set(this.metaDataName, arr);
        if (!this.properties.get(this.metaDataName)) return undefined;
        var result = this.properties.get(this.metaDataName).find(p => p.name.toLowerCase() == name.toLowerCase());
        return result;
      }

    }
  }

  public getColumnTitle(itemName: string): string {
    let title = '';

    title = this.getText(String.Format("{0}.{1}", this.entityTypeName, itemName))


    return title;
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
          data: resData,
          total: totalCount
        }

        this.showloadingMessage = !(resData.length == 0);
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

  reloadGridEvent() {
    this.reloadGrid();
  }

  deleteModel(confirm: boolean) {

  }

  public addNew() {

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

  }

  public pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
    this.reloadGrid();
  }

  public editHandler(arg: any) {

  }

  public saveHandler(model: any, isNew: boolean) {

  }
}
