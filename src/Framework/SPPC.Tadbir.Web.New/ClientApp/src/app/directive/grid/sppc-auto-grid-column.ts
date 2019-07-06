
import { Directive, Host, Input, ElementRef, Renderer2 } from "@angular/core";
import { ColumnComponent } from "@progress/kendo-angular-grid";
import { TranslateService } from '@ngx-translate/core';
import { DefaultComponent } from "../../class/default.component";
import { String } from '../../class/source';
import { GridFilterComponent } from "./component/grid-filter.component";
import { ColumnViewConfig } from "../../model/columnViewConfig";
import { EnviromentComponent } from "../../class/enviroment.component";
import { ColumnViewDeviceConfig } from "../../model/columnViewDeviceConfig";


@Directive({
  selector: '[sppc-auto-grid-column]',
  providers: [String, DefaultComponent, GridFilterComponent]
})

export class SppcAutoGridColumn extends EnviromentComponent {

  constructor(@Host() private hostColumn: ColumnComponent) {
    super();
  }


  @Input('sppc-auto-grid-column') value: string;

  ngOnInit() {
  }


  ngOnChanges() {
    let setting: ColumnViewConfig;
    setting = JSON.parse(this.value);
    var size = this.screenSize;
    let screenSetting: ColumnViewDeviceConfig = setting[size];

    this.hostColumn.resizable = true;
    this.hostColumn.sortable = true;
    this.hostColumn.width = screenSetting.width;
    this.hostColumn.title = screenSetting.title;

    var fieldName = setting.name.split('.');
    for (var i = 0; i < fieldName.length; i++) {
      fieldName[i] = fieldName[i].charAt(0).toLowerCase() + fieldName[i].slice(1);
    }

    this.hostColumn.field = fieldName.join('.');
  }
}
