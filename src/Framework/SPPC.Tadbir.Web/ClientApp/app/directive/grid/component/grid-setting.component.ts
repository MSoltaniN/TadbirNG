import { RTL } from "@progress/kendo-angular-l10n";
import { Layout } from "../../../enviroment";
import { Component, OnInit, ViewContainerRef, Host } from "@angular/core";
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

    constructor(public toastrService: ToastrService, public translate: TranslateService, @Host() private grid: GridComponent) {
        super(toastrService);
    }

    ngOnInit() {

        if (this.CurrentLanguage == 'fa')
            this.rtl = true;
        else
            this.rtl = false;


        this.grid.leafColumns.toArray().forEach((item, index, arr) => {
            if (item.constructor.name == "ColumnComponent") {
                var row = { "visibility": true, "name": item.displayTitle };
                this.rowData.push(row);
            }
        });
        
    }

    changeVisibility(name :string, event : any) {

        var hidden: boolean;
        
        hidden = !event.target.checked;
        
        this.grid.columns.toArray().forEach((item, index, arr) => {
            if (item.constructor.name == "ColumnComponent") {
                if ((<ColumnComponent>item).field == name)
                    item.hidden = hidden;
            }
        });

    }

    public showSetting() {
        this.show = true;
    }

    public closeSetting() {
        this.show = false;
    }

}