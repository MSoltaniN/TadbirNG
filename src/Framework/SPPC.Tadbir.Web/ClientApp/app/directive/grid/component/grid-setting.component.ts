import { RTL } from "@progress/kendo-angular-l10n";
import { Layout, ColumnVisibility } from "../../../enviroment";
import { Component, OnInit, ViewContainerRef, Host, ElementRef, OnDestroy } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from "ng2-translate";
import { SppcLoadingService } from "../../../controls/sppcLoading/index";
import { BaseComponent } from "../../../class/base.component";
import { GridDataResult, GridComponent, ColumnComponent } from "@progress/kendo-angular-grid";
import { DefaultComponent } from "../../../class/default.component";
import { ListFormViewConfig } from "../../../model/listFormViewConfig";
import { ColumnViewDeviceConfig } from "../../../model/columnViewDeviceConfig";
import { ColumnViewConfig } from "../../../model/columnViewConfig";
import { ColumnViewDeviceConfigInfo, ColumnViewConfigInfo } from "../../../service/index";
import { ListFormViewConfigInfo, SettingViewModelInfo } from "../../../service/settings.service";


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

    public rowData: ListFormViewConfig | null = null;
    public gridRowData: Array<SettingViewModelInfo> | null = null;
    //public rowData: GridDataResult;

    constructor(public toastrService: ToastrService, public translate: TranslateService,
        @Host() private grid: GridComponent, private elRef: ElementRef, @Host() public defaultComponent: DefaultComponent) {

        super(toastrService);
    }

    ngOnDestroy() {
        console.log('ondestroy called');

    }

    ngOnInit() {

        this.loadSetting();        
    }

    /** براساس سایز تنظیمات را از انتیتی ارسالی به تابع برمیگرداند */
    private getCurrentColumnViewConfig(columnViewDevice: ColumnViewConfig): ColumnViewDeviceConfig {

        var currentColumnViewDevice: ColumnViewDeviceConfig = columnViewDevice.medium;
        switch (this.media) {
            case "xs":                
                currentColumnViewDevice = columnViewDevice.extraSmall;
                break;
            case "sm":
                currentColumnViewDevice = columnViewDevice.small;
                break;
            case "md":
                currentColumnViewDevice = columnViewDevice.medium;
                break;
            case "l":
                currentColumnViewDevice = columnViewDevice.large;
                break;
            case "el":
                currentColumnViewDevice = columnViewDevice.extraLarge;
                break;
        }

        
        return currentColumnViewDevice;
    }


    /** براساس سایز ، تنظیمات را مقدار دهی و به تابع برمیگرداند */
    private setCurrentColumnViewConfig(columnViewDevice: ColumnViewConfig, value: ColumnViewDeviceConfig): ColumnViewConfig {

        var currentColumnViewDevice: ColumnViewConfig = columnViewDevice;
        switch (this.media) {
            case "xs":
                columnViewDevice.extraSmall = value;
                break;
            case "sm":
                columnViewDevice.small = value;
                break;
            case "md":
                columnViewDevice.medium = value;
                break;
            case "l":
                columnViewDevice.large = value;
                break;
            case "el":
                columnViewDevice.extraLarge = value;
                break;
        }

        return columnViewDevice;
    }

    private fillViewModel(rowData: ListFormViewConfig): Array<SettingViewModelInfo> {
        var rows: Array<SettingViewModelInfo> = new Array<SettingViewModelInfo>();

        rowData.columnViews.forEach((item) => {
            var model = new SettingViewModelInfo();
            var setting = this.getCurrentColumnViewConfig(item);
            if (setting) {
                
                model.designIndex = setting.designIndex;
                model.index = setting.index;
                model.visibilty = this.checkVisibility(setting.visibilty);
                model.width = setting.width;
                model.name = item.name;

                rows.push(model);
            }
        });


        return rows;

    }


    private checkVisibility(visibility : string): boolean {
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
        var storageId: string = this.grid.wrapper.nativeElement.id + this.defaultComponent.UserId;
        if (this.grid.wrapper.nativeElement.id == "") return;

        var viewId: string = this.grid.wrapper.nativeElement.id
        
        var rowDataString = localStorage.getItem(storageId);
        if (rowDataString) {
            this.rowData = JSON.parse(rowDataString);
            
        }
        else {
            this.rowData = new ListFormViewConfigInfo(parseInt(viewId), 10);
            this.gridRowData = null;           
        }

        //#region change column in runtime and fill ro data from desgined grid
        this.grid.leafColumns.toArray().forEach((item, index, arr) => {

            if (item instanceof ColumnComponent) {

                if (this.rowData) {
                    var arrayIndex = this.rowData.columnViews.findIndex(p => p.name == (<ColumnComponent>item).field)
                    var arrayItem: ColumnViewConfig | null = null;
                    if (arrayIndex >= 0)
                        arrayItem = this.rowData.columnViews[arrayIndex];

                    var columnViewDeviceConfig: ColumnViewDeviceConfig | undefined = undefined;
                    if (arrayItem)
                        columnViewDeviceConfig = this.getCurrentColumnViewConfig(arrayItem);

                    if (columnViewDeviceConfig && columnViewDeviceConfig != undefined) {
                        //var row: ColumnViewDeviceConfig = { index: arrayIndex, designIndex: item.orderIndex, visibilty: ColumnVisibility.AlwaysVisible };
                        

                        if (columnViewDeviceConfig != undefined) {
                            item.hidden = !this.checkVisibility(columnViewDeviceConfig.visibilty);

                            this.rowData.columnViews[arrayIndex] = this.setCurrentColumnViewConfig(this.rowData.columnViews[arrayIndex], columnViewDeviceConfig);
                        }                       

                    }
                    else {

                        var row: ColumnViewDeviceConfig = { index: index, designIndex: item.orderIndex, visibilty: ColumnVisibility.AlwaysVisible };

                        var colView: ColumnViewConfigInfo = new ColumnViewConfigInfo((<ColumnComponent>item).field);
                        var existIndex = this.rowData.columnViews.findIndex(p => p.name == colView.name);
                        if (existIndex > -1)
                            colView = this.rowData.columnViews[existIndex];

                        colView = this.setCurrentColumnViewConfig(colView, row);                        
                        
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
            this.gridRowData = this.changeLastColumns(this.fillViewModel(this.rowData));
        }

        
    }

    /**
     * رویداد نمایش یا عدم نمایش ستون در گرید و ذخیره آن در حافظه مرورگر
     * @param name نام ستون مربوطه در گرید
     * @param event پارامتر رویداد
     */
    changeVisibility(name :string, event : any) {


        //var id: string = this.grid.wrapper.nativeElement.id + "_" + this.defaultComponent.UserId  + "_hidden";
        var storageId: string = this.grid.wrapper.nativeElement.id + this.defaultComponent.UserId;
        if (this.grid.wrapper.nativeElement.id == "") return;

        var viewId: string = this.grid.wrapper.nativeElement.id

        var hidden: boolean;

        

        hidden = !event.target.checked;
        
        this.grid.columns.toArray().forEach((item, index, arr) => {
            if (item instanceof ColumnComponent) {
                if ((<ColumnComponent>item).field == name) {
                    item.hidden = hidden;
                    if (this.rowData) {
                        this.rowData.viewId = parseInt(storageId);
                        var arrayIndex = this.rowData.columnViews.findIndex(p => p.name == (<ColumnComponent>item).field);

                        var arrayItem: ColumnViewDeviceConfig | null = null;
                        if (arrayIndex >= 0)
                            arrayItem = this.getCurrentColumnViewConfig(this.rowData.columnViews[arrayIndex]);

                        

                        if (arrayItem) {

                            arrayItem.visibilty = hidden == true ? ColumnVisibility.Hidden : ColumnVisibility.Visible;
                            
                            var columnViewConfig = this.setCurrentColumnViewConfig(this.rowData.columnViews[arrayIndex], arrayItem);
                            
                            this.rowData.columnViews[arrayIndex] = columnViewConfig;
                        }
                    }
                }
            }
        });

        localStorage.setItem(storageId, JSON.stringify(this.rowData));

        if (this.rowData) {
            this.gridRowData = this.changeLastColumns(this.fillViewModel(this.rowData));
        }
    }


    private changeLastColumns(rows: Array<SettingViewModelInfo>): Array<SettingViewModelInfo> {

        var hiddenColumns = rows.filter(p => p.visibilty == false);
        if (hiddenColumns.length == rows.length - 1) {
            var lastVisibleColumn = rows.findIndex(p => p.visibilty == true);
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

