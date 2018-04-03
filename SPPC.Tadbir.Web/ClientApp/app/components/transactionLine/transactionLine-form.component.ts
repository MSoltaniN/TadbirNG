﻿import { Component, Input, Output, EventEmitter, Renderer2 } from '@angular/core';
import { Validators, FormGroup, FormControl } from '@angular/forms';
//import { requiredValidatorLogic } from './required.directive';
import { TransactionLineService, TransactionLineInfo, AccountService, LookupService } from '../../service/index';

import { TransactionLine, FullAccount } from '../../model/index';

import { TranslateService } from "ng2-translate";
import { ToastrService, ToastConfig } from 'toastr-ng2';

import { Observable } from 'rxjs/Observable';
import { ContextInfo } from "../../service/login/authentication.service";
import { DefaultComponent } from "../../class/default.component";
import { MetaDataService } from '../../service/metadata/metadata.service';

import createNumberMask from 'text-mask-addons/dist/createNumberMask'
import { Metadatas, Entities } from '../../enviroment';



interface Item {
    Key: string,
    Value: string
}


@Component({
    selector: 'transactionLine-form-component',
    styles: [
        "input[type=text] { width: 100%; } .ddl-fAcc {width:49%}"
    ],
    templateUrl: './TransactionLine-form.component.html'
})

export class TransactionLineFormComponent extends DefaultComponent {

    //TODO
    public dollarMask = createNumberMask({
        prefix: '$ ',
        suffix: ''
    })

    public rialMask = createNumberMask({
        prefix: '',
        suffix: ' ریال'
    })

    //create a form controls
    private editForm = new FormGroup({
        id: new FormControl("", Validators.required),
        debit: new FormControl("", Validators.required),
        credit: new FormControl("", Validators.required),
        description: new FormControl(),
        transactionId: new FormControl(),
        currencyId: new FormControl("",Validators.required),

        accountId: new FormControl("", Validators.required),
        detailId: new FormControl(),
        costCenterId: new FormControl(),
        projectId: new FormControl()
    });

    //create properties
    public accountsRows: Array<Item>;
    public detailAccountsRows: Array<Item>;
    public costCentersRows: Array<Item>;
    public projectsRows: Array<Item>;
    public currenciesRows: Array<Item>;
    public selectedAccountValue: string;
    public selectedDetailAccountValue: string;
    public selectedCostCenterValue: string;
    public selectedprojectValue: string;
    public selectedCurrencyValue: string;

    active: boolean = false;
    @Input() public isNew: boolean = false;
    @Input() public errorMessage: string;


    @Input() public set model(transactionLine: TransactionLine) {

        this.editForm.reset(transactionLine);
        this.active = transactionLine !== undefined || this.isNew;

        if (transactionLine != undefined) {
            if (transactionLine.accountId > 0)
                this.selectedAccountValue = transactionLine.accountId.toString();           
            if (transactionLine.detailId != undefined)
                this.selectedDetailAccountValue = transactionLine.detailId.toString();
            if (transactionLine.costCenterId != undefined)
                this.selectedCostCenterValue = transactionLine.costCenterId.toString();
            if (transactionLine.projectId != undefined)
                this.selectedprojectValue = transactionLine.projectId.toString();

            if (transactionLine.currencyId > 0)
            this.selectedCurrencyValue = transactionLine.currencyId.toString();
        }
    }

    @Output() cancel: EventEmitter<any> = new EventEmitter();
    @Output() save: EventEmitter<TransactionLine> = new EventEmitter();
    //create properties

    //Events
    public onSave(e: any): void {
        e.preventDefault();
        this.save.emit(this.editForm.value);
        this.active = true;
    }

    public onCancel(e: any): void {
        e.preventDefault();
        this.closeForm();
    }

    private closeForm(): void {
        this.isNew = false;
        this.active = false;
        this.cancel.emit();
    }
    //Events


    constructor(private transactionLineService: TransactionLineService, private accountService: AccountService,
        public toastrService: ToastrService, public translate: TranslateService, public lookupService: LookupService,
        public renderer: Renderer2, public metadata: MetaDataService) {

        super(toastrService, translate, renderer, metadata, Entities.Transaction, Metadatas.Transaction);    
        
        this.GetAccounts();
        this.GetDetailAccounts();
        this.GetCostCenters();
        this.GetProjects();
        this.GetCurrencies();
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

    GetCurrencies() {
        this.lookupService.GetCurrenciesLookup().subscribe(res => {
            this.currenciesRows = res;
        })
    }

}