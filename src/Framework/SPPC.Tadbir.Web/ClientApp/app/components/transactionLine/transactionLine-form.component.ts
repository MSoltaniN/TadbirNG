import { Component, Input, Output, EventEmitter, Renderer2 } from '@angular/core';
import { Validators, FormGroup, FormControl } from '@angular/forms';
//import { requiredValidatorLogic } from './required.directive';
import { TransactionLineService, TransactionLineViewModelInfo, AccountService, LookupService } from '../../service/index';

import { TransactionLineViewModel } from '../../model/index';

import { TranslateService } from "ng2-translate";
import { ToastrService } from 'ngx-toastr';

import { Observable } from 'rxjs/Observable';
import { ContextInfo } from "../../service/login/authentication.service";
import { DefaultComponent } from "../../class/default.component";
import { MetaDataService } from '../../service/metadata/metadata.service';

import createNumberMask from 'text-mask-addons/dist/createNumberMask'
import { Metadatas, Entities } from '../../enviroment';
import { FullAccountService } from '../../service/fullAccount.service';




interface Item {
    Key: string,
    Value: string
}


@Component({
    selector: 'transactionLine-form-component',
    styles: [
        "input[type=text],textarea { width: 100%; } .ddl-fAcc {width:49%} /deep/ kendo-numerictextbox{ width:100% !important; }"
    ],
    templateUrl: './TransactionLine-form.component.html'
})

export class TransactionLineFormComponent extends DefaultComponent {

    //TODO: create form with metadata
    public editForm1 = new FormGroup({
        id: new FormControl(),
        transactionId: new FormControl(),
        currencyId: new FormControl("", Validators.required),
        debit: new FormControl("", Validators.required),
        credit: new FormControl("", Validators.required),
        description: new FormControl("", Validators.maxLength(512)),
        fullAccount: new FormControl()
    });


    public currenciesRows: Array<Item>;

    public selectedCurrencyValue: string;

    active: boolean = false;
    @Input() public isNew: boolean = false;
    @Input() public errorMessage: string;


    @Input() public set model(transactionLineViewModel: TransactionLineViewModel) {

        this.editForm1.reset(transactionLineViewModel);

        this.active = transactionLineViewModel !== undefined || this.isNew;

        if (transactionLineViewModel != undefined && transactionLineViewModel.currencyId > 0)
            this.selectedCurrencyValue = transactionLineViewModel.currencyId.toString();

    }


    @Output() cancel: EventEmitter<any> = new EventEmitter();
    @Output() save: EventEmitter<TransactionLineViewModel> = new EventEmitter();
    //create properties

    //Events
    public onSave(e: any): void {
        e.preventDefault();
        this.save.emit(this.editForm1.value);
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
        public toastrService: ToastrService, public translate: TranslateService, public lookupService: LookupService, private fullAccountService: FullAccountService,
        public renderer: Renderer2, public metadata: MetaDataService) {

        super(toastrService, translate, renderer, metadata, Entities.TransactionLine, Metadatas.TransactionArticles);

        this.GetCurrencies();
    }


    GetCurrencies() {
        this.lookupService.GetCurrenciesLookup().subscribe(res => {
            this.currenciesRows = res;
        })
    }

}