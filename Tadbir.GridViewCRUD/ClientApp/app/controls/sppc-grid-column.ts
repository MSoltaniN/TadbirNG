
import { Component, OnInit, Input } from '@angular/core';



@Component({
    selector: 'sppc-grid-column',
    template: `<kendo-grid-column field="code" [sortable]="true" width="150" [filterable]="true" title="code"  >    
</kendo-grid-column>`
})

export class SppcGridColumn implements OnInit {


    @Input() public dataField: string = "";

    @Input() public allowSort: boolean = false;

    @Input() public allowFilter: boolean = false;

    @Input() public columnTitle: string = "";

    @Input() public width: string = "";

    @Input() public textColumn: boolean = false;

    @Input() public boolColumn: boolean = false;


    constructor() {        

    }


    ngOnInit() {

        var i = 0;

    }


}