import { Component, OnInit, Input, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NG_VALIDATORS, FormControl, Validator, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { LookupService, FullAccountService, FullAccountViewModelInfo } from '../../service/index';

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

    accountForm: FormGroup;

    public accountsRows: Array<Item>;
    public detailAccountsRows: Array<Item>;
    public costCentersRows: Array<Item>;
    public projectsRows: Array<Item>;

    public selectedAccountValue: string;
    public selectedDetailAccountValue: string;
    public selectedCostCenterValue: string;
    public selectedprojectValue: string;


    constructor(private lookupService: LookupService, private fullAccountService: FullAccountService, private formBuilder: FormBuilder) {
        this.GetAccounts();
        this.GetDetailAccounts();
        this.GetCostCenters();
        this.GetProjects();
    }

    ngOnInit() {
        this.accountForm = this.formBuilder.group({
            accountId: ['', Validators.required],
            detailId: '',
            costCenterId: '',
            projectId: ''
        });
    }


    @Input() fullAccount: FullAccountViewModelInfo;

    propagateChange: any = () => { };


    writeValue(value: any): void {

        if (value) {
            this.accountForm.setValue(value);
            
            this.fullAccount = value;

            if (this.fullAccount.accountId != null)    
                this.selectedAccountValue = this.fullAccount.accountId.toString();
            if (this.fullAccount.detailId != null)
                this.selectedDetailAccountValue = this.fullAccount.detailId.toString();
            if (this.fullAccount.costCenterId != null)
                this.selectedCostCenterValue = this.fullAccount.costCenterId.toString();
            if (this.fullAccount.projectId != null)
                this.selectedprojectValue = this.fullAccount.projectId.toString();
        }

        console.log(this.accountForm.value);
    }

    registerOnChange(fn: any): void {
        this.propagateChange = fn;
    }

    registerOnTouched(fn: any): void { }

    ddlChange(value: any) {
        this.propagateChange(this.accountForm.value);
    }

    public validate(c: FormControl) {

        if (this.accountForm.valid)
            this.parseError = false;
        else
            this.parseError = true;

        return (!this.parseError) ? null : {
            jsonParseError: {
                valid: false,
            },
        };
    }

    GetAccounts() {
        this.lookupService.GetAccountsLookup().subscribe(res => {
            this.accountsRows = res;
        })

    }

    GetDetailAccounts() {
        this.lookupService.GetDetailAccountsLookup().subscribe(res => {
            this.detailAccountsRows = res;
        })
    }

    GetCostCenters() {
        this.lookupService.GetCostCentersLookup().subscribe(res => {
            this.costCentersRows = res;
        })
    }

    GetProjects() {
        this.lookupService.GetProjectsLookup().subscribe(res => {
            this.projectsRows = res;
        })
    }

}
