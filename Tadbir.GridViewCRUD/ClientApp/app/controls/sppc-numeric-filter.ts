import { Component, Input } from '@angular/core';
import { FilterService } from '@progress/kendo-angular-grid';
import { NumericFilterComponent } from "@progress/kendo-angular-grid/dist/es/filtering/numeric-filter.component";
import { LocalizationService } from "@progress/kendo-angular-l10n/dist/es/localization.service";
import { CompositeFilterDescriptor } from "@progress/kendo-data-query";


@Component({
    selector: 'sppc-numeric-filter',
    template: `    
        <kendo-numerictextbox
            [value]="value" >
        </kendo-numerictextbox>
  `
})
export class SppcNumberFilterComponent extends NumericFilterComponent {

    public get selectedValue(): any {
        const filter = this.filterByField(this.valueField);
        return filter ? filter.value : null;
    }

    @Input() public filter: CompositeFilterDescriptor;
    @Input() public data: any[];
    @Input() public textField: string;
    @Input() public valueField: string;

    constructor(filterService: FilterService,localization:LocalizationService) {
        super(filterService,localization);
    }

    public onChange(value: any): void {
        this.applyFilter(
            value === null ? // value of the default item
                this.removeFilter(this.valueField) : // remove the filter
                this.updateFilter({ // add a filter for the field with the value
                    field: this.valueField,
                    operator: this.currentOperator,
                    value: value
                })
        ); // update the root filter
    }
}
