
import { Component, OnInit, Input } from '@angular/core';
import { ColumnComponent } from "@progress/kendo-angular-grid";
import { ColumnBase } from "@progress/kendo-angular-grid/dist/es/columns/column-base";


@Component({
    selector: 'sppc-grid-column',
    template: `<kendo-grid-column [field]="field" [sortable]="sortable" width="150" media="(min-width: 450px) and (max-width: 1024px)"  [filterable]="false"
                [title]="title" >
                    <ng-template kendoGridFilterCellTemplate>
                        <input type="text" class="k-textbox" />                        
                    </ng-template>
                </kendo-grid-column>`,
    providers: [ColumnBase]
})

export class SppcGridColumn extends ColumnComponent{

    
    @Input() public field: string = "";

    @Input() public sortable: boolean = false;

    @Input() public filterable: boolean = false;

    @Input() public title: string = "";

    ////@Input() public width: string = "";

    //@Input() public textColumn: boolean = false;

    //@Input() public boolColumn: boolean = false;

    

}