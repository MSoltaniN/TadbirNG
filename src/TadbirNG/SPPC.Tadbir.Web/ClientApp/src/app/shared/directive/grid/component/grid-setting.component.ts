import { RTL } from "@progress/kendo-angular-l10n";
import { Component, OnInit, Host, ElementRef, OnDestroy, Input } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from '@ngx-translate/core';
import { GridComponent, ColumnComponent } from "@progress/kendo-angular-grid";
import { Layout, ColumnVisibility } from "@sppc/shared/enum/metadata";
import { ListFormViewConfig, ColumnViewConfig, ColumnViewDeviceConfig } from "@sppc/shared/models";
import { SettingViewModelInfo, SettingService, ListFormViewConfigInfo, ColumnViewConfigInfo } from "@sppc/config/service";
import { BaseComponent, DefaultComponent } from "@sppc/shared/class";
import { BrowserStorageService } from "@sppc/shared/services";


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'grid-setting',
  templateUrl: './grid-setting.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class GridSettingComponent extends BaseComponent implements OnInit, OnDestroy {

  show: boolean;

  rtl: boolean;

  @Input() public entityTypeName: string;

  public rowData: ListFormViewConfig | null = null;
  public gridRowData: Array<SettingViewModelInfo> | null = null;
  //public rowData: GridDataResult;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public settingService: SettingService, public bStorageService: BrowserStorageService,
    @Host() private grid: GridComponent, private elRef: ElementRef, @Host() public defaultComponent: DefaultComponent) {

    super(toastrService, bStorageService);
  }

  ngOnDestroy() {

    //#region Save View Setting
    var viewId: number = parseInt(this.grid.wrapper.nativeElement.id);
    var currentSetting = this.settingService.getSettingByViewId(viewId)

    if (currentSetting)
      this.settingService.putUserSettings(this.UserId, currentSetting).subscribe(response => {

      }, (error => {

      }));

    //#endregion

  }

  ngOnInit() {
    this.loadSetting();
  }




  private fillViewModel(rowData: ListFormViewConfig): Array<SettingViewModelInfo> {
    var rows: Array<SettingViewModelInfo> = new Array<SettingViewModelInfo>();

    rowData.columnViews.forEach((item) => {
      var model = new SettingViewModelInfo();
      var setting = this.settingService.getCurrentColumnViewConfig(item);
      if (setting && setting.index && setting.visibility != ColumnVisibility.AlwaysHidden) {

        model.designIndex = setting.designIndex;
        model.index = setting.index;
        model.visibility = this.checkVisibility(setting.visibility);
        model.width = setting.width;
        model.name = item.name;

        var parts = item.name.split('.');

        for (var i = 0; i < parts.length; i++) {
          parts[i] = parts[i].charAt(0).toUpperCase() + parts[i].slice(1);
        }


        var title = "";
        //var key = this.entityTypeName + "." + item.name.charAt(0).toUpperCase() + item.name.slice(1);
        var key = this.entityTypeName + "." + parts.join('.');
        this.translate.get(key).subscribe((msg: string) => {
          title = msg;

          model.title = title;

          rows.push(model);
        });

        //console.log(rows);
      }
    });


    return rows;

  }


  private checkVisibility(visibility: string): boolean {
    if (visibility == 'AlwaysVisible' || visibility == 'Visible' || visibility == 'Default')
      return true;

    return false;
  }

  /** چپ چین کردن دکمه تنظیمات و لود کردن ستون ها در گرید */
  private loadSetting() {

    if (this.CurrentLanguage == 'fa')
      this.rtl = true;
    else
      this.rtl = false;

    //var id: string = this.grid.wrapper.nativeElement.id + "_" + this.defaultComponent.UserId + "_hidden";
    //var storageId: string = this.grid.wrapper.nativeElement.id + this.defaultComponent.UserId;
    if (this.grid.wrapper.nativeElement.id == "") return;

    var viewId: number = parseInt(this.grid.wrapper.nativeElement.id);

    var currentSetting = this.settingService.getSettingByViewId(viewId);

    if (currentSetting) {
      this.rowData = currentSetting;

    }
    else {
      this.rowData = new ListFormViewConfigInfo(viewId, 10);
    }

    var fields: Array<string> = new Array<string>();


    //#region change column in runtime and fill ro data from desgined grid
    this.grid.leafColumns.toArray().forEach((item, index, arr) => {

      if (item instanceof ColumnComponent) {

        fields.push(item.field);

        if (this.rowData) {
          var arrayIndex = this.rowData.columnViews.findIndex(p => p.name.toLowerCase() == (<ColumnComponent>item).field.toLowerCase())
          var arrayItem: ColumnViewConfig | null = null;
          if (arrayIndex >= 0)
            arrayItem = this.rowData.columnViews[arrayIndex];

          var columnViewDeviceConfig: ColumnViewDeviceConfig | undefined = undefined;
          if (arrayItem)
            columnViewDeviceConfig = this.settingService.getCurrentColumnViewConfig(arrayItem);

          if (columnViewDeviceConfig && columnViewDeviceConfig.index) {
            //var row: ColumnViewDeviceConfig = { index: arrayIndex, designIndex: item.orderIndex, visibilty: ColumnVisibility.AlwaysVisible };                        
            columnViewDeviceConfig.title = item.displayTitle;
            item.hidden = !this.checkVisibility(columnViewDeviceConfig.visibility);
            this.rowData.columnViews[arrayIndex] = this.settingService.setCurrentColumnViewConfig(this.rowData.columnViews[arrayIndex], columnViewDeviceConfig);

          }
          else {

            var visibilityValue = item.isVisible ? ColumnVisibility.AlwaysVisible : ColumnVisibility.AlwaysHidden;


            var row: ColumnViewDeviceConfig = {
              index: index, designIndex: item.orderIndex,
              visibility: visibilityValue, title: item.displayTitle,
              width: item.width
            };

            var colView: ColumnViewConfigInfo = new ColumnViewConfigInfo((<ColumnComponent>item).field);
            var existIndex = this.rowData.columnViews.findIndex(p => p.name.toLowerCase() == colView.name.toLowerCase());
            if (existIndex > -1)
              colView = this.rowData.columnViews[existIndex];

            colView = this.settingService.setCurrentColumnViewConfig(colView, row);

            if (existIndex > -1)
              this.rowData.columnViews[existIndex] = colView;
            else
              this.rowData.columnViews.push(colView);
          }



        }


      }
    });
    //#endregion

    if (this.rowData) {

      this.rowData.columnViews.forEach((item) => {
        if (!fields.find(p => p.toLowerCase() == item.name.toLowerCase()) && this.rowData) {
          var deletedIndex = this.rowData.columnViews.findIndex(p => p.name.toLowerCase() == item.name.toLowerCase());
          this.rowData.columnViews.splice(deletedIndex, 1);
        }

      });
    }

    if (this.rowData) {
      this.gridRowData = this.changeLastColumns(this.fillViewModel(this.rowData));
    }


  }

  /**
   * رویداد نمایش یا عدم نمایش ستون در گرید و ذخیره آن در حافظه مرورگر
   * @param name نام ستون مربوطه در گرید
   * @param event پارامتر رویداد
   */
  changeVisibility(name: string, event: any) {


    //var id: string = this.grid.wrapper.nativeElement.id + "_" + this.defaultComponent.UserId  + "_hidden";
    var storageId: string = this.grid.wrapper.nativeElement.id + this.defaultComponent.UserId;
    if (this.grid.wrapper.nativeElement.id == "") return;

    var viewId: number = parseInt(this.grid.wrapper.nativeElement.id)

    var hidden: boolean;



    hidden = !event.target.checked;

    this.grid.columns.toArray().forEach((item, index, arr) => {
      if (item instanceof ColumnComponent) {
        if ((<ColumnComponent>item).field && (<ColumnComponent>item).field.toLowerCase() == name.toLowerCase()) {
          item.hidden = hidden;
          if (this.rowData) {
            this.rowData.viewId = viewId;
            var arrayIndex = this.rowData.columnViews.findIndex(p => p.name.toLowerCase() == (<ColumnComponent>item).field.toLowerCase());

            var arrayItem: ColumnViewDeviceConfig | null = null;
            if (arrayIndex >= 0)
              arrayItem = this.settingService.getCurrentColumnViewConfig(this.rowData.columnViews[arrayIndex]);



            if (arrayItem) {

              arrayItem.visibility = hidden == true ? ColumnVisibility.Hidden : ColumnVisibility.Visible;

              var columnViewConfig = this.settingService.setCurrentColumnViewConfig(this.rowData.columnViews[arrayIndex], arrayItem);

              this.rowData.columnViews[arrayIndex] = columnViewConfig;
            }
          }
        }
      }
    });


    if (this.rowData) {
      this.settingService.setSettingByViewId(viewId, this.rowData);
      this.gridRowData = this.changeLastColumns(this.fillViewModel(this.rowData));
    }



  }


  private changeLastColumns(rows: Array<SettingViewModelInfo>): Array<SettingViewModelInfo> {

    var hiddenColumns = rows.filter(p => p.visibility == false);
    if (hiddenColumns.length == rows.length - 1) {
      var lastVisibleColumn = rows.findIndex(p => p.visibility == true);
      rows[lastVisibleColumn].disabled = true;
    }
    else {
      var lastDisableColumn = rows.findIndex(p => p.disabled == true);
      if (lastDisableColumn >= 0)
        rows[lastDisableColumn].disabled = false;

    }

    return rows;
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

