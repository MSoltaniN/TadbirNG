import { Component, OnInit, Input, forwardRef, OnChanges, OnDestroy, ViewChild } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NG_VALIDATORS, FormControl, Validator } from '@angular/forms'

import { KeyCode } from '../../enum/KeyCode';

@Component({
    selector: 'sppc-numericTextbox',
    template: `
 <kendo-numerictextbox [(ngModel)]="numberValue" [spinners]="spinners" [format]="format" class="input-number">
                        </kendo-numerictextbox>
`,
    styles: [`
.input-number{ width:100% }
       `],
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => SppcNumericTextbox),
            multi: true
        },
        {
            provide: NG_VALIDATORS,
            useExisting: forwardRef(() => SppcNumericTextbox),
            multi: true,
        }
    ]
})
export class SppcNumericTextbox implements OnInit, ControlValueAccessor, Validator {


    parseError: boolean = false;

    @Input() spinners: boolean = false;
    @Input() format: string = '';


    propagateChange: any = () => { };

    constructor() {
    }

    ngOnInit() {
    }


    writeValue(value: any): void {
        if (value) {

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
