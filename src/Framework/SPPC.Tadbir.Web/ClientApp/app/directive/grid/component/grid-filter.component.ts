import { BaseComponent } from "../../../class/base.component";
import { OnInit, OnDestroy, Component, Host, ElementRef, Input } from "@angular/core";
import { Layout } from "../../../enviroment";
import { RTL } from "@progress/kendo-angular-l10n";
import { ToastrService } from "ngx-toastr";
import { GridComponent } from "@progress/kendo-angular-grid";
import { TranslateService } from "ng2-translate";
import { SettingService } from "../../../service/settings.service";
import { DefaultComponent } from "../../../class/default.component";
import { AccountComponent } from "../../../components/account/account.component";

export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}

@Component({
    selector: 'grid-filter',
    templateUrl: './grid-filter.component.html',
    styleUrls: ['./grid-filter.component.css'],
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]
})

export class GridFilterComponent extends BaseComponent implements OnInit, OnDestroy {

    rtl: boolean;
    @Input() public showClearFilter: number = 0;
    @Input() public parentComponent: any;
    
    constructor(public toastrService: ToastrService, public translate: TranslateService, public settingService: SettingService,
        @Host() private grid: GridComponent, private elRef: ElementRef) {

        super(toastrService);
        if(this.grid.filter)
            this.grid.filter.filters = [];
    }

    ngOnDestroy(): void {
       
    }

    ngOnInit(): void {
        if (this.CurrentLanguage == 'fa')
            this.rtl = true;
        else
            this.rtl = false;

        var self = this;

        //document.addEventListener('keydown', function (ev: KeyboardEvent) {
        //    if (ev.srcElement.hasAttribute('kendofilterinput') && ev.key == 'Enter') {
        //        self.parentComponent.reloadGrid();
        //    }
        //});

        
        
        document.addEventListener('keydown', function (ev: KeyboardEvent) {
            if (ev.srcElement.hasAttribute('kendofilterinput') && ev.key == 'Enter') {
                self.parentComponent.reloadGrid();
            }
        });       

    }
    

    filterGrid(): void {        

        this.showClearFilter = this.grid.filter.filters.length;

        this.parentComponent.reloadGrid();
    }

    removeFilterGrid(): void {
        this.grid.filter.filters = [];
        this.showClearFilter = this.grid.filter.filters.length;

        this.parentComponent.filterChange(this.grid.filter);
        this.parentComponent.reloadGrid();

    }

}