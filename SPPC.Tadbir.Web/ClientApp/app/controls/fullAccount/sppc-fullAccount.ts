import { Component, OnInit, Input, forwardRef, OnChanges, OnDestroy, ViewChild } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NG_VALIDATORS, FormControl, Validator } from '@angular/forms';
import { FullAccountService } from '../../service/index';

interface Item {
    key: string,
    value: string
}

@Component({
    selector: 'sppc-fullAccount',
    templateUrl: './spps-fullAccount.html',
    styles: ['.ddl-fAcc {width:49%}'],
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => SppcFullAccount),
            multi: true
        },
        {
            provide: NG_VALIDATORS,
            useExisting: forwardRef(() => SppcFullAccount),
            multi: true,
        }
    ]
})
export class SppcFullAccount implements OnInit, ControlValueAccessor, Validator {

    private parseError: boolean = false;

    public accountsRows: Array<Item>;
    public detailAccountsRows: Array<Item>;
    public costCentersRows: Array<Item>;
    public projectsRows: Array<Item>;
    public selectedAccountValue: string = '2';
    public selectedDetailAccountValue: number;
    public selectedCostCenterValue: number;
    public selectedprojectValue: number;

    constructor(private fullAccountService: FullAccountService) {
        this.GetAccounts();
        this.GetDetailAccounts();
        this.GetCostCenters();
        this.GetProjects();

        console.log(this.selectedAccountValue);
    }

    ngOnInit() { }


    @Input() fullAccount: any;

    propagateChange: any = () => { };


    writeValue(value: any): void {
        //debugger;
        if (value) {
            this.fullAccount = value;
            //this.selectedaccountValue = 1;
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

    public valueChange(value: any): void {
        debugger;
        this.selectedAccountValue = value;
    }

    GetAccounts() {
        this.fullAccountService.GetAccountsLookup().subscribe(res => {
            this.accountsRows = res;
        })

    }

    GetDetailAccounts() {
        this.fullAccountService.GetDetailAccountsLookup().subscribe(res => {
            this.detailAccountsRows = res;
        })
    }

    GetCostCenters() {
        this.fullAccountService.GetCostCentersLookup().subscribe(res => {
            this.costCentersRows = res;
        })
    }

    GetProjects() {
        this.fullAccountService.GetProjectsLookup().subscribe(res => {
            this.projectsRows = res;
        })
    }

}
