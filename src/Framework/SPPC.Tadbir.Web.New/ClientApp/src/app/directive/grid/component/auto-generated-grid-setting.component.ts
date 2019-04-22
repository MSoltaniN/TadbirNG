import { RTL } from "@progress/kendo-angular-l10n";
import { Layout, ColumnVisibility } from "../../../../environments/environment";
import { Component, OnInit, Host, ElementRef, OnDestroy, Input, Output, EventEmitter } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from '@ngx-translate/core';
import { BaseComponent } from "../../../class/base.component";
import { GridComponent } from "@progress/kendo-angular-grid";
import { DefaultComponent } from "../../../class/default.component";
import { ListFormViewConfig } from "../../../model/listFormViewConfig";
import { ColumnViewDeviceConfig } from "../../../model/columnViewDeviceConfig";
import { ColumnViewConfig } from "../../../model/columnViewConfig";
import { SettingService } from "../../../service/index";
import { ListFormViewConfigInfo, SettingViewModelInfo } from "../../../service/settings.service";
import { ViewName } from "../../../security/viewName";
import { Property } from "../../../class/metadata/property";
import { async } from "q";


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'auto-generated-grid-setting',
  templateUrl: './auto-generated-grid-setting.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class AutoGeneratedGridSettingComponent extends BaseComponent implements OnInit, OnDestroy {

  show: boolean;
  rtl: boolean;
  viewId: number;
  entityName: string;
  currentSetting: ListFormViewConfig;

  /**
   * نوع آدرس خواندن متا دیتا را مشخص میکند
   * 1: آدرس متادیتا در کنترلر موجودیت
   * 2: آدرس گرفتن متادیتا در کنترلر متادیتا
  */
  @Input() metadataUrlType: number;

  @Input() public set entityTypeName(entityName: string) {
    this.entityName = entityName;
    this.viewId = ViewName[entityName];
  }

  testMetaType: string;

  @Input() public set metadataType(metaType: string) {
    this.testMetaType = metaType;

    this.rowData = null;
    this.gridRowData = null;
    this.gridColumn = [];
    this.allGridColumn = [];

    (async () => {
      switch (this.metadataUrlType) {
        case 1:
          this.allGridColumn = await this.defaultComponent.getAllMetaDataAsync(metaType);
          break;
        case 2:
          this.allGridColumn = await this.defaultComponent.getAllMetaDataByViewIdAsync(this.viewId, metaType);
          break;
        default:
      }

      this.loadSetting();

    })();
  }


  @Input() public displayBtnSetting: boolean = true;

  @Output() columnsList: EventEmitter<any> = new EventEmitter();


  public rowData: ListFormViewConfig | null = null;
  public gridRowData: Array<SettingViewModelInfo> | null = null;

  /**
   *ستون های قابل نمایش در گرید
   */
  gridColumn: Array<any> = [];
  /**
 *تمام ستون های گرید که در کش ذخیره شده یا از سرویس خوانده میشوند
 */
  allGridColumn: Array<Property> = [];


  constructor(public toastrService: ToastrService, public translate: TranslateService, public settingService: SettingService,
    @Host() private grid: GridComponent, private elRef: ElementRef, @Host() public defaultComponent: DefaultComponent) {

    super(toastrService);
  }

  ngOnDestroy() {

    if (!this.viewId)
      return;

    var currentSetting = this.settingService.getSettingByViewId(this.viewId)

    if (currentSetting)
      this.settingService.putUserSettings(this.UserId, currentSetting).subscribe(response => {

      }, (error => {

      }));

  }

  ngOnInit() { }


  /** چپ چین کردن دکمه تنظیمات و لود کردن ستون ها در گرید */
  private loadSetting() {

    if (this.CurrentLanguage == 'fa')
      this.rtl = true;
    else
      this.rtl = false;

    if (!this.viewId)
      return;

    this.currentSetting = this.getCurrentSetting();

    this.setSettingGridRow();

    var size = this.screenSize;
    if (this.currentSetting) {      

      this.rowData = this.currentSetting;

      this.currentSetting.columnViews.forEach(item => {
        var columnSetting = item[size];
        if (columnSetting.visibility == ColumnVisibility.AlwaysVisible ||
          columnSetting.visibility == ColumnVisibility.Visible ||
          columnSetting.visibility == ColumnVisibility.Default) {
          var colItem = this.allGridColumn.filter(f => f.name.toLowerCase() == item.name.toLowerCase());
          if (colItem.length > 0) {
            this.gridColumn.push(colItem[0]);
          }
        }
      })

      this.allGridColumn.forEach(item => {

        var colItem = JSON.parse(item.settings);

        if (colItem[size].visibility == ColumnVisibility.AlwaysVisible &&
          this.gridColumn.filter(f => f.name.toLowerCase() == item.name.toLowerCase()).length == 0) {
          this.gridColumn.push(item);
        }

      })

    }
    else {

      this.rowData = new ListFormViewConfigInfo(this.viewId, 10);

      this.allGridColumn.forEach(item => {
        if (this.gridRowData.filter(f => f.name.toLowerCase() == item.name.toLowerCase()).length > 0) {
          this.gridColumn.push(item);
        }
        this.rowData.columnViews.push(JSON.parse(item.settings));
      })
    }


    this.emitGridColumns();
  }

  /**
   * تنظیمات ذخیره شده کاربر را مرتب میکند
   * @param currentSetting
   */
  getCurrentSetting(): ListFormViewConfig {
    var size = this.screenSize;
    this.currentSetting = this.settingService.getSettingByViewId(this.viewId);
    if (this.currentSetting) {
      this.currentSetting.columnViews.sort((obj1, obj2) => {

        if (obj1[size].index > obj2[size].index) {
          return 1;
        }

        if (obj1[size].index < obj2[size].index) {
          return -1;
        }

        return 0;
      });

      this.settingService.setSettingByViewId(this.viewId, this.currentSetting);

      return this.currentSetting;
    }
  }


  /**
   * سطرهای اطلاعاتی گرید تنظیمات را بدست می آورد
   * @param currentSetting
   */
  setSettingGridRow() {

    this.gridRowData = [];

    var size = this.screenSize;

    this.allGridColumn.forEach(item => {
      var itemSetting: ColumnViewConfig = JSON.parse(item.settings);
      var rowItem = new SettingViewModelInfo();

      rowItem.name = itemSetting.name;

      var columnView: ColumnViewDeviceConfig;
      columnView = itemSetting[size];

      if (columnView.visibility != ColumnVisibility.AlwaysHidden) {
        rowItem.designIndex = columnView.designIndex;
        rowItem.index = columnView.index;
        //rowItem.title = columnView.title;
        rowItem.width = columnView.width;
        rowItem.visibility = columnView.visibility == ColumnVisibility.AlwaysVisible ||
          columnView.visibility == ColumnVisibility.Visible || columnView.visibility == ColumnVisibility.Default ?
          true : false;
        rowItem.disabled = columnView.visibility == ColumnVisibility.AlwaysVisible ? true : false;

        if (this.currentSetting) {
          var currentSettingItem = this.currentSetting.columnViews.filter(f => f.name.toLowerCase() == rowItem.name.toLowerCase());
          if (currentSettingItem.length > 0 && (currentSettingItem[0][size].visibility == ColumnVisibility.Hidden || currentSettingItem[0][size].visibility == ColumnVisibility.AlwaysHidden)) {
            rowItem.visibility = false;
          }
        }

        var parts = item.name.split('.');
        for (var i = 0; i < parts.length; i++) {
          parts[i] = parts[i].charAt(0).toUpperCase() + parts[i].slice(1);
        }
        var key = this.entityName + "." + parts.join('.');
        rowItem.title = key;
        this.gridRowData.push(JSON.parse(JSON.stringify(rowItem)));

        //this.translate.get(key).subscribe((msg: string) => {
        //  rowItem.title = msg;

        //  this.gridRowData.push(JSON.parse(JSON.stringify(rowItem)));
        //});

      }
    })

    this.gridRowData.sort((obj1, obj2) => {
      if (obj1.index > obj2.index) {
        return 1;
      }

      if (obj1.index < obj2.index) {
        return -1;
      }

      return 0;
    });
  }


  /**
   * رویداد نمایش یا عدم نمایش ستون در گرید و ذخیره آن در حافظه مرورگر
   * @param name نام ستون مربوطه در گرید
   * @param event پارامتر رویداد
   */
  changeVisibility(dataItem: SettingViewModelInfo, event: any) {

    if (!this.viewId)
      return;

    var currentSetting = this.getCurrentSetting();
    if (currentSetting)
      this.rowData = currentSetting;

    var hidden: boolean = !event.target.checked;

    var rowItemIndex = this.gridRowData.indexOf(dataItem);

    this.gridRowData[rowItemIndex].visibility = !hidden;

    var size = this.screenSize;


    if (hidden) {
      this.gridColumn = this.gridColumn.filter(f => f.name.toLowerCase() != dataItem.name.toLowerCase());

      var rowdataItem = this.rowData.columnViews.filter(f => f.name.toLowerCase() == dataItem.name.toLowerCase());
      if (rowdataItem.length > 0) {
        rowdataItem[0][size].visibility = ColumnVisibility.Hidden;
      }
    }
    else {
      var item = this.allGridColumn.filter(f => f.name.toLowerCase() == dataItem.name.toLowerCase());
      if (item.length > 0) {
        this.gridColumn.push(item[0]);

        var rowdataItem = this.rowData.columnViews.filter(f => f.name.toLowerCase() == dataItem.name.toLowerCase());
        if (rowdataItem.length > 0) {
          rowdataItem[0][size].visibility = ColumnVisibility.Visible;
        }
      }
    }

    if (this.rowData) {
      this.settingService.setSettingByViewId(this.viewId, this.rowData);
    }

    this.emitGridColumns();
  }

  /**
   *لیست ستون های گرید را مرتب میکند و برای نمایش به گرید میفرستد
   * */
  emitGridColumns() {

    this.currentSetting = this.getCurrentSetting();

    if (this.currentSetting) {
      let gridColumn: any[] = [];
      this.currentSetting.columnViews.forEach(item => {
        var column = this.gridColumn.find(f => f.name.toLowerCase() == item.name.toLowerCase());
        if (column)
          gridColumn.push(column);
      })

      this.columnsList.emit(gridColumn);
    }
    else {
      this.columnsList.emit(this.gridColumn);
    }
  }

  /** نمایش فرم تنظیمات گرید */
  public showSetting() {
    this.show = true;
  }

  /** بستن فرم تنظیمات گرید */
  public closeSetting() {
    this.show = false;
  }


}

