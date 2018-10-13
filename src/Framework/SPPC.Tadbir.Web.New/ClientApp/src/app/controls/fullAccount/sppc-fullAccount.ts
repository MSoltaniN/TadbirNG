import { Component, OnInit } from '@angular/core'
import { FormBuilder, ControlContainer, Validators } from '@angular/forms'
import { LookupService, FullAccountInfo } from '../../service/index';

interface Item {
    key: string,
    value: string
}

@Component({
    selector: 'sppc-fullAccount',
    template: `
    <ng-container [formGroup]="controlContainer.control">
     
<div>
    
    <kendo-dropdownlist [data]="accountsRows" [valuePrimitive]="true" formControlName="accountId" class="ddl-fAcc"
                        [textField]="'value'" [value]="selectedAccountValue" [(ngModel)]="selectedAccountValue" valueField="key" [defaultItem]="{ value: '', key: null}">
    </kendo-dropdownlist>

    <kendo-dropdownlist [data]="detailAccountsRows" [valuePrimitive]="true" formControlName="detailId" class="ddl-fAcc"
                        [textField]="'value'" [value]="selectedDetailAccountValue" [(ngModel)]="selectedDetailAccountValue" [valueField]="'key'" [defaultItem]="{ value: '', key: null}">
    </kendo-dropdownlist>

    <kendo-dropdownlist [data]="costCentersRows" [valuePrimitive]="true" formControlName="costCenterId" class="ddl-fAcc"
                        [textField]="'value'" [value]="selectedCostCenterValue" [(ngModel)]="selectedCostCenterValue" [valueField]="'key'" [defaultItem]="{ value: '', key: null}">
    </kendo-dropdownlist>

    <kendo-dropdownlist [data]="projectsRows" [valuePrimitive]="true" formControlName="projectId" class="ddl-fAcc"
                        [textField]="'value'" [value]="selectedprojectValue" [(ngModel)]="selectedprojectValue" [valueField]="'key'" [defaultItem]="{ value: '', key: null}">
    </kendo-dropdownlist>


    <div class="k-tooltip k-tooltip-validation" [hidden]="controlContainer.control.valid || controlContainer.control.pristine">
        {{ 'AllValidations.FullAccount.AccountIdIsRequired' | translate }}
    </div>

</div>



    </ng-container>
  `,
    styles: ['.ddl-fAcc {width:49%}'],
})
export class SppcFullAccountComponent implements OnInit {


    public accountsRows: Array<Item>;
    public detailAccountsRows: Array<Item>;
    public costCentersRows: Array<Item>;
    public projectsRows: Array<Item>;

    public selectedAccountValue: string;
    public selectedDetailAccountValue: string;
    public selectedCostCenterValue: string;
    public selectedprojectValue: string;

    fullAccount: FullAccountInfo;

    constructor(public controlContainer: ControlContainer, private lookupService: LookupService) {
        this.GetAccounts();
        this.GetDetailAccounts();
        this.GetCostCenters();
        this.GetProjects();
    }

    ngOnInit(): void {

        this.fullAccount = this.controlContainer.value;

        if (this.fullAccount.account.id)
            this.selectedAccountValue = this.fullAccount.account.id.toString();
        if (this.fullAccount.detailAccount.id)
            this.selectedDetailAccountValue = this.fullAccount.detailAccount.id.toString();
        if (this.fullAccount.costCenter.id)
            this.selectedCostCenterValue = this.fullAccount.costCenter.id.toString();
        if (this.fullAccount.project.id)
            this.selectedprojectValue = this.fullAccount.project.id.toString();
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
