
import { Component, Input } from '@angular/core';
import { CompositeFilterDescriptor } from "@progress/kendo-data-query/dist/es/main";
import { Property } from '../../class/metadata/property';

@Component({
    selector: 'sppc-grid-filter',
    templateUrl:  './sppc-grid-filter.html',
    //template: `<kendo-grid-string-filter-cell [column]="column"  [filter]="filter"  >
    //                        <kendo-filter-eq-operator></kendo-filter-eq-operator>
    //                        <kendo-filter-neq-operator></kendo-filter-neq-operator>
    //                        <kendo-filter-contains-operator></kendo-filter-contains-operator>
    //                        <kendo-filter-not-contains-operator></kendo-filter-not-contains-operator>
    //                        <kendo-filter-startswith-operator></kendo-filter-startswith-operator>
    //                        <kendo-filter-endswith-operator></kendo-filter-endswith-operator>
    //                    </kendo-grid-string-filter-cell> `
})
export class SppcGridFilter {

    @Input() public column: string;
    @Input() public filter: CompositeFilterDescriptor;
    @Input() public isNumber: boolean = false;
    @Input() public isString: boolean = false;

    @Input('metaData')
    public set metaData(value: Property | undefined) {
        if (value == undefined) return;

        switch (value.scriptType) {
            case "number":
                this.isNumber = true;
                break;
            case "string":
                this.isString = true;
                break;
            default :
                this.isString = true;
        }

    }
}
