import { RTL } from "@progress/kendo-angular-l10n";
import { Layout } from "../../../enviroment";
import { Component, OnInit, ViewContainerRef } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from "ng2-translate";
import { SppcLoadingService } from "../../../controls/sppcLoading/index";


export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
}


@Component({
    selector: 'grid-setting-form-component',
    templateUrl: './grid-setting-form.component.html',
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]
})

export class GridSettingFormComponent implements OnInit {

    show: boolean;

    constructor(public toastrService: ToastrService, public translate: TranslateService) {
            
    }

    ngOnInit() {


    }

    
}