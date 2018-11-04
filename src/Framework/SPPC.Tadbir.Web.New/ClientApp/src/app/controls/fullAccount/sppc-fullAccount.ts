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

<div formGroupName="account" class="fAcc">
<kendo-dropdownlist [data]="accountsRows" [valuePrimitive]="true" formControlName="id" class="ddl-fAcc" [filterable]="true" (filterChange)="handleAccountFilter($event)"
                        [textField]="'value'" [value]="selectedAccountValue" [(ngModel)]="selectedAccountValue" valueField="key" [defaultItem]="{ value: '', key: undefined}">
    </kendo-dropdownlist>
</div>
    

<div formGroupName="detailAccount" class="fAcc">
    <kendo-dropdownlist [data]="detailAccountsRows" [valuePrimitive]="true" formControlName="id" class="ddl-fAcc" [filterable]="true" (filterChange)="handleDetailAccountFilter($event)"
                        [textField]="'value'" [value]="selectedDetailAccountValue" [(ngModel)]="selectedDetailAccountValue" [valueField]="'key'" [defaultItem]="{ value: '', key: undefined}">
    </kendo-dropdownlist>
</div>

<div formGroupName="costCenter" class="fAcc">
    <kendo-dropdownlist [data]="costCentersRows" [valuePrimitive]="true" formControlName="id" class="ddl-fAcc" [filterable]="true" (filterChange)="handleCostCenterFilter($event)"
                        [textField]="'value'" [value]="selectedCostCenterValue" [(ngModel)]="selectedCostCenterValue" [valueField]="'key'" [defaultItem]="{ value: '', key: undefined}">
    </kendo-dropdownlist>
</div>

<div formGroupName="project" class="fAcc">
    <kendo-dropdownlist [data]="projectsRows" [valuePrimitive]="true" formControlName="id" class="ddl-fAcc" [filterable]="true" (filterChange)="handleProjectFilter($event)"
                        [textField]="'value'" [value]="selectedprojectValue" [(ngModel)]="selectedprojectValue" [valueField]="'key'" [defaultItem]="{ value: '', key: undefined}">
    </kendo-dropdownlist>
</div>


<div class="k-tooltip k-tooltip-validation"[hidden] = "controlContainer.control.valid || controlContainer.control.pristine" >
  {{ 'AllValidations.FullAccount.AccountIdIsRequired' | translate }}
</div>
    
</div>



    </ng-container>
  `,
  styles: [' .fAcc{width:49%; display: inline-block;} .ddl-fAcc {width:100%}'],
})






export class SppcFullAccountComponent implements OnInit {


  public accountsRows: Array<Item>;
  public detailAccountsRows: Array<Item>;
  public costCentersRows: Array<Item>;
  public projectsRows: Array<Item>;

  accountList: any;
  detailAccountList: any;
  costCenterList: any;
  projectList: any;

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
      this.accountList = res;
    })

  }

  GetDetailAccounts() {
    this.lookupService.GetDetailAccountsLookup().subscribe(res => {
      this.detailAccountsRows = res;
      this.detailAccountList = res;
    })
  }

  GetCostCenters() {
    this.lookupService.GetCostCentersLookup().subscribe(res => {
      this.costCentersRows = res;
      this.costCenterList = res;
    })
  }

  GetProjects() {
    this.lookupService.GetProjectsLookup().subscribe(res => {
      this.projectsRows = res;
      this.projectList = res;
    })
  }

  handleAccountFilter(strValue: any) {
    this.accountsRows = this.accountList.filter((s) => s.value.toLowerCase().indexOf(strValue.toLowerCase()) !== -1);
  }

  handleDetailAccountFilter(strValue: any) {
    this.detailAccountsRows = this.detailAccountList.filter((s) => s.value.toLowerCase().indexOf(strValue.toLowerCase()) !== -1);
  }

  handleCostCenterFilter(strValue: any) {
    this.costCentersRows = this.costCenterList.filter((s) => s.value.toLowerCase().indexOf(strValue.toLowerCase()) !== -1);
  }

  handleProjectFilter(strValue: any) {
    this.projectsRows = this.projectList.filter((s) => s.value.toLowerCase().indexOf(strValue.toLowerCase()) !== -1);
  }
}
