﻿
import { Directive, Host, Input, HostListener, ElementRef } from "@angular/core";
import { GridComponent, ColumnComponent, CheckboxColumnComponent, ColumnBase } from "@progress/kendo-angular-grid";
import { TranslateService } from "ng2-translate";
import { DefaultComponent } from "../../class/default.component";
import { CommandColumnComponent } from "@progress/kendo-angular-grid/dist/es2015/columns/command-column.component";


@Directive({
    selector: '[sppc-grid-resize]',
    providers: [DefaultComponent]
})

export class SppcGridResize {
    constructor( @Host() private grid: GridComponent, private elRef: ElementRef, private translate: TranslateService) {
        
    }

    @Input('sppc-grid-column') value: string;

    @HostListener('columnResize', ['$event']) columnResize(event: any) {
        //console.log(`column reorderd from ${event.oldIndex} to ${event.newIndex}`);

        var columnSizeList: { [id: number]: number; } = {}

        
            var items = this.grid.leafColumns.toArray();

        
            var resizeValues: Array < number > = [];
            var id: string = this.elRef.nativeElement.id + "_size";

            var newWidth = event.newWidth;
            
            var resizeColumnIndex = this.grid.columnList.toArray().findIndex(o => o == event[0].column);
            
            var resizes = localStorage.getItem(id);
            if(resizes)
                columnSizeList = JSON.parse(resizes);

            
            columnSizeList[resizeColumnIndex] = event[0].newWidth;
            
        
        
            localStorage.setItem(id, JSON.stringify(columnSizeList));

        
    }

    ngOnInit() {
        this.grid.resizable = true;
        

    }

    ngOnChanges() {


        

    }

    ngAfterContentInit(): void {

        
        var id: string = this.elRef.nativeElement.id + "_size";
        var resizeIndexList: { [id: number]: number; } = {}

        var resizeJson :string | null = localStorage.getItem(id);;
        if (resizeJson) {
            resizeIndexList = JSON.parse(resizeJson != null ? resizeJson.toString() : "")

            var all = this.grid.columnList.toArray();
            if (resizeIndexList) {
                this.grid.leafColumns.toArray().forEach((item, index, arr) => {

                    var indexId = all.findIndex(o => o == item);

                    if (resizeIndexList[indexId])
                        item.width = resizeIndexList[indexId]; 
                    
                });


                
            }
        }
            
    }

    
    
}