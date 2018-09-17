
import { Directive, Host, Input } from "@angular/core";
import { ColumnComponent, FilterMenuTemplateDirective } from "@progress/kendo-angular-grid";
import { TranslateService } from "ng2-translate";
import { DefaultComponent } from "../../class/default.component";
import { String } from '../../class/source';
//import { Component } from '@angular/core'




@Directive({
    selector: '[sppc-grid-column]',
    providers: [String,DefaultComponent]
})

export class SppcGridColumn {
    constructor( @Host() private hostColumn: ColumnComponent, @Host() public hostColumn1: DefaultComponent, private translate: TranslateService)
    {
        //var props = def.properties;
    }
    
    @Input('sppc-grid-column') value: string;

    ngOnInit() {
        var item = this.hostColumn1;
    }

    ngOnChanges() {

        
        this.hostColumn.resizable = true;
        this.hostColumn.sortable = true;

        var parts = this.value.split('.');        

        this.hostColumn.field = parts[1];
        
        var key = parts[0] + "." + parts[1].charAt(0).toUpperCase() + parts[1].slice(1);
        this.translate.get(key).subscribe((msg: string) => {
            this.hostColumn.title = msg;
        });

    }

    ngAfterContentInit(): void {
        
        
    }

   
    //my new properties
}