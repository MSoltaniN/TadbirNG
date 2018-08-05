
import { Component, OnInit, Input, forwardRef } from '@angular/core';
import { ControlValueAccessor, Validator, AbstractControl, FormControl, NG_VALUE_ACCESSOR, NG_VALIDATORS } from '@angular/forms';
import { KeyCode } from '../../enum/KeyCode';




@Component({
    selector: 'sppc-numberbox',
    template: `<kendo-numerictextbox
          [spinners]="spinners"
          [format]="format"
          [step]="step"
          [(ngModel)]="value"    
        
          (keydown)="keyPress($event.keyCode)">
          </kendo-numerictextbox>`,
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => SppcNumberBox),
            multi: true
        },
        {
            provide: NG_VALIDATORS,
            useExisting: forwardRef(() => SppcNumberBox),
            multi: true,
        }
    ]
})

export class SppcNumberBox implements OnInit, ControlValueAccessor, Validator {

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

        if (this.value)
            this.parseError = false;
        else
            this.parseError = true;

        this.propagateChange(this.value);
    }

    writeValue(value: any): void {
        if (value) {
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