
import { Component, Input } from '@angular/core';
import { CompositeFilterDescriptor } from "@progress/kendo-data-query/dist/es/main";

@Component({
    selector: 'sppc-string-filter',
    template: `<kendo-grid-string-filter-cell [column]="column"  [filter]="filter"  >
                            <kendo-filter-eq-operator></kendo-filter-eq-operator>
                            <kendo-filter-neq-operator></kendo-filter-neq-operator>
                            <kendo-filter-contains-operator></kendo-filter-contains-operator>
                            <kendo-filter-not-contains-operator></kendo-filter-not-contains-operator>
                            <kendo-filter-startswith-operator></kendo-filter-startswith-operator>
                            <kendo-filter-endswith-operator></kendo-filter-endswith-operator>
                        </kendo-grid-string-filter-cell> `
})
export class SppcStringFilter {

    @Input() public column: string;
    @Input() public filter: CompositeFilterDescriptor;
    
}
