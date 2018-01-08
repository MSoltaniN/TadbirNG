
import { Component, OnInit, Input } from '@angular/core';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from "@angular/forms";

@Component({
    selector: 'sppc-dropdownlist',
    template: `<kendo-dropdownlist [data]="data" [valuePrimitive]="true"
                                    [textField]="'Value'" formControlName="fiscalPeriodId" [value]="selectedValue" [(ngModel)]="selectedValue" [valueField]="'Key'">
                </kendo-dropdownlist>`,
    providers: [{
        provide: NG_VALUE_ACCESSOR,
        useExisting: SppcDropDownList,
        multi: true
    }]
})

export class SppcDropDownList implements OnInit, ControlValueAccessor {

    writeValue(obj: any): void {
        throw new Error("Method not implemented.");
    }
    registerOnChange(fn: any): void {
        throw new Error("Method not implemented.");
    }
    registerOnTouched(fn: any): void {
        throw new Error("Method not implemented.");
    }
    
    ngOnInit() {
        
    }
    

}