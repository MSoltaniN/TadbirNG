
import { Directive, Host, Input, HostListener, ElementRef } from "@angular/core";
import { GridComponent, ColumnComponent, CheckboxColumnComponent, ColumnBase } from "@progress/kendo-angular-grid";
import { TranslateService } from "ng2-translate";
import { DefaultComponent } from "../../class/default.component";
import { String } from '../../class/source';
import { CommandColumnComponent } from "@progress/kendo-angular-grid/dist/es2015/columns/command-column.component";


@Directive({
    selector: '[sppc-grid-reorder]',
    providers: [DefaultComponent, String]
})

export class SppcGridReorder {
    constructor(@Host() private grid: GridComponent, private elRef: ElementRef, private translate: TranslateService, @Host() public defaultComponent: DefaultComponent) {
        
    }

    @Input('sppc-grid-column') value: string;

    @HostListener('columnReorder', ['$event']) columnReorder(event: any) {        
                
        this.reorderAndSave(event);
        
    }

    ngOnInit() {

        this.grid.reorderable = true;        
    }
    
    ngAfterContentInit(): void {

        this.reorderOnLoad();        
    }

    /** در زمان لود چینش مربوط به ستون ها را مرتب میکند */
    private reorderOnLoad() {

        this.grid.leafColumns.toArray().forEach((item, index, arr) => {

            if (!(item instanceof ColumnComponent)) {
                item.reorderable = false;
            }

        });


        var i: number = 0;
        var id: string = this.elRef.nativeElement.id + "_" + + this.defaultComponent.UserId + "_reorder";
        var orderIndexList: { [id: number]: number; } = {}

        var orderJson: string | null = localStorage.getItem(id);;
        if (orderJson) {
            orderIndexList = JSON.parse(orderJson != null ? orderJson.toString() : "")

            var all = this.grid.columnList.toArray();
            if (orderIndexList) {
                this.grid.leafColumns.toArray().forEach((item, index, arr) => {

                    var indexId = all.findIndex(o => o == item);

                    item.orderIndex = orderIndexList[indexId];


                });



            }
        }
    }

    /**
     * این متد چینش مربوط به ستون های گرید را در آرایه مرتب میکند و در حافظه مرورگر ذخیره میکند 
     * @param event
     */
    private reorderAndSave(event: any) {
        var orderIndexList: { [id: number]: number; } = {}


        var orders: Array<ColumnBase> = []
        var items = this.grid.leafColumns.toArray();

        if (event.oldIndex > event.newIndex) {

            var oldIndex = -1;
            var newIndex = -1;


            for (var i = 0; i < this.grid.leafColumns.length; i++) {

                if (i == oldIndex) continue;
                if (event.newIndex == i) {
                    var newColumn = items[event.oldIndex];
                    newColumn.orderIndex = i;
                    orders.push(newColumn);

                    var oldColumn = items[i];
                    oldColumn.orderIndex = i + 1;
                    orders.push(oldColumn);

                    oldIndex = event.oldIndex;
                    continue;
                }

                var column = items[i];
                column.orderIndex = i;
                orders.push(column);

            }

        }

        if (event.oldIndex < event.newIndex) {

            var oldIndex = -1;
            var newIndex = -1;
            var plus = 0;

            for (var i = 0; i < this.grid.leafColumns.length; i++) {
                if (oldIndex > -1 && i == newIndex) {

                    var column = items[i];
                    column.orderIndex = i;
                    orders.push(column);

                    var newColumn = items[oldIndex];
                    newColumn.orderIndex = i + 1;
                    orders.push(newColumn);

                    plus = 1;

                    continue;
                }
                if (event.oldIndex == i) {
                    oldIndex = event.oldIndex;
                    newIndex = event.newIndex;
                    continue;
                }

                var column = items[i];
                column.orderIndex = i + plus;
                orders.push(column);
            }

        }



        var id: string = this.elRef.nativeElement.id + "_" + + this.defaultComponent.UserId + "_reorder";
        var leafColumnArray = this.grid.leafColumns.toArray();
        var all = this.grid.columnList.toArray();
        for (var i = 0; i < orders.length; i++) {
            var result = leafColumnArray[i];
            if (result) {
                var indexId = all.findIndex(o => o == result);
                var col = orders.findIndex(o => o == result);
                if (col >= 0)
                    orderIndexList[indexId] = col;
            }

        }

        localStorage.setItem(id, JSON.stringify(orderIndexList));

    }
    
}