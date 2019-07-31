import { RTL } from "@progress/kendo-angular-l10n";
import { Layout, ColumnVisibility } from "../../../../environments/environment";
import { Component, OnInit, Host, ElementRef, OnDestroy, Input, Output, EventEmitter } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from '@ngx-translate/core';
import { BaseComponent } from "../../../class/base.component";
import { GridComponent } from "@progress/kendo-angular-grid";
import { DefaultComponent } from "../../../class/default.component";
import { ColumnViewDeviceConfig } from "../../../model/columnViewDeviceConfig";
import { ColumnViewConfig } from "../../../model/columnViewConfig";
import { SettingService } from "../../../service/index";
import {  SettingViewModelInfo, QuickSearchConfigInfo, QuickSearchColumnConfigInfo } from "../../../service/settings.service";
import { ViewName } from "../../../security/viewName";
import { Property } from "../../../class/metadata/property";
import { QuickSearchConfig } from "../../../model/index";
import { BrowserStorageService } from "../../../service/browserStorage.service";


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'selectForm-grid-setting',
  templateUrl: './selectForm-grid-setting.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class SelectFormGridSettingComponent extends BaseComponent implements OnInit, OnDestroy {

  show: boolean;
  rtl: boolean;
  viewId: number;
  entityName: string;
  currentSetting: QuickSearchConfig;
  size: any;

  @Input() public set entityTypeName(entityName: string) {

    if (this.viewId)
      this.saveChangesSettings();

    this.entityName = entityName;
    this.viewId = ViewName[entityName];
    this.size = this.screenSize;

    this.rowData = null;
    this.gridRowData = null;
    this.gridColumn = [];
    this.allGridColumn = [];

    (async () => {
      if (this.viewId) {
        this.allGridColumn = await this.defaultComponent.getAllMetaDataByViewIdAsync(this.viewId);
        this.currentSetting = await this.settingService.getQuickSearchSettingsByUserAndViewAsync(this.UserId, this.viewId);

        this.loadSetting();
      }
    })();
  }

  @Input() public displayBtnSetting: boolean = true;

  @Output() columnsList: EventEmitter<any> = new EventEmitter();
  @Output() quickSearch: EventEmitter<QuickSearchConfig> = new EventEmitter();

  public rowData: QuickSearchConfig | null = null;
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
    @Host() private grid: GridComponent, private elRef: ElementRef, @Host() public defaultComponent: DefaultComponent, public bStorageService: BrowserStorageService) {

    super(toastrService, bStorageService);
  }

  ngOnDestroy() {

    this.saveChangesSettings();

  }

  ngOnInit() { }


  /** چپ چین کردن دکمه تنظیمات و لود کردن ستون ها در گرید */
  private loadSetting() {

    if (this.CurrentLanguage == 'fa')
      this.rtl = true;
    else
      this.rtl = false;

    this.sortCurrentSetting();

    this.setSettingGridRow();

    if (this.currentSetting && this.currentSetting.columns.length > 0) {

      this.rowData = this.currentSetting;

      this.currentSetting.columns.forEach(item => {

        if (item.isDisplayed) {
          var colItem = this.allGridColumn.find(f => f.name.toLowerCase() == item.name.toLowerCase());
          if (colItem) {

            //#region جایگزینی تنظیمات ذخیره شده با تنظیمات پیش فرض
            var colSetting = JSON.parse(colItem.settings);
            colSetting[this.size].visibility = ColumnVisibility.Visible;
            colItem.settings = JSON.stringify(colSetting);
            //#endregion
            this.gridColumn.push(colItem);
          }
        }
      })

      this.allGridColumn.forEach(item => {

        var colItem = JSON.parse(item.settings);

        if (colItem[this.size].visibility == ColumnVisibility.AlwaysVisible &&
          this.gridColumn.filter(f => f.name.toLowerCase() == item.name.toLowerCase()).length == 0) {
          this.gridColumn.push(item);
        }

      })

    }
    else {

      this.rowData = new QuickSearchConfigInfo(this.viewId, "Contains");

      this.allGridColumn.forEach(item => {
        var settingGridRow = this.gridRowData.find(f => f.name.toLowerCase() == item.name.toLowerCase())
        var qsColumn = new QuickSearchColumnConfigInfo();
        if (settingGridRow) {
          this.gridColumn.push(item);

          qsColumn.name = item.name;
          qsColumn.title = settingGridRow.title;
          qsColumn.displayIndex = settingGridRow.designIndex;
          qsColumn.isDisplayed = settingGridRow.visibility;
          qsColumn.isSearched = settingGridRow.isSearched;

        }
        this.rowData.columns.push(qsColumn);
      })
    }


    this.emitGridColumns();
  }

  /**
   * تنظیمات ذخیره شده کاربر را مرتب میکند
   */
  sortCurrentSetting() {
    if (this.currentSetting) {
      this.currentSetting.columns.sort((obj1, obj2) => {

        if (obj1.displayIndex > obj2.displayIndex) {
          return 1;
        }

        if (obj1.displayIndex < obj2.displayIndex) {
          return -1;
        }

        return 0;
      });
    }
  }


  /**
   * سطرهای اطلاعاتی گرید تنظیمات را بدست می آورد
   * @param currentSetting
   */
  setSettingGridRow() {

    this.gridRowData = [];

    this.allGridColumn.forEach(item => {
      var itemSetting: ColumnViewConfig = JSON.parse(item.settings);
      var rowItem = new SettingViewModelInfo();

      rowItem.name = itemSetting.name;

      var columnView: ColumnViewDeviceConfig;
      columnView = itemSetting[this.size];

      if (columnView.visibility != ColumnVisibility.AlwaysHidden) {
        rowItem.designIndex = columnView.designIndex;
        rowItem.index = columnView.index;
        rowItem.title = columnView.title;
        rowItem.width = columnView.width;
        rowItem.visibility = columnView.visibility == ColumnVisibility.AlwaysVisible ||
          columnView.visibility == ColumnVisibility.Visible || columnView.visibility == ColumnVisibility.Default ?
          true : false;
        rowItem.disabled = columnView.visibility == ColumnVisibility.AlwaysVisible ? true : false;

        if (this.currentSetting) {
          var currentSettingItem = this.currentSetting.columns.find(f => f.name.toLowerCase() == rowItem.name.toLowerCase());
          if (currentSettingItem) {
            rowItem.visibility = currentSettingItem.isDisplayed;
            rowItem.isSearched = currentSettingItem.isSearched;
          }
        }

        this.gridRowData.push(JSON.parse(JSON.stringify(rowItem)));
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

    if (this.currentSetting)
      this.rowData = this.currentSetting;

    var hidden: boolean = !event.target.checked;
    var rowItemIndex = this.gridRowData.indexOf(dataItem);
    this.gridRowData[rowItemIndex].visibility = !hidden;

    if (hidden) {
      this.gridColumn = this.gridColumn.filter(f => f.name.toLowerCase() != dataItem.name.toLowerCase());
    }
    else {
      var item = this.allGridColumn.find(f => f.name.toLowerCase() == dataItem.name.toLowerCase());
      if (item) {
        this.gridColumn.push(item);
      }
    }

    var rowdataItem = this.rowData.columns.find(f => f.name.toLowerCase() == dataItem.name.toLowerCase());
    if (rowdataItem) {
      rowdataItem.isDisplayed = event.target.checked;
    }
    else {
      var column = new QuickSearchColumnConfigInfo();
      column.name = dataItem.name;
      column.title = dataItem.title;
      column.displayIndex = dataItem.designIndex;
      column.isDisplayed = dataItem.visibility;
      column.isSearched = dataItem.isSearched;
      this.rowData.columns.push(column);
    }

    if (this.rowData) {
      this.currentSetting = this.rowData;
      this.settingService.setLocalQuickSearchSettings(this.UserId, this.viewId, this.rowData);
    }

    this.emitGridColumns();
  }

  changeIsSearched(dataItem: SettingViewModelInfo, event: any) {

    if (!this.viewId)
      return;

    if (this.currentSetting)
      this.rowData = this.currentSetting;

    var hidden: boolean = !event.target.checked;
    var rowItemIndex = this.gridRowData.indexOf(dataItem);
    this.gridRowData[rowItemIndex].isSearched = !hidden;

    var rowdataItem = this.rowData.columns.find(f => f.name.toLowerCase() == dataItem.name.toLowerCase());
    if (rowdataItem) {
      rowdataItem.isSearched = event.target.checked;
    }
    else {
      var column = new QuickSearchColumnConfigInfo();
      column.name = dataItem.name;
      column.title = dataItem.title;
      column.displayIndex = dataItem.designIndex;
      column.isDisplayed = dataItem.visibility;
      column.isSearched = dataItem.isSearched;
      this.rowData.columns.push(column);
    }

    if (this.rowData) {
      this.currentSetting = this.rowData
      this.settingService.setLocalQuickSearchSettings(this.UserId, this.viewId, this.rowData);
    }

    this.quickSearch.emit(this.currentSetting);

  }

  gettextSearchModeState(mode: string) {
    if (this.currentSetting.searchMode == mode)
      return true;
    return false;
  }

  onChangeTxtSearchMode(mode: string) {    
    this.currentSetting.searchMode = mode;

    this.quickSearch.emit(this.currentSetting);

    this.settingService.setLocalQuickSearchSettings(this.UserId, this.viewId, this.currentSetting);
  }

  /**
   *لیست ستون های گرید را مرتب میکند و برای نمایش به گرید میفرستد
   * */
  emitGridColumns() {
    this.columnsList.emit(this.gridColumn);

    this.quickSearch.emit(this.currentSetting);
  }

  saveChangesSettings() {

    if (!this.viewId)
      return;

    var currentSetting = this.settingService.getLocalQuickSearchSettings(this.UserId, this.viewId)

    if (currentSetting)
      this.settingService.putUserQuickSearchSettings(this.UserId, currentSetting).subscribe(response => {
      }, (error => {

      }));
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

