
import { Component, OnInit, Input, forwardRef, OnChanges, OnDestroy, SimpleChanges } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { DropDownsModule } from "@progress/kendo-angular-dropdowns";

const noop = () => {
};



@Component({
    selector: 'sppc-dropdownlist',
    template: `<kendo-dropdownlist [data]="data" [valuePrimitive]="true"
                                    [textField]="textField" (valueChange)="valueChange($event)"  [value]="selectedValue" [valueField]="valueField"
                [(ngModel)]="currentValue" ></kendo-dropdownlist>` ,
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            multi: true,
            useExisting: forwardRef(() => SppcDropDownList),
        }
    ]
})

export class SppcDropDownList implements OnInit, ControlValueAccessor {

    private onTouchedCallback: () => void = noop;
    private onChangeCallback: (_: any) => void = noop;


    writeValue(obj: any): void {
        
        this.selectedValue = obj;      
    }

    //From ControlValueAccessor interface
    registerOnChange(fn: any) {
        this.onChangeCallback = fn;
    }

    //From ControlValueAccessor interface
    registerOnTouched(fn: any) {
        this.onTouchedCallback = fn;
    }

    public valueChange(value: any): void {
        this.selectedValue = value;
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
        
    }


    //get accessor
    get currentValue(): any {
        return this.selectedValue;
    };

    //set accessor including call the onchange callback
    set currentValue(v: any) {
        if (v !== this.selectedValue) {
            this.selectedValue = v;
            this.onChangeCallback(v);
        }
    }

    //Set touched on blur
    onBlur() {
        this.onTouchedCallback();
    }

}