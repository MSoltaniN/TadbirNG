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
import { ListFormViewConfigInfo } from "../../../service/settings.service";


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

        var currentColumnViewDevice: ColumnViewDeviceConfig = columnViewDevice.medium;
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
        if (rowDataString)
            this.rowData = JSON.parse(rowDataString);
        else {
            this.rowData = new ListFormViewConfigInfo(parseInt(viewId), 10);
        }

        this.grid.leafColumns.toArray().forEach((item, index, arr) => {
            
            if (item instanceof ColumnComponent) {

                if (this.rowData) {
                    var arrayIndex = this.rowData.columnViews.findIndex(p => p.name == (<ColumnComponent>item).field)
                    var arrayItem: ColumnViewConfig | null = null;
                    if (arrayIndex >= 0)
                        arrayItem = this.rowData.columnViews[arrayIndex];


                    
                    if (arrayItem) {
                        //var row: ColumnViewDeviceConfig = { index: arrayIndex, designIndex: item.orderIndex, visibilty: ColumnVisibility.AlwaysVisible };
                        var columnViewDeviceConfig: ColumnViewDeviceConfig = this.getCurrentColumnViewConfig(arrayItem);

                        //columnViewDeviceConfig.
                        //columnViewDeviceConfig.visibilty = columnViewDeviceConfig.visibilty;

                        if (columnViewDeviceConfig.visibilty == ColumnVisibility.AlwaysHidden || columnViewDeviceConfig.visibilty == ColumnVisibility.Hidden)
                            item.hidden = true;
                        else
                            item.hidden = false;

                        //var hiddenColumns = this.rowData.filter(p => p.visibility == false);

                        //if (row.visibility && hiddenColumns.length == this.rowData.length - 1)
                        //    row.disabled = true;

                        this.rowData.columnViews[arrayIndex] = this.setCurrentColumnViewConfig(this.rowData.columnViews[arrayIndex], columnViewDeviceConfig);

                    }
                    else {
                        
                        var row: ColumnViewDeviceConfig = { index: index, designIndex: item.orderIndex, visibilty: ColumnVisibility.AlwaysVisible };
                        var colView: ColumnViewConfigInfo = new ColumnViewConfigInfo((<ColumnComponent>item).field);

                        colView = this.setCurrentColumnViewConfig(colView, row);                       
                        this.rowData.columnViews.push(colView);
                    }
                }
            }
        });
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

                            //var hiddenColumns = this.rowData.filter(p => p.visibility == false);
                            //if (hiddenColumns.length == this.rowData.length - 1) {
                            //    var lastVisibleColumn = this.rowData.findIndex(p => p.visibility == true);
                            //    this.rowData[lastVisibleColumn].disabled = true;
                            //}
                            //else {
                            //    var lastDisableColumn = this.rowData.findIndex(p => p.disabled == true);
                            //    if (lastDisableColumn >= 0)
                            //        this.rowData[lastDisableColumn].disabled = false;

                            //}

                            var columnViewConfig = this.setCurrentColumnViewConfig(this.rowData.columnViews[arrayIndex], arrayItem);
                            
                            this.rowData.columnViews[arrayIndex] = columnViewConfig;
                        }
                    }
                }
            }
        });

        localStorage.setItem(storageId, JSON.stringify(this.rowData));

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