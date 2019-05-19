
import { Directive, Host, Input, HostListener, ElementRef } from "@angular/core";
import { GridComponent, ColumnComponent, CheckboxColumnComponent, ColumnBase } from "@progress/kendo-angular-grid";
import { TranslateService } from '@ngx-translate/core';
import { DefaultComponent } from "../../class/default.component";
import { SettingService } from "../../service/settings.service";
import { ListFormViewConfigInfo } from "../../service/index";
import { ColumnViewDeviceConfig } from "../../model/columnViewDeviceConfig";
import { ColumnViewConfig } from "../../model/columnViewConfig";



@Directive({
  selector: '[sppc-auto-generated-grid-resize]',
  providers: [DefaultComponent]
})

export class SppcAutoGeneratedGridResize {
  constructor(@Host() private grid: GridComponent, private elRef: ElementRef, public settingService: SettingService,
    private translate: TranslateService, @Host() public defaultComponent: DefaultComponent) {

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
