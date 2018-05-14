
import { Directive, Host, Input, HostListener, ElementRef } from "@angular/core";
import { GridComponent, ColumnComponent } from "@progress/kendo-angular-grid";
import { TranslateService } from "ng2-translate";
import { DefaultComponent } from "../../class/default.component";
import { String } from '../../class/source';


@Directive({
    selector: '[sppc-grid-reorder]',
    providers: [DefaultComponent, String]
})

export class SppcGridReorder {
    constructor( @Host() private grid: GridComponent, private elRef: ElementRef, private translate: TranslateService) {
        
    }

    @Input('sppc-grid-column') value: string;

    @HostListener('columnReorder', ['$event']) columnReorder(event: any) {
        //console.log(`column reorderd from ${event.oldIndex} to ${event.newIndex}`);

        var  orderIndexList : { [id: string]: number; } = {}

        this.grid.leafColumns.forEach((item, index, arr) => {
            if (item.constructor.name == "ColumnComponent") {
                var col: ColumnComponent = <ColumnComponent>item;

                var orderIndex: number = col.orderIndex;

                if (col.field == event.column.field) orderIndex = event.newIndex;
                
                orderIndexList[col.field] = orderIndex;
            }
            

        });

        var id: string = this.elRef.nativeElement.id;

        localStorage.setItem(id, JSON.stringify(orderIndexList));

        
    }

    ngOnInit() {
        this.grid.reorderable = true;
        

    }

    ngOnChanges() {


        

    }

    ngAfterContentInit(): void {

        var i: number = 0;
        var id: string = this.elRef.nativeElement.id;
        var orderIndexList: { [id: string]: number; } | null = {}

        var orderJson :string | null = localStorage.getItem(id);;
        if (orderJson) {
            orderIndexList = JSON.parse(orderJson != null ? orderJson.toString() : "")

            if (orderIndexList) {
                this.grid.leafColumns.forEach((item, index, arr) => {
                    if (item.constructor.name == "ColumnComponent") {

                        var col: ColumnComponent = <ColumnComponent>item;
                        col.orderIndex = orderIndexList ? orderIndexList[col.field] : col.orderIndex;
                    }


                });
            }
        }
    }


    //my new properties
}