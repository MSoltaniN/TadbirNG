import { Injectable, ViewChild, Renderer2, Optional, Inject, ChangeDetectorRef, NgZone, EventEmitter, Output, OnDestroy, ReflectiveInjector, DebugElement } from "@angular/core";
import { GridComponent, GridDataResult, PageChangeEvent, SelectAllCheckboxState, RowArgs, ColumnBase } from "@progress/kendo-angular-grid";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from "@ngx-translate/core";
import { SortDescriptor, CompositeFilterDescriptor } from "@progress/kendo-data-query";
import { take, map } from 'rxjs/operators';
import { String } from '@sppc/shared/class/source';
import { FilterExpression } from '@sppc/shared/class/filterExpression';
import { Filter } from '@sppc/shared/class/filter';
import { Property } from '@sppc/shared/class/metadata/property';
import { FilterExpressionOperator } from '@sppc/shared/class/filterExpressionOperator';
import { ListComponent } from '@sppc/shared/class/list.component';
import { BrowserStorageService, SessionKeys } from "@sppc/shared/services/browserStorage.service";
import { SettingService } from "@sppc/config/service/settings.service";
import { ViewName } from "@sppc/shared/security/viewName";
import { Permissions, GlobalPermissions } from "@sppc/shared/security/permissions";
import { ColumnViewConfig, ListFormViewConfig, FilterRow, GroupFilter, Item, IEntity } from "@sppc/shared/models";
import { MetaDataService } from "@sppc/shared/services/metadata.service";
import { GridService } from "@sppc/shared/services/grid.service";
import { MessageType } from "@sppc/env/environment";
import { AdvanceFilterComponent } from "@sppc/shared/components/advanceFilter/advance-filter.component";
import { DialogRef, DialogService } from "@progress/kendo-angular-dialog";
import { ServiceLocator } from "@sppc/service.locator";
import { ExcelExportData } from "@progress/kendo-angular-excel-export";
import { Observable } from "rxjs";
import { ErrorListComponent } from "@sppc/shared/components/errorList/errorList.component";
import { NumberConfig } from "@sppc/config/models";
import { debug } from "util";
import { ReloadOption } from "./reload-option";
import { ReloadStatusType } from "../enum";
import { BaseService } from ".";
import { ResultOption } from "./result.option";



@Injectable()
export class AutoGeneratedGridComponent<T = void> extends ListComponent implements OnDestroy {
   
  @ViewChild(GridComponent) grid: GridComponent;
  public viewId: number;
  tempViewId: number;
  public metadataKey: string;
  public rowData: GridDataResult;
  public selectedRows: any[] = [];
  public totalRecords: number;
  public reportFilter: FilterExpression;
  public reportCurrentFilter: FilterExpression;  
  viewAccess: boolean;
  //allSelectedRows: any[] = [];
  exportAccessed: boolean;
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
  defaultFilter: Array<Filter>;

  quickFilter: Filter[];

  config: NumberConfig;

  /** اگر برای تابع ریلود بخواهیم پارامتر ارسال کنیم مانند فرم مانده به تفکیک این پارامتر باید مقدار صحیح داشته باشد */
  public useReloadParameter: boolean = false;

  /** این فلگ برای زمانی میباشید که میخواهیم فیلتری از کلاس مشتق شده با مجموعه فیلتر ها اور کنیم */
  public useCustomFilterExpression: boolean = false;
  public customFilter: FilterExpression;

  /** این فلگ برای زمانی میباشید که میخواهیم فیلتری از کلاس مشتق شده با مجموعه فیلتر های سریع اور کنیم */
  public useCustomQuickFilterExpression: boolean = false;
  public customQuickFilter: FilterExpression;

  /** این فلگ برای زمانی میباشید که میخواهیم عمل لاگ انجام شود یا انجام نشود */
  public listChanged: boolean = true;

  /** این فلگ برای زمانی میباشید که میخواهیم عمل لاگ انجام شود یا انجام نشود */
  public listChangedViews: Array<number> = [];

  /** اگر بخواهیم قبل از لود دیتا فراخوانی متد سرویس را متوقف کنیم این خاصیت را مقدار دهی میکنیم */
  public cancelLoad: boolean;

  /*** این پارامتر برای گزارش های مثل سود و زیان استفاده میشود */
  parameters: Array<number>;
  //dialogService: DialogService;
  //permission: Permissions;
  //filterDialogRef: DialogRef;

  public set entityName(name: string) {
    this.entityTypeName = name;
    this.localizeMsg(name);
    this.getPageSize();
  }

  public dialogRef: DialogRef;
  public dialogModel: any;
  public editDataItem?: T | any;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public gridService: GridService,
    public renderer: Renderer2, public metadataService: MetaDataService, public settingService: SettingService, public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef, public ngZone: NgZone) {    
    super(toastrService, translate, gridService, renderer, metadataService, settingService, bStorageService, cdref, ngZone);

    (async () => {
      this.config = await this.settingService.getNumberConfigBySettingIdAsync();
    })();   
    
  }

  ngAfterViewInit(): void {
    (async () => {
      if (this.viewId) {
        var entityName = await this.getEntityName(this.viewId);
        var code = <number>GlobalPermissions.Export;
        this.exportAccessed = this.isAccess(entityName, code);
      }
    })();
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
  

  reloadGrid(options?: ReloadOption):void {

    this.onBeforeDataBind();

    if (!this.cancelLoad) {
      //check pagenumber  and correction
      if (options && options.Status == ReloadStatusType.AfterDelete && this.rowData) {
        var pageCount = Math.floor((this.rowData.total - this.selectedRows.length) / this.pageSize) + 1;
        if (this.pageIndex > 0 && this.pageIndex > pageCount)
          this.pageIndex = ((this.pageIndex - 1) * this.pageSize) - this.pageSize;
      }

      if (options && (options.Status == ReloadStatusType.AfterFilter || options.Status == ReloadStatusType.None)) {
        this.skip = 0;
      }
      //check pagenumber  and correction

      //implement overload 2
      if (this.useReloadParameter || (options && options.Parameter)) {
        if ((options && !options.Parameter)) {
          options.Parameter = this.onGenerateParameters()
        }
        this.reloadWithParam(options);
      }
      //implement overload 1
      else {
        this.baseReload(options);
      }
    }
    else
      this.cancelLoad = false;
  }

  private reloadWithParam(options?: ReloadOption) {
    if (this.getDataUrl) {

      this.grid.loading = true;

      if (this.totalRecords == this.skip && this.totalRecords != 0) {
        this.skip = this.skip - this.pageSize;
      }

      if (options && options.InsertedModel)
        this.goToLastPage(this.totalRecords);

      var currentFilter: FilterExpression = this.currentFilter;

      if (this.defaultFilter && this.defaultFilter.length > 0) {               

        this.defaultFilter.forEach(item => {
          currentFilter = this.addFilterToFilterExpression(currentFilter,
            item, FilterExpressionOperator.And);
        })

        if (this.currentFilter) {
          currentFilter = this.andTwoFilterExpression(currentFilter,
            this.currentFilter);
        }

        if (this.useCustomFilterExpression && this.customFilter) {
          currentFilter = this.andTwoFilterExpression(currentFilter,
            this.customFilter);
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
      this.reportCurrentFilter = filter;

      //init list change from listchange variable or listchange array
      if (!this.customListChanged) {
        var changed = this.listChanged;
        if (this.listChangedViews && this.listChangedViews.findIndex(f => f === this.viewId) > -1) {
          changed = false;
        }
      }
      else
        changed = this.listChanged;
      

      //init params
      //var gridPaging = { pageIndex: this.pageIndex, pageSize: this.pageSize };
      //var gridOptions = { paging: gridPaging, filter: filter, quickFilter: filterExp, sortColumns: this.sort, listChanged : changed };
      //var params = parameter;
      //params.gridOptions = gridOptions;
      
      this.gridService.getAllByParams(this.getDataUrl, options.Parameter,this.pageIndex, this.pageSize, this.sort, filter, filterExp, changed).subscribe((res) => {

        //load metadata from response 
        if (res.body.viewMetadata) {
          this.properties.set(res.body.viewMetadata.name, res.body.viewMetadata.columns);
          this.gridColumns = res.body.viewMetadata.columns;
        }

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

        if (res.body.viewMetadata) 
        {
          this.rowData = {
            data: resData.comparativeItems,
            total: totalCount
          }
          this.showloadingMessage = !(resData.comparativeItems.length == 0);
          this.viewId = res.body.viewMetadata.id;
        }
        else {
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
        }        

        this.totalRecords = totalCount;
        this.grid.loading = false;

        this.listChanged = true;
        this.disableViewListChanged(this.viewId);

        this.onDataBind(resData);        
      })

    }
    this.cdref.detectChanges();
  }

  //method overload 1
  private baseReload(options?: ReloadOption) {
    if (this.getDataUrl) {

      this.grid.loading = true;

      if (this.totalRecords == this.skip && this.totalRecords != 0) {
        this.skip = this.skip - this.pageSize;
      }

      if (options && options.InsertedModel)
        this.goToLastPage(this.totalRecords);  
  
      let currentFilter = undefined;
      if (this.currentFilter)
        currentFilter = JSON.parse(JSON.stringify(this.currentFilter));


      if (this.defaultFilter && this.defaultFilter.length > 0) {        

        this.defaultFilter.forEach(item => {
          currentFilter = this.addFilterToFilterExpression(currentFilter,
            item, FilterExpressionOperator.And);
        })     

        if (this.useCustomFilterExpression && this.customFilter) {
          currentFilter = this.andTwoFilterExpression(currentFilter,
            this.customFilter);
        }
      }

      var filterExp: FilterExpression;
      if (this.quickFilter) {
        this.quickFilter.forEach(item => {
          filterExp = this.addFilterToFilterExpression(filterExp,
            item, FilterExpressionOperator.And);
        })

        if (this.useCustomQuickFilterExpression && this.customQuickFilter) {
          filterExp = this.andTwoFilterExpression(filterExp,
            this.customQuickFilter);
        }
      }

      //this code for concat filters to advanceFilters
      if (this.advanceFilters)
        currentFilter = this.andFilterToFilterExpression(currentFilter,
          this.advanceFilters);

      var filter = currentFilter;
      this.reportFilter = filterExp;      
      this.reportCurrentFilter = filter;
      //init list change from listchange variable or listchange array
      if (!this.customListChanged) {
        var changed = this.listChanged;
        if (this.listChangedViews && this.listChangedViews.findIndex(f => f === this.viewId) > -1) {
          changed = false;
        }
      }
      else
        changed = this.listChanged;

      this.gridService.getAll(this.getDataUrl, this.pageIndex, this.pageSize, this.sort, filter, filterExp, changed).subscribe((res) => {
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
        this.disableViewListChanged(this.viewId);

        this.onDataBind(resData);
        
      }, (error => {
        var message = error.message ? error.message : error;
        this.grid.loading = false;
        this.showMessage(message, MessageType.Warning);
      }))

    }
    this.cdref.detectChanges();
  }

  public getExportData(): Observable<GridDataResult> {
    if (!this.exportAccessed) {
      this.showMessage(this.getText('App.AccessDenied'), MessageType.Warning);
      return Observable.empty<GridDataResult>();
    }

    if (this.getDataUrl) {

      let currentFilter = undefined;
      if (this.currentFilter)
        currentFilter = JSON.parse(JSON.stringify(this.currentFilter));

      if (this.defaultFilter && this.defaultFilter.length > 0) {
        this.defaultFilter.forEach(item => {
          currentFilter = this.addFilterToFilterExpression(currentFilter,
            item, FilterExpressionOperator.And);
        })

        if (this.useCustomFilterExpression && this.customFilter) {
          currentFilter = this.andTwoFilterExpression(currentFilter,
            this.customFilter);
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

      if (!this.parameters || this.parameters.length == 0) 
      {
        return this.gridService.getAll(this.getDataUrl, 1, 1000000, this.sort, filter, filterExp, false).pipe(
          map(response => (<GridDataResult>{
            data: (response.body.items) ? response.body.items : response.body,
            total: (response.body.items) ? response.body.items.length : response.body.length,
          }))
        );
      }
      else {
        var param = { paraf: "", items: this.parameters };
        return this.gridService.getAllByParams(this.getDataUrl,param, 1, 1000000, this.sort, filter, filterExp, false).pipe(
          map(response => (<GridDataResult>{
            data: (response.body.comparativeItems && response.body.comparativeItems.length > 0) ? response.body.comparativeItems : response.body.items,
            total: (response.body.comparativeItems && response.body.comparativeItems.length > 0) ? response.body.comparativeItems.length : response.body.items.length,
          }))
        );

      }
    }
    else {
      this.showMessage(this.getText("App.PleaseLoadData"));
      return Observable.empty<GridDataResult>();
    }
  }

  public allData = (): Observable<any> => {
    this.excelFileName = this.getExcelFileName();
    return this.getExportData();
  }

  public onExcelExport(e: any): void  {    
    
    const rows = e.workbook.sheets[0].rows;
    var header = null;

    //set decimalPrecision
    var decimalPrecision = "";
    if (this.config.decimalPrecision > 0) {
      decimalPrecision = "0."
      for (var i = 0; i < this.config.decimalPrecision; i++) {
        decimalPrecision += "0"
      }      
    }
      
    //set footer data for reports
    rows.forEach((row) => {
      if (row.type === 'header' && header == null) {
        header = row;
      }
      if (row.type === 'footer') {
        this.onFooterExportToExcel(header, row);
      }
    });

    //set format for cells
    rows.forEach((row) => {      
      if (row.type === 'data' || row.type === 'footer') {        
        var cellNo = 0;
        row.cells.forEach((cell) => {

          //set font
          cell.fontSize = 12;
          cell.fontName = "Tahoma";
          if (this.gridColumns[cellNo]) {
            if (this.CurrentLanguage == 'fa') {

              if (this.gridColumns[cellNo].scriptType == 'number' && this.gridColumns[cellNo].storageType == 'money') {
                if (cell.value != "0")
                  cell.format = '[$-3020429]#,###' + decimalPrecision;
                else
                  cell.format = '[$-3020429]#';
              }
              else if (this.gridColumns[cellNo].scriptType == 'number' &&
                (this.gridColumns[cellNo].storageType == 'int' || this.gridColumns[cellNo].storageType == 'smallint')) {
                cell.format = '[$-3020429]#';
              }
              else if (this.gridColumns[cellNo].scriptType == 'boolean' &&
                (this.gridColumns[cellNo].storageType == 'bit')) {
                if (cell.value.toString().toLower() == 'false')
                  cell.value = this.getText('Exports.False');
                if (cell.value.toString().toLower() == 'true')
                  cell.value = this.getText('Exports.True');
              }
              else if (cell.value && this.gridColumns[cellNo].scriptType == 'string' &&
                this.gridColumns[cellNo].storageType == 'nvarchar') {
                cell.value = '‏' + cell.value;
              }

              cell.hAlign = "right";
            }
            else {
              if (this.gridColumns[cellNo].scriptType == 'number' && this.gridColumns[cellNo].storageType == 'money') {
                if (cell.value != "0")
                  cell.format = '#,###' + decimalPrecision;                
              }              
              else if (this.gridColumns[cellNo].scriptType == 'boolean' &&
                (this.gridColumns[cellNo].storageType == 'bit')) {
                if (cell.value.toString().toLower() == 'false')
                  cell.value = this.getText('Exports.False');
                if (cell.value.toString().toLower() == 'true')
                  cell.value = this.getText('Exports.True');
              }
              else if (cell.value && this.gridColumns[cellNo].scriptType == 'string' &&
                this.gridColumns[cellNo].storageType == 'nvarchar') {
                cell.value = '‎‏' + cell.value;
              }

              cell.hAlign = "left";
            }
          }      

          cellNo++;

          });                
      }
      
    });

  }

  reloadGridEvent() {
    this.reloadGrid();
  }  

  disableViewListChanged(viewId:number) {
    if (this.listChangedViews.findIndex(p => p === viewId) == -1) {      
      this.listChangedViews.push(viewId);
    }

  }

  deleteModel(confirm: boolean) {

  }

  public addNew() {

  }

  selectionKey(context: RowArgs): any {
    //return context.dataItem;    
    return context.dataItem.id;  
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
      var options = new ReloadOption();
      options.Status = ReloadStatusType.AfterFilter;

      this.reloadGrid();
    }
  }

  refetchGridColumns() {
    var properties = this.getAllMetaData(this.viewId);
    this.rowData = undefined;
    this.showloadingMessage = false;
    this.gridColumns = properties.filter(p=>p.visibility != 'Hidden');
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
    this.setPageSizeByViewId();
    this.reloadGrid();    
  }

  public editHandler(arg: any) {

  }

  /**
   * این متد برای ایجاد یا ویرایش موجودیت بکار میرود و در دو اورلود پیاده سازی شده است
   * @param model
   * @param isNew
   */
  saveHandler(model: any, isNew: boolean): void;
  saveHandler(model: T | any, isNew: boolean, service: BaseService, serviceUrl?: string);
  saveHandler(model: T | any, isNew: boolean, service?: BaseService, serviceUrl?: string) {
    var promise = new Promise((resolve, reject) => {
      if (service) {
        this.grid.loading = true;
        if (!isNew) {
          service.edit<T>(String.Format(serviceUrl, model.id), model)
            .subscribe(response => {
              this.editDataItem = undefined;
              this.showMessage(this.updateMsg, MessageType.Succes);

              if (this.dialogRef) {
                this.dialogRef.close();
                this.dialogModel.errorMessage = undefined;
                this.dialogModel.model = undefined;
              }

              var reloadOption = new ReloadOption();
              reloadOption.Status = ReloadStatusType.AfterEdit;

              this.reloadGrid(reloadOption);
              this.selectedRows = [];
              this.grid.loading = false;

              var resultOption = new ResultOption(true);             

              resolve(resultOption);

            }, (error => {
                this.editDataItem = model;
                if (this.dialogModel)
                  this.dialogModel.errorMessage = error;

                this.grid.loading = false;

                var resultOption = new ResultOption(false, error);
                reject(resultOption);
            }));
        }
        else {
          service.insert<T>(serviceUrl, model)
            .subscribe((response: any) => {
              this.editDataItem = undefined;
              this.showMessage(this.insertMsg, MessageType.Succes);
              var insertedModel = response;

              if (this.dialogRef) {
                this.dialogRef.close();
                this.dialogModel.errorMessage = undefined;
                this.dialogModel.model = undefined;
              }

              var options = new ReloadOption();
              options.InsertedModel = insertedModel
              options.Status = ReloadStatusType.AfterInsert;
              this.reloadGrid(options);

              this.selectedRows = [];
              this.grid.loading = false;
              var resultOption = new ResultOption(true);
              resolve(resultOption);

            }, (error => {
                if (this.dialogModel)
                  this.dialogModel.errorMessage = error;

                this.grid.loading = false;
                var resultOption = new ResultOption(false, error);
                reject(resultOption);
            }));
        }
      }
    }
    );

    return promise;    
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

  openErrorListDialog(rowData: any[], total: number) {

    this.dialogRef = this.dialogService.open({
      title: this.getText('ErrorList.GroupOperationReport'),
      content: ErrorListComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.rowData = rowData;
    this.dialogModel.totalItems = total;

    const closeForm = this.dialogRef.content.instance.cancel.subscribe((res) => {
      this.dialogRef.close();
    });

  }


  /**این تابع بعد از فراخوانی سرویس فراخوانی و دیتای مربوطه را برمیگرداند*/
  public onDataBind(res: any) {
    /*console.log('base ondatabind')*/
  }

  /**این تابع قبل از فراخوانی سرویس اجرا میشود*/
  public onBeforeDataBind() {
    /*console.log('base ondatabind')*/
  }

  /**این تابع قبل از فراخوانی متد ریلود فراخوانی و پارامتر مربوطه را برمیگرداند*/
  public onGenerateParameters(): any {
    /*console.log('base onGenerateParameters')*/
  }

  customListChanged: boolean;
  /** این ایونت اگر در کلاس مشتق شده پیاده سازی شده باشد میتوان برای تغییر متغیر listchange از آن استفاده کرد */
  public onListChanged() {

  }
  
}
