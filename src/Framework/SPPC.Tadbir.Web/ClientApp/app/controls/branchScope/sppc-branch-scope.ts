import { Component, OnInit, Input, forwardRef, OnChanges, OnDestroy, ViewChild, SimpleChanges, Optional, Host, SkipSelf } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NG_VALIDATORS, FormControl, Validator, ControlContainer, AbstractControl } from '@angular/forms'


interface Item {
    key: number,
    value: string
}

@Component({
    selector: 'sppc-branch-scope',
    template: `
               <kendo-dropdownlist [data]="scopeData" [valuePrimitive]="true" [disabled]="!isNew"
                                   [textField]="'value'" [(ngModel)]="scopeSelected" [value]="scopeSelected" [valueField]="'key'"
                                   (valueChange)="onPermissionChange($event)">        
                            <ng-template kendoDropDownListValueTemplate let-dataItem>
                                {{ dataItem?.value | translate }}
                            </ng-template>
                            <ng-template kendoDropDownListItemTemplate let-dataItem>
                                {{ dataItem?.value | translate }}
                            </ng-template>
               </kendo-dropdownlist>
`,
    styles: [`/deep/ .k-dropdown { width:100% }`],
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => SppcBranchScope),
            multi: true
        },
        {
            provide: NG_VALIDATORS,
            useExisting: forwardRef(() => SppcBranchScope),
            multi: true,
        }
    ]
})
export class SppcBranchScope implements OnInit, ControlValueAccessor, Validator {

    private parseError: boolean = false;
    public scopeData: Array<Item>;
    permission: number;
    scopeSelected: number;

    @Input() public parentScope: number;
    @Input() public isNew: boolean;

    propagateChange: any = () => { };


    constructor() {
        this.scopeData = [
            { value: "BranchScope.AllBranches", key: 0 },
            { value: "BranchScope.CurrentBranchAndSubsets", key: 1 },
            { value: "BranchScope.CurrentBranch", key: 2 }
        ];
    }

    ngOnInit() {
        if (this.isNew) {
            this.scopeSelected = this.parentScope;
        }
        this.scopeData = this.scopeData.filter(f => f.key >= this.parentScope);
    }


    onPermissionChange(e: any) {
        if (this.scopeSelected && this.scopeSelected >= 0) {
            this.parseError = false;
            this.propagateChange(this.scopeSelected);
        }
        else {
            this.parseError = true;
        }
    }


    writeValue(value: any): void {
        if (value != undefined) {
            if (this.isNew)
                this.scopeSelected = this.parentScope;
            else
                this.scopeSelected = value;

        }
    }

    registerOnChange(fn: any): void {
        this.propagateChange = fn;
    }

    registerOnTouched(fn: any): void {
        //this.propagateChange = fn;
    }

    public validate(control: FormControl) {
        return (!this.parseError) ? null : {
            jsonParseError: {
                valid: false,
            },
        };
    }


}
