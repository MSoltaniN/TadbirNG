
import { Directive, Host, Input, HostListener, ElementRef } from "@angular/core";
import { GridComponent, ColumnComponent, CheckboxColumnComponent, ColumnBase } from "@progress/kendo-angular-grid";
import { TranslateService } from '@ngx-translate/core';
import { DefaultComponent } from "../../class/default.component";
import { SettingService } from "../../service/settings.service";
import { ListFormViewConfigInfo } from "../../service/index";
import { ColumnViewDeviceConfig } from "../../model/columnViewDeviceConfig";
import { ColumnViewConfig } from "../../model/columnViewConfig";



@Directive({
    selector: '[sppc-grid-resize]',
    providers: [DefaultComponent]
})

export class SppcGridResize {
    constructor( @Host() private grid: GridComponent, private elRef: ElementRef, public settingService: SettingService,
        private translate: TranslateService, @Host() public defaultComponent: DefaultComponent) {
        
    }

    @Input('sppc-grid-column') value: string;

    @HostListener('columnResize', ['$event']) columnResize(event: any) {
       
        this.resizeEvent(event);        
    }

    ngOnInit() {

        this.grid.resizable = true;        
    }

    ngAfterContentInit(): void {

        this.resizeOnLoad();            
    }

    /** تغییر اندازه ستون ها در زمان لود گرید */
    private resizeOnLoad() {
        
        var viewId: number = parseInt(this.elRef.nativeElement.id) //+ this.defaultComponent.UserId + "_size";
        var currentSetting = this.settingService.getSettingByViewId(viewId);
        
        this.grid.leafColumns.toArray().forEach((item, index, arr) => {

            if (currentSetting) {

                if (item instanceof ColumnComponent) {


                    var arrayIndex = currentSetting.columnViews.findIndex(p => p.name.toLowerCase() == (<ColumnComponent>item).field.toLowerCase())
                    var arrayItem: ColumnViewConfig | null = null;
                    if (arrayIndex >= 0)
                        arrayItem = currentSetting.columnViews[arrayIndex];

                    var columnViewDeviceConfig: ColumnViewDeviceConfig | undefined = undefined;
                    if (arrayItem)
                        columnViewDeviceConfig = this.settingService.getCurrentColumnViewConfig(arrayItem);

                    if (columnViewDeviceConfig)
                        if (columnViewDeviceConfig.width)
                            item.width = columnViewDeviceConfig.width;
                }
                    
            }
        });

            
        
    }

    /**
     * رویداد مربوط به تغییر اندازه ستون های گرید
     * @param event
     */
    private resizeEvent(event: any) {
        

        var items = this.grid.leafColumns.toArray();


        var resizeValues: Array<number> = [];
        var viewId: number = parseInt(this.elRef.nativeElement.id) //+ this.defaultComponent.UserId + "_size";

        var newWidth = event.newWidth;

        var resizeColumnIndex = this.grid.columnList.toArray().findIndex(o => o == event[0].column);

        //var resizes = localStorage.getItem(id);
        //if (resizes)
            //columnSizeList = JSON.parse(resizes);

        var currentSetting = this.settingService.getSettingByViewId(viewId);

        if (currentSetting) {
            
            
           
            var arrayIndex = currentSetting.columnViews.findIndex(p => p.name.toLowerCase() == event[0].column.field.toLowerCase())
            var arrayItem: ColumnViewConfig | null = null;
            if (arrayIndex >= 0)
                arrayItem = currentSetting.columnViews[arrayIndex];

            var columnViewDeviceConfig: ColumnViewDeviceConfig | undefined = undefined;
            if (arrayItem)
                columnViewDeviceConfig = this.settingService.getCurrentColumnViewConfig(arrayItem);

            if (columnViewDeviceConfig && columnViewDeviceConfig.index) {
                
                columnViewDeviceConfig.width = event[0].newWidth;
                currentSetting.columnViews[arrayIndex] = this.settingService.setCurrentColumnViewConfig(currentSetting.columnViews[arrayIndex], columnViewDeviceConfig);

            }

            this.settingService.setSettingByViewId(viewId, currentSetting);
            
        }

        
    }

    
    
}
