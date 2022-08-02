import {
  ChangeDetectorRef,
  Injectable,
  NgZone,
  OnDestroy,
  Renderer2,
  ViewChild,
} from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import {
  ColumnBase,
  GridComponent,
  GridDataResult,
  PageChangeEvent,
} from "@progress/kendo-angular-grid";
import {
  CompositeFilterDescriptor,
  filterBy,
  orderBy,
  SortDescriptor,
} from "@progress/kendo-data-query";
import { SettingService } from "@sppc/config/service/settings.service";
import { DefaultComponent } from "@sppc/shared/class/default.component";
import { Filter } from "@sppc/shared/class/filter";
import { FilterExpression } from "@sppc/shared/class/filterExpression";
import { Property } from "@sppc/shared/class/metadata/property";
import { String } from "@sppc/shared/class/source";
import {
  ColumnViewConfig,
  FilterRow,
  ListFormViewConfig,
} from "@sppc/shared/models";
import {
  BrowserStorageService,
  SessionKeys,
} from "@sppc/shared/services/browserStorage.service";
import { GridService } from "@sppc/shared/services/grid.service";
import { MetaDataService } from "@sppc/shared/services/metadata.service";
import { ToastrService } from "ngx-toastr";
import { take } from "rxjs/operators";
import { ViewName } from "../security";

@Injectable()
export class AutoGeneratedClientGridComponent
  extends DefaultComponent
  implements OnDestroy
{
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

  @ViewChild(GridComponent, {static: false}) grid: GridComponent;
  public viewId: number;
  tempViewId: number;
  public metadataKey: string;
  public rowData: any[];
  public currentRowData: GridDataResult;
  public selectedRows: any[] = [];
  public totalRecords: number;
  public reportFilter: FilterExpression;
  viewAccess: boolean;
  //allSelectedRows: any[] = [];

  deleteConfirm: boolean;
  deleteModelId: number;
  currentFilter: CompositeFilterDescriptor;
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

  advanceFilters: FilterExpression;
  advanceFilterList: Array<FilterRow>;
  //advanceGroupFilterRow: Array<Filter>;
  selectedGroupFilter: number;

  /** اگر برای تابع ریلود بخواهیم پارامتر ارسال کنیم مانند فرم مانده به تفکیک این پارامتر باید مقدار صحیح داشته باشد */
  public useReloadParameter: boolean = false;

  /** این فلگ برای زمانی میباشید که میخواهیم فیلتری از کلاس مشتق شده با مجموعه فیلتر ها اور کنیم */
  public useCustomFilterExpression: boolean = false;
  public customFilter: FilterExpression;

  /** اگر بخواهیم قبل از لود دیتا فراخوانی متد سرویس را متوقف کنیم این خاصیت را مقدار دهی میکنیم */
  public cancelLoad: boolean;

  public set entityName(name: string) {
    this.entityTypeName = name;
    this.localizeMsg(name);
    this.getPageSize();
  }

  constructor(
    public toastrService: ToastrService,
    public translate: TranslateService,
    public gridService: GridService,
    public renderer: Renderer2,
    public metadataService: MetaDataService,
    public settingService: SettingService,
    public bStorageService: BrowserStorageService,
    public cdref: ChangeDetectorRef,
    public ngZone: NgZone
  ) {
    super(
      toastrService,
      translate,
      bStorageService,
      renderer,
      metadataService,
      settingService,
      "",
      undefined
    );
  }

  ngOnDestroy(): void {
    this.bStorageService.removeSessionStorage("unSaveFilter" + this.viewId);
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
          result = "فعال";
        } else {
          result = "غیرفعال";
        }
        break;
      }
      case "currency":
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
    this.metadataKey = String.Format(
      SessionKeys.MetadataKey,
      this.viewId ? this.viewId.toString() : "",
      this.currentlang
    );
    var viewId = ViewName[this.entityTypeName];

    if (viewId) {
      var item: string | null;
      item = this.bStorageService.getMetadata(this.metadataKey);

      if (!this.properties)
        this.properties = new Map<string, Array<Property>>();

      var arr = JSON.parse(item != null ? item.toString() : "");
      this.properties.set(this.metadataKey, arr);
      if (!this.properties.get(this.metadataKey)) return undefined;

      var result = this.properties
        .get(this.metadataKey)
        .find((p) => p.name.toLowerCase() == name.toLowerCase());
      return result;
    }
  }

  public getColumnTitle(item: any): string {
    let setting: ColumnViewConfig;
    setting = JSON.parse(item.settings);
    var size = this.screenSize;
    var screenSetting = setting[size];
    return screenSetting.title;
  }

  reloadGrid(): void {
    this.onBeforeDataBind();

    if (!this.cancelLoad) {
      this.baseReload();
    } else this.cancelLoad = false;
  }

  //method overload 1
  private baseReload() {
    this.grid.loading = true;

    var rows: any[] = this.rowData;
    if (this.currentFilter) rows = filterBy(rows, this.currentFilter);

    if (this.sort) rows = orderBy(rows, this.sort);

    this.currentRowData = {
      data: rows.slice(this.skip, this.skip + this.pageSize),
      total: rows.length,
    };

    this.totalRecords = this.rowData.length;
    this.grid.loading = false;
    this.showloadingMessage = !(this.rowData.length == 0);
    this.cdref.detectChanges();
  }

  reloadGridEvent() {
    this.reloadGrid();
  }

  filterChange(filter: CompositeFilterDescriptor): void {
    this.currentFilter = filter;
  }

  public sortChange(sort: SortDescriptor[]): void {
    this.sort = sort.filter((f) => f.dir != undefined);
    this.reloadGrid();
  }

  public pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
    this.pageSize = event.take;
    this.setPageSizeByViewId();
    this.reloadGrid();
  }

  public editHandler(arg: any) {}

  public saveHandler(model: any, isNew: boolean) {}

  public onDataStateChange(event): void {
    if (this.rowData && this.rowData.length > 0) {
      var fcolumns = new Array<ColumnBase>();
      this.grid.columns.forEach(function (column) {
        if (column.width == undefined) fcolumns.push(column);
      });
      this.fitColumns(fcolumns);
    }
  }

  public fitColumns(fcolumns: Array<ColumnBase>): void {
    if (fcolumns.length > 0) {
      this.ngZone.onStable
        .asObservable()
        .pipe(take(1))
        .subscribe(() => {
          this.grid.autoFitColumns(fcolumns);
        });
    }
  }

  setPageSizeByViewId() {
    var settingsJson = this.bStorageService.getUserSettings(this.UserId);
    if (settingsJson) {
      var settings: Array<ListFormViewConfig> = JSON.parse(settingsJson);

      if (settings) {
        var findIndex = settings.findIndex((s) => s.viewId == this.viewId);

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
        var item = settings.find((s) => s.viewId == viewId);
        if (item) {
          this.pageSize = item.pageSize;
        }
      }
    }
  }
}
