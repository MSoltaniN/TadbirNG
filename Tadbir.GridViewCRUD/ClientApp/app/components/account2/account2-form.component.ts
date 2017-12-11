import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Validators, FormGroup, FormControl } from '@angular/forms';
import { AccountService, AccountInfo, TransactionLineService, TransactionLineInfo, FiscalPeriodService } from '../../service/index';

import { Account, TransactionLine } from '../../model/index';
import { TranslateService } from "ng2-translate";
import { ToastrService, ToastConfig } from 'toastr-ng2'; 

import { Observable } from 'rxjs/Observable';

interface Item {
    Key: string,
    Value: string
}


@Component({
    selector: 'account-form-component',
    styles: [
        "input[type=text] { width: 100%; }"
    ],
    templateUrl: './account2-form.component.html'
})
        
export class AccountFormComponent {

    //create a form controls
    private editForm = new FormGroup({
        accountId : new FormControl("", Validators.required),
        code: new FormControl("", Validators.required),
        name: new FormControl("", Validators.required),   
        description: new FormControl(),
        fiscalPeriodId: new FormControl("", Validators.required),
        branchId: new FormControl("", Validators.required)
    });

    //create properties
    active: boolean = false;
    @Input() public isNew: boolean = false;

    @Input() public set model(account: Account) {
        
        this.editForm.reset(account);

        this.active = account !== undefined;
        if (account != undefined)
        {
            //var index = this.fiscalPeriodRows.find(p => p.Key == account.fiscalPeriodId.toString());
            this.selectedValue = account.fiscalPeriodId.toString();
            if (this.fiscalPeriodRows == undefined) this.getFiscalPeriod();
        }
            
        //this.editForm.setValue({ fiscalPeriodId: account.fiscalPeriodId });
    }

    @Output() cancel: EventEmitter<any> = new EventEmitter();
    @Output() save: EventEmitter<Account> = new EventEmitter();
    //create properties

    //public placeHolder: { Key: string, Value: string } = { Key: "-1" , Value: "---" };
    public fiscalPeriodRows : Array<Item>;
    public selectedValue : string = '1';

    //Events
    public onSave(e : any): void {
        e.preventDefault();
        this.save.emit(this.editForm.value);
        this.active = false;
    }

    public onCancel(e : any): void {
        e.preventDefault();
        this.closeForm();
    }

    private closeForm(): void {
        this.active = false;
        this.cancel.emit();
    }
    //Events

    constructor(private accountService: AccountService, private transactionLineService: TransactionLineService, private fiscalPeriodService: FiscalPeriodService,
        private toastrService: ToastrService, private translate: TranslateService) {
        translate.addLangs(["en", "fa"]);
        translate.setDefaultLang('fa');

        var browserLang = 'fa';//translate.getBrowserLang();
        translate.use(browserLang);

        this.getFiscalPeriod();
        
    }

    /* load fiscal periods */
    getFiscalPeriod() {
        
        this.fiscalPeriodService.getFiscalPeriods().subscribe(res => {
            this.fiscalPeriodRows = res;            
        });
    }
}