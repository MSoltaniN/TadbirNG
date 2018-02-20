import { Component, Input, Output, EventEmitter, Renderer2 } from '@angular/core';
import { Validators, FormGroup, FormControl } from '@angular/forms';
import { TransactionService, TransactionInfo, TransactionLineService, TransactionLineInfo, FiscalPeriodService } from '../../service/index';

import { Transaction, TransactionLine } from '../../model/index';
import { TranslateService } from "ng2-translate";
import { ToastrService, ToastConfig } from 'toastr-ng2';

import { Observable } from 'rxjs/Observable';
import { ContextInfo } from "../../service/login/authentication.service";
import { DefaultComponent } from "../../class/default.component";



interface Item {
    Key: string,
    Value: string
}


@Component({
    selector: 'transaction-form-component',
    styles: [
        "input[type=text] { width: 100%; }"
    ],
    templateUrl: './transaction-form.component.html'
})

export class TransactionFormComponent extends DefaultComponent{

    //create a form controls
    private editForm = new FormGroup({
        id: new FormControl("", Validators.required),
        fiscalPeriodId: new FormControl("", Validators.required),
        branchId: new FormControl("1", Validators.required),
        description: new FormControl(),
        no: new FormControl("", Validators.required),
        date: new FormControl("", Validators.required)
    });

    //create properties
    active: boolean = false;
    @Input() public isNew: boolean = false;

    @Input() public set model(transaction: Transaction) {

        this.editForm.reset(transaction);

        this.active = transaction !== undefined || this.isNew;
        if (transaction != undefined) {
            this.selectedValue = transaction.fiscalPeriodId.toString();
            if (this.fiscalPeriodRows == undefined) this.getFiscalPeriod();
        }

    }

    @Output() cancel: EventEmitter<any> = new EventEmitter();
    @Output() save: EventEmitter<Transaction> = new EventEmitter();
    //create properties

    //public placeHolder: { Key: string, Value: string } = { Key: "-1" , Value: "---" };
    public fiscalPeriodRows: Array<Item>;
    public selectedValue: string = '1';

    //Events
    public onSave(e: any): void {
        e.preventDefault();
        this.save.emit(this.editForm.value);
        this.active = false;
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

    constructor(private transactionService: TransactionService, private transactionLineService: TransactionLineService, private fiscalPeriodService: FiscalPeriodService,
        public toastrService: ToastrService, public translate: TranslateService, public renderer: Renderer2) {

        super(toastrService, translate, renderer, "Transaction");   
    
    }

    /* load fiscal periods */
    getFiscalPeriod() {

        var currentUser: ContextInfo = new ContextInfo();
        if (localStorage.getItem('currentContext')) {
            const userJson = localStorage.getItem('currentContext');

            currentUser = userJson !== null ? JSON.parse(userJson) : null;

        }

        this.fiscalPeriodService.getFiscalPeriod(currentUser.companyId).subscribe(res => {
            this.fiscalPeriodRows = res;
        });
    }
}