
import { Directive, Host, Input, HostListener, ElementRef, ViewChild, ViewContainerRef, ComponentFactoryResolver } from "@angular/core";
import { GridComponent, ColumnComponent } from "@progress/kendo-angular-grid";
import { TranslateService } from "ng2-translate";
import { DefaultComponent } from "../../class/default.component";
import { String } from '../../class/source';
import { GridSettingComponent } from "./component/grid-setting.component";


@Directive({
    selector: '[sppc-grid-setting]',        
    providers: [DefaultComponent, String,GridSettingComponent]
})

export class SppcGridSetting {

    @ViewChild('kendo-grid', { read: ViewContainerRef }) container: ViewContainerRef;

    
    components = [];

    constructor(private elRef: ElementRef, private translate: TranslateService, @Host() private parent: GridSettingComponent) {

    }   

    @HostListener('click', ['$event']) click(event: any) {

        //this.setting.show = true;
        
    }

    ngOnInit() {
        

    }

    

}