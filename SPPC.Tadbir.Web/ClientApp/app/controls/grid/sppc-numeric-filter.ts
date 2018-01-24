
import { Component, Input } from '@angular/core';
import { CompositeFilterDescriptor } from "@progress/kendo-data-query/dist/es/main";

@Component({
    selector: 'sppc-numeric-filter',
    template: `<kendo-grid-numeric-filter-cell [spinners]="spinners" [column]="column"   [filter]="filter">
                            <kendo-filter-eq-operator></kendo-filter-eq-operator>
                            <kendo-filter-neq-operator></kendo-filter-neq-operator>
                            <kendo-filter-lt-operator></kendo-filter-lt-operator>
                            <kendo-filter-lte-operator></kendo-filter-lte-operator>                            
                            <kendo-filter-gt-operator></kendo-filter-gt-operator>
                            <kendo-filter-gte-operator></kendo-filter-gte-operator>                                                        
                        </kendo-grid-numeric-filter-cell> `
})
export class SppcNumericFilter {

    @Input() public column: string;
    @Input() public filter: CompositeFilterDescriptor;
    @Input() public spinners: string;

    
}
