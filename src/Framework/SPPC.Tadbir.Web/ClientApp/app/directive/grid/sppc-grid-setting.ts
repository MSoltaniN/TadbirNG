
import { Directive, Host, Input, HostListener, ElementRef } from "@angular/core";
import { GridComponent, ColumnComponent } from "@progress/kendo-angular-grid";
import { TranslateService } from "ng2-translate";
import { DefaultComponent } from "../../class/default.component";
import { String } from '../../class/source';


@Directive({
    selector: '[sppc-grid-setting]',
    providers: [DefaultComponent, String]
})

export class SppcGridReorder {
    constructor(@Host() private grid: GridComponent, private elRef: ElementRef, private translate: TranslateService) {

    }   

    @HostListener('click', ['$event']) click(event: any) {
       

    }

    ngOnInit() {
        

    }

    ngOnChanges() {




    }

    ngAfterContentInit(): void {

       
    }


    //my new properties
}