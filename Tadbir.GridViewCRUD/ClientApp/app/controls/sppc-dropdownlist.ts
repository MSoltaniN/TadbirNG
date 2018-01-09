
import { Component, OnInit, Input, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { DropDownsModule } from "@progress/kendo-angular-dropdowns";


@Component({
    selector: 'sppc-dropdownlist',
    template: `<kendo-dropdownlist [data]="data" [valuePrimitive]="true"
                                    [textField]="textField"  [value]="selectedValue" [valueField]="valueField" [(ngModel)]="selectedValue" ></kendo-dropdownlist>` ,
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            multi: true,
            useExisting: forwardRef(() => SppcDropDownList),
        }
    ]
})

export class SppcDropDownList extends DropDownsModule implements OnInit,ControlValueAccessor {
    writeValue(obj: any): void {
        this.selectedValue = obj;      
    }
    registerOnChange(fn: any): void {        
    }
    registerOnTouched(fn: any): void {        
    }

    public selectedValue: string;
    @Input() public data: any;
    @Input() public textField: string; 
    @Input() public valueField: string; 
    //public fiscalPeriodId: string; 

    ngOnInit() {
        
    }

    constructor()
    {
        super();
    }

}