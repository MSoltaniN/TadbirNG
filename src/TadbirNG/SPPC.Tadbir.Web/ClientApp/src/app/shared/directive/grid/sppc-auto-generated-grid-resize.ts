
import { Directive, Host, Input, HostListener, ElementRef, ViewChildren, ViewChild } from "@angular/core";
import { GridComponent } from "@progress/kendo-angular-grid";
import { TranslateService } from '@ngx-translate/core';
import { ColumnViewConfig, ColumnViewDeviceConfig } from "@sppc/shared/models";
import { SettingService } from "@sppc/config/service";
import { DefaultComponent, EnviromentComponent } from "@sppc/shared/class";
import { BrowserStorageService } from "@sppc/shared/services";



@Directive({
  selector: '[sppc-auto-generated-grid-resize]',
  providers: [DefaultComponent]
})

export class SppcAutoGeneratedGridResize extends EnviromentComponent {  

  constructor(@Host() private grid: GridComponent, private elRef: ElementRef, public settingService: SettingService,
    @Host() public defaultComponent: DefaultComponent, public bStorageService: BrowserStorageService) {
    super(bStorageService);
  }

  @Input('sppc-grid-column') value: string;

  @HostListener('columnResize', ['$event']) columnResize(event: any) {
    this.resizeEvent(event);
  }

  ngOnInit() {   
    this.grid.resizable = true;
  }

   

  /**
   * رویداد مربوط به تغییر اندازه ستون های گرید
   * @param event
   */
  private resizeEvent(event: any) {    

    var viewId: number = parseInt(this.elRef.nativeElement.id)
    var currentSetting = this.settingService.getSettingByViewId(viewId);

    if (currentSetting) {
      var arrayIndex = currentSetting.columnViews.findIndex(p => event[0].column.field && p.name.toLowerCase() == event[0].column.field.toLowerCase())
      var arrayItem: ColumnViewConfig | null = null;
      if (arrayIndex >= 0)
        arrayItem = currentSetting.columnViews[arrayIndex];

      var columnViewDeviceConfig: ColumnViewDeviceConfig | undefined = undefined;
      if (arrayItem)
        columnViewDeviceConfig = this.settingService.getCurrentColumnViewConfig(arrayItem);

      if (columnViewDeviceConfig) {

        columnViewDeviceConfig.width = event[0].newWidth;
        currentSetting.columnViews[arrayIndex] = this.settingService.setCurrentColumnViewConfig(currentSetting.columnViews[arrayIndex], columnViewDeviceConfig);

      }

      this.settingService.setSettingByViewId(viewId, currentSetting);
    }

  }



}
