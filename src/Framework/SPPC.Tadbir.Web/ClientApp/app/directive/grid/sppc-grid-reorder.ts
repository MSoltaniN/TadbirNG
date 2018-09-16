
import { Directive, Host, Input, HostListener, ElementRef } from "@angular/core";
import { GridComponent, ColumnComponent, CheckboxColumnComponent, ColumnBase } from "@progress/kendo-angular-grid";
import { TranslateService } from "ng2-translate";
import { DefaultComponent } from "../../class/default.component";
import { String } from '../../class/source';
import { CommandColumnComponent } from "@progress/kendo-angular-grid/dist/es2015/columns/command-column.component";
import { SettingService } from "../../service/index";
import { ColumnViewConfig } from "../../model/columnViewConfig";
import { ColumnViewDeviceConfig } from "../../model/columnViewDeviceConfig";


@Directive({
    selector: '[sppc-grid-reorder]',
    providers: [ String,DefaultComponent]
})

export class SppcGridReorder {
    constructor( @Host() private grid: GridComponent, private elRef: ElementRef,public settingService: SettingService,
        private translate: TranslateService, @Host() public defaultComponent: DefaultComponent) {
        
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

        var viewId: number = parseInt(this.elRef.nativeElement.id) //+ this.defaultComponent.UserId + "_size";
        var currentSetting = this.settingService.getSettingByViewId(viewId);


        this.grid.leafColumns.toArray().forEach((item, index, arr) => {

            if (!(item instanceof ColumnComponent)) {
                item.reorderable = false;                
            }

        });

        
        /*
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
        */

        var all = this.grid.columnList.toArray();

        var firstIndex = -1;
        this.grid.leafColumns.toArray().forEach((item, index, arr) => {
            if (currentSetting) {

                
                if (firstIndex == -1) {
                    var indexId = all.findIndex(o => o == item);
                    firstIndex = indexId;
                }
                if (item instanceof ColumnComponent) {
                    
                    var arrayIndex = currentSetting.columnViews.findIndex(p => p.name.toLowerCase() == (<ColumnComponent>item).field.toLowerCase())
                    var arrayItem: ColumnViewConfig | null = null;
                    if (arrayIndex >= 0)
                        arrayItem = currentSetting.columnViews[arrayIndex];

                    var columnViewDeviceConfig: ColumnViewDeviceConfig | undefined = undefined;
                    if (arrayItem)
                        columnViewDeviceConfig = this.settingService.getCurrentColumnViewConfig(arrayItem);

                    if (columnViewDeviceConfig)
                        if (columnViewDeviceConfig.designIndex)
                            item.orderIndex = columnViewDeviceConfig.designIndex;
                }
                else if (item instanceof CheckboxColumnComponent) {
                    item.orderIndex = 0;
                }
                else
                {
                    item.orderIndex = this.grid.columnList.toArray().findIndex(p => p == item) - firstIndex;
                }

            }
        });



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



        var viewId: number = parseInt(this.elRef.nativeElement.id) //+ this.defaultComponent.UserId + "_size";
        var currentSetting = this.settingService.getSettingByViewId(viewId);

        var leafColumnArray = this.grid.leafColumns.toArray();
        var all = this.grid.columnList.toArray();

        for (var i = 0; i < orders.length; i++) {
            var result = leafColumnArray[i];
            if (result) {
                var indexId = all.findIndex(o => o == result);
                var col = orders.findIndex(o => o == result);
                if (col >= 0)
                    orderIndexList[indexId] = col;


                if (currentSetting && result instanceof ColumnComponent) {

                    var currentColumn = <ColumnComponent>result;
                    var arrayIndex = currentSetting.columnViews.findIndex(p => p.name.toLowerCase() == currentColumn.field.toLowerCase())
                    var arrayItem: ColumnViewConfig | null = null;
                    if (arrayIndex >= 0)
                        arrayItem = currentSetting.columnViews[arrayIndex];

                    var columnViewDeviceConfig: ColumnViewDeviceConfig | undefined = undefined;
                    if (arrayItem)
                        columnViewDeviceConfig = this.settingService.getCurrentColumnViewConfig(arrayItem);

                    if (col >= 0 && columnViewDeviceConfig && columnViewDeviceConfig.index) {
                        columnViewDeviceConfig.designIndex = col;


                    }
                }
            }

        }
        
        if(currentSetting)
            this.settingService.setSettingByViewId(viewId, currentSetting);

    }
    
}