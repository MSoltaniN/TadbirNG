
import { Directive, Host, Input } from "@angular/core";
import { ColumnComponent, GridComponent } from "@progress/kendo-angular-grid";
import { ColumnViewConfig, ColumnViewDeviceConfig } from "@sppc/shared/models";
import { GridFilterComponent } from "./component/grid-filter.component";
import { DefaultComponent, EnviromentComponent } from "@sppc/shared/class";
import { BrowserStorageService } from "@sppc/shared/services";



@Directive({
  selector: '[sppc-auto-grid-column]',
  providers: [DefaultComponent, GridFilterComponent]
})

export class SppcAutoGridColumn extends EnviromentComponent {

  constructor(@Host() private hostColumn: ColumnComponent, public bStorageService: BrowserStorageService, @Host() private grid: GridComponent) {
    super(bStorageService);
  }

  columnsWidth: number;

  @Input('sppc-auto-grid-column') value: string;
  @Input('sppc-auto-grid-column-hidden') hidden: boolean;

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
    this.hostColumn.hidden = this.hidden;

    var fieldName = setting.name.split('.');
    for (var i = 0; i < fieldName.length; i++) {
      fieldName[i] = fieldName[i].charAt(0).toLowerCase() + fieldName[i].slice(1);
    }

    this.hostColumn.field = fieldName.join('.');    
  }
}
