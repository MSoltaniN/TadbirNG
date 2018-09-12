import { BaseComponent } from "../../../class/base.component";
import { OnInit, OnDestroy, Component, Host, ElementRef } from "@angular/core";
import { Layout } from "../../../enviroment";
import { RTL } from "@progress/kendo-angular-l10n";
import { ToastrService } from "ngx-toastr";
import { GridComponent } from "@progress/kendo-angular-grid";
import { TranslateService } from "ng2-translate";
import { SettingService } from "../../../service/settings.service";
import { DefaultComponent } from "../../../class/default.component";

export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}

@Component({
    selector: 'grid-filter',
    templateUrl: './grid-filter.component.html',
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]
})

export class GridFilterComponent extends BaseComponent implements OnInit, OnDestroy {

    constructor(public toastrService: ToastrService, public translate: TranslateService, public settingService: SettingService,
        @Host() private grid: GridComponent, private elRef: ElementRef, @Host() public defaultComponent: DefaultComponent) {

        super(toastrService);
    }

    ngOnDestroy(): void {
        throw new Error("Method not implemented.");
    }
    ngOnInit(): void {
        throw new Error("Method not implemented.");
    }

}