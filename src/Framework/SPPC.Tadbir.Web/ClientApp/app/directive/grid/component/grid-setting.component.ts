import { RTL } from "@progress/kendo-angular-l10n";
import { Layout } from "../../../enviroment";
import { Component, OnInit, ViewContainerRef, Host, ElementRef } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from "ng2-translate";
import { SppcLoadingService } from "../../../controls/sppcLoading/index";
import { BaseComponent } from "../../../class/base.component";
import { GridDataResult, GridComponent, ColumnComponent } from "@progress/kendo-angular-grid";


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
        @Host() private grid: GridComponent, private elRef: ElementRef) {
        super(toastrService);
    }

    ngOnInit() {

        if (this.CurrentLanguage == 'fa')
            this.rtl = true;
        else
            this.rtl = false;

        var id: string = this.elRef.nativeElement.id + "_hidden";

        var rowDataString = localStorage.getItem(id);
        if (rowDataString)
            this.rowData = JSON.parse(rowDataString);

        this.grid.leafColumns.toArray().forEach((item, index, arr) => {
            if (item.constructor.name == "ColumnComponent") {
                var arrayIndex = this.rowData.findIndex(p => p.field == (<ColumnComponent>item).field)
                var arrayItem: any = null;
                if (arrayIndex >= 0)
                    arrayItem = this.rowData[arrayIndex];

                
                var row = { visibility: true, name: item.displayTitle, field: (<ColumnComponent>item).field, disabled : false};
                if (arrayItem) {
                    row.visibility = arrayItem.visibility;
                    this.rowData[arrayItem] = row;                    
                    item.hidden = !row.visibility;

                    var hiddenColumns = this.rowData.filter(p => p.visibility == false);
                    
                    if (row.visibility && hiddenColumns.length == this.rowData.length - 1)
                        row.disabled = true;

                }
                else
                    this.rowData.push(row);
            }
        });

        
        
        
    }

    changeVisibility(name :string, event : any) {


        var id: string = this.elRef.nativeElement.id + "_hidden";
        var hidden: boolean;
        
        hidden = !event.target.checked;
        
        this.grid.columns.toArray().forEach((item, index, arr) => {
            if (item.constructor.name == "ColumnComponent") {
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

    public showSetting() {
        this.show = true;
    }

    public closeSetting() {
        this.show = false;
    }

}