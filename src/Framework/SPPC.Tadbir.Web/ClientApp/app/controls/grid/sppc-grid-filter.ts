﻿
import { Component, Input } from '@angular/core';
import { CompositeFilterDescriptor } from "@progress/kendo-data-query";  //npm shrinkwrap
import { Property } from '../../class/metadata/property';

@Component({
    selector: 'sppc-grid-filter',
    templateUrl:  './sppc-grid-filter.html'    
})
export class SppcGridFilter {

    @Input() public column: string;
    @Input() public filter: CompositeFilterDescriptor;
    @Input() public isNumber: boolean = false;
    @Input() public isString: boolean = false;
    @Input() public isDate: boolean = false;

    @Input('metaData')
    public set metaData(value: Property | undefined) {
        if (value == undefined) return;
        switch (value.scriptType.toLowerCase()) {
            case "number":
                this.isNumber = true;
                break;
            case "string":
                this.isString = true;
                break;
            case "date":
                this.isDate = true;
                break;
            default:
                break;
        }

    }
}
