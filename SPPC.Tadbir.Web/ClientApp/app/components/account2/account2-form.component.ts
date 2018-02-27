import { Component, Input, Output, EventEmitter, Renderer2 } from '@angular/core';
import { Validators, FormGroup, FormControl } from '@angular/forms';
import { AccountService, AccountInfo, TransactionLineService, TransactionLineInfo, FiscalPeriodService } from '../../service/index';

import { Account, TransactionLine } from '../../model/index';
import { TranslateService } from "ng2-translate";
import { ToastrService, ToastConfig } from 'toastr-ng2'; 

import { Observable } from 'rxjs/Observable';
import { ContextInfo } from "../../service/login/authentication.service";
import { DefaultComponent } from "../../class/default.component";

import { Layout } from "../../enviroment";
import { RTL } from '@progress/kendo-angular-l10n';


export function getLayoutModule(layout: Layout) {
    return layout.getLayout();
} 

interface Item {
    Key: string,
    Value: string
}


@Component({
    selector: 'account-form-component',
    styles: [
        "input[type=text] { width: 100%; }"
    ],
    templateUrl: './account2-form.component.html',
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]

})
        
export class AccountFormComponent extends DefaultComponent{

    //create a form controls
    private editForm = new FormGroup({
        id : new FormControl("", Validators.required),
        code: new FormControl("", Validators.required),
        name: new FormControl("", Validators.required),   
        description: new FormControl(),
        fiscalPeriodId: new FormControl("", Validators.required),
        branchId: new FormControl("", Validators.required),
        level: new FormControl(0),
        fullCode: new FormControl("0")
    });

    //create properties
    active: boolean = false;
    @Input() public isNew: boolean = false;

    @Input() public set model(account: Account) {
        
        this.editForm.reset(account);

        this.active = account !== undefined || this.isNew;
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
        this.isNew = false;
        this.active = false;
        this.cancel.emit();
    }
    //Events

    constructor(private accountService: AccountService, private transactionLineService: TransactionLineService, private fiscalPeriodService: FiscalPeriodService,
        public toastrService: ToastrService, public translate: TranslateService, public renderer: Renderer2) {
        //translate.addLangs(["en", "fa"]);
        //translate.setDefaultLang('fa');

        //var browserLang = 'fa';//translate.getBrowserLang();
        //translate.use(browserLang);
        super(toastrService, translate, renderer, "Account");

        this.getFiscalPeriod();
        
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