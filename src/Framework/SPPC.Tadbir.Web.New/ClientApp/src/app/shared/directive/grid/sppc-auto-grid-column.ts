
import { Directive, Host, Input } from "@angular/core";
import { ColumnComponent } from "@progress/kendo-angular-grid";
import { BrowserStorageService, EnviromentComponent, DefaultComponent, ColumnViewDeviceConfig, ColumnViewConfig } from "@sppc/shared";
import { GridFilterComponent } from "./component/grid-filter.component";



@Directive({
  selector: '[sppc-auto-grid-column]',
  providers: [String, DefaultComponent, GridFilterComponent]
})

export class SppcAutoGridColumn extends EnviromentComponent {

  constructor(@Host() private hostColumn: ColumnComponent, public bStorageService: BrowserStorageService) {
    super(bStorageService);
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
