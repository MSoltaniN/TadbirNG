import { RTL } from "@progress/kendo-angular-l10n";
import { Layout } from "../../../enviroment";
import { Component, OnInit, ViewContainerRef, Host, ElementRef } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from "ng2-translate";
import { SppcLoadingService } from "../../../controls/sppcLoading/index";
import { BaseComponent } from "../../../class/base.component";
import { GridDataResult, GridComponent, ColumnComponent } from "@progress/kendo-angular-grid";
import { DefaultComponent } from "../../../class/default.component";


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

export class GridSettingComponent extends BaseComponent implements OnInit {

    show: boolean;

    rtl: boolean;

    public rowData: any[] = [];
    //public rowData: GridDataResult;

    constructor(public toastrService: ToastrService, public translate: TranslateService,
        @Host() private grid: GridComponent, private elRef: ElementRef, @Host() public defaultComponent: DefaultComponent) {

        super(toastrService);
    }

    ngOnInit() {

        this.loadSetting();        
    }


    /** چپ چین کردن دکمه تنظیمات و لود کردن ستون ها در گرید */
    private loadSetting() {

        if (this.CurrentLanguage == 'fa')
            this.rtl = true;
        else
            this.rtl = false;

        var id: string = this.elRef.nativeElement.id + "_" + this.defaultComponent.UserId + "_hidden";

        var rowDataString = localStorage.getItem(id);
        if (rowDataString)
            this.rowData = JSON.parse(rowDataString);

        this.grid.leafColumns.toArray().forEach((item, index, arr) => {
            
            if (item instanceof ColumnComponent) {
                var arrayIndex = this.rowData.findIndex(p => p.field == (<ColumnComponent>item).field)
                var arrayItem: any = null;
                if (arrayIndex >= 0)
                    arrayItem = this.rowData[arrayIndex];


                var row = { visibility: true, name: item.displayTitle, field: (<ColumnComponent>item).field, disabled: false };
                if (arrayItem) {
                    row.visibility = arrayItem.visibility;
                    
                    item.hidden = !row.visibility;

                    var hiddenColumns = this.rowData.filter(p => p.visibility == false);

                    if (row.visibility && hiddenColumns.length == this.rowData.length - 1)
                        row.disabled = true;

                    this.rowData[arrayIndex] = row;

                }
                else
                    this.rowData.push(row);
            }
        });
    }

    /**
     * رویداد نمایش یا عدم نمایش ستون در گرید و ذخیره آن در حافظه مرورگر
     * @param name نام ستون مربوطه در گرید
     * @param event پارامتر رویداد
     */
    changeVisibility(name :string, event : any) {


        var id: string = this.elRef.nativeElement.id + "_" + this.defaultComponent.UserId  + "_hidden";
        var hidden: boolean;
        
        hidden = !event.target.checked;
        
        this.grid.columns.toArray().forEach((item, index, arr) => {
            if (item instanceof ColumnComponent) {
                if ((<ColumnComponent>item).field == name) {
                    item.hidden = hidden;

                    var arrayIndex = this.rowData.findIndex(p => p.field == (<ColumnComponent>item).field)
                    var arrayItem: any = null;
                    this.rowData[arrayIndex].visibility = !hidden;

                    var hiddenColumns = this.rowData.filter(p => p.visibility == false);
                    if (hiddenColumns.length == this.rowData.length - 1) {
                        var lastVisibleColumn = this.rowData.findIndex(p => p.visibility == true);
                        this.rowData[lastVisibleColumn].disabled = true;
                    }
                    else {
                        var lastDisableColumn = this.rowData.findIndex(p => p.disabled == true);
                        if(lastDisableColumn >= 0)
                            this.rowData[lastDisableColumn].disabled = false;

                    }
                }
            }
        });

        localStorage.setItem(id, JSON.stringify(this.rowData));

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