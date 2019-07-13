
import { Component, Input } from '@angular/core';
import { CompositeFilterDescriptor } from "@progress/kendo-data-query";  //npm shrinkwrap

@Component({
  selector: 'sppc-auto-grid-filter',
  templateUrl: './sppc-grid-filter.html'
})
export class SppcAutoGridFilter {

  @Input() public column: string;
  @Input() public filter: CompositeFilterDescriptor;
  @Input() public isNumber: boolean = false;
  @Input() public isString: boolean = false;
  @Input() public isDate: boolean = false;
  @Input() public isDateTime: boolean = false;

  @Input('metaData')
  public set metaData(value: string | undefined) {
    if (value == undefined) return;
    switch (value.toLowerCase()) {
      case "number":
        this.isNumber = true;
        break;
      case "string":
        this.isString = true;
        break;
      case "date":
        this.isDate = true;
        break;
      case "datetime":
        this.isDateTime = true;
        break;
      default:
        break;
    }

  }
}
