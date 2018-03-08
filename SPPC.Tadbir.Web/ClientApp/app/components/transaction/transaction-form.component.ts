import { Component, Input, Output, EventEmitter, Renderer2 } from '@angular/core';
import { Validators, FormGroup, FormControl } from '@angular/forms';
import { TransactionService, TransactionInfo, TransactionLineService, TransactionLineInfo, FiscalPeriodService } from '../../service/index';

import { Transaction, TransactionLine } from '../../model/index';
import { TranslateService } from "ng2-translate";
import { ToastrService, ToastConfig } from 'toastr-ng2';

import { Observable } from 'rxjs/Observable';
import { ContextInfo } from "../../service/login/authentication.service";
import { DefaultComponent } from "../../class/default.component";

import { Layout } from "../../enviroment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';


export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
} 

interface Item {
    Key: string,
    Value: string
}


@Component({
    selector: 'transaction-form-component',
    styles: [
        "input[type=text] { width: 100%; } /deep/ .new-dialog > .k-dialog {width: 450px !important; min-width: 250px !important;}",
        "/deep/ .edit-dialog > .k-dialog {width: 100% !important; min-width: 250px !important; height:100%}",
        "/deep/ .edit-dialog .k-window-titlebar{ padding: 5px 16px !important;}",
        "/deep/ .edit-dialog .edit-form-body { background: #f6f6f6; border: solid 1px #989898; border-radius: 4px; padding-top: 10px;}"
    ],
    templateUrl: './transaction-form.component.html',
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]
})

export class TransactionFormComponent extends DefaultComponent {

    //create a form controls
    private editForm = new FormGroup({
        id: new FormControl("", Validators.required),
        description: new FormControl(),
        no: new FormControl("", Validators.required),
        date: new FormControl("", Validators.required)
    });

    //create properties
    public transaction_Id: number;
    active: boolean = false;
    @Input() public isNew: boolean = false;

    @Input() public set model(transaction: Transaction) {

        this.editForm.reset(transaction);

        this.active = transaction !== undefined || this.isNew;

        if (transaction != undefined) {
            this.transaction_Id = transaction.id;
        }

    }

    @Output() cancel: EventEmitter<any> = new EventEmitter();
    @Output() save: EventEmitter<Transaction> = new EventEmitter();
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

    public onDeleteData() {
        alert("Data deleted.");
    }
    //Events

    constructor(private transactionService: TransactionService, private transactionLineService: TransactionLineService, private fiscalPeriodService: FiscalPeriodService,
        public toastrService: ToastrService, public translate: TranslateService, public renderer: Renderer2, public metadata: MetaDataService) {

        super(toastrService, translate, renderer, metadata,'Transaction');

    }

}