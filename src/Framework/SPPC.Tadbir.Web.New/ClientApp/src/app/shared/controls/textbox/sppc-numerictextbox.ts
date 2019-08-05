
import { Component, OnInit, Input, forwardRef } from '@angular/core';
import { ControlValueAccessor, Validator, AbstractControl, FormControl, NG_VALUE_ACCESSOR, NG_VALIDATORS } from '@angular/forms';
import { KeyCode } from '@sppc/shared/enum';




@Component({
    selector: 'sppc-numerictextbox',
    template: `<kendo-numerictextbox
          [spinners]="spinners"
          [format]="format"
          [step]="step"
          [(ngModel)]="value"    
          (ngModelChange)="onChangeModel()"
          (keydown)="keyPress($event.keyCode)">
          </kendo-numerictextbox>`,
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => SppcNumericTextBox),
            multi: true
        },
        {
            provide: NG_VALIDATORS,
            useExisting: forwardRef(() => SppcNumericTextBox),
            multi: true,
        }
    ]
})

export class SppcNumericTextBox implements OnInit, ControlValueAccessor, Validator {

    parseError: boolean = false;

    @Input() public spinners: boolean = false;
    @Input() public format: string = '';
    @Input() public step: string = "1";

    public value: any;

    propagateChange: any = () => { };

    constructor() {
    }

    ngOnInit() {
    }

    keyPress(keyCode: any) {
        switch (keyCode) {
            case KeyCode.Plus: {
                this.value = this.value * 100;
                break;
            }
            case KeyCode.Minus: {
                this.value = this.value * 1000;
                break;
            }
            default:
                break;
        }
        this.propagateChange(this.value);
    }

    onChangeModel() {
        if (this.value != undefined) {
            this.parseError = false;
            this.propagateChange(this.value);
        }
        else {
            //this.parseError = true;
            this.propagateChange();
        }
    }

    writeValue(value: any): void {
        if (value != undefined) {
            this.value = value;
        }
    }

    registerOnChange(fn: any): void {
        this.propagateChange = fn;
    }

    registerOnTouched(fn: any): void { }

    public validate(c: FormControl) {
        return (!this.parseError) ? null : {
            jsonParseError: {
                valid: false,
            },
        };
    }
}
