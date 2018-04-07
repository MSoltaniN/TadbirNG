import { Component, Input, Output, EventEmitter, Renderer2 } from '@angular/core';
import { Validators, FormGroup, FormControl } from '@angular/forms';
import { AccountService, AccountInfo, TransactionLineService, TransactionLineInfo, FiscalPeriodService } from '../../service/index';

import { Account, TransactionLine } from '../../model/index';
import { TranslateService } from "ng2-translate";
import { ToastrService, ToastConfig } from 'toastr-ng2';

import { Observable } from 'rxjs/Observable';
import { ContextInfo } from "../../service/login/authentication.service";
import { DefaultComponent } from "../../class/default.component";

import { Layout, Entities, Metadatas } from "../../enviroment";
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

export class AccountFormComponent extends DefaultComponent {

    //create a form controls
    private editForm = new FormGroup({
        id: new FormControl("", Validators.required),
        code: new FormControl("", Validators.required),
        name: new FormControl("", Validators.required),
        description: new FormControl(),
        level: new FormControl(0),
        fullCode: new FormControl("0")
    });

    //create properties
    active: boolean = false;
    @Input() public isNew: boolean = false;
    @Input() public errorMessage: string = '';

    @Input() public set model(account: Account) {

        this.editForm.reset(account);

        this.active = account !== undefined || this.isNew;
        if (account != undefined) {
            this.selectedValue = account.fiscalPeriodId.toString();
            if (this.fiscalPeriodRows == undefined) this.getFiscalPeriod();
        }

    }

    @Output() cancel: EventEmitter<any> = new EventEmitter();
    @Output() save: EventEmitter<Account> = new EventEmitter();
    //create properties

    public fiscalPeriodRows: Array<Item>;
    public selectedValue: string = '1';

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

    constructor(private accountService: AccountService, private transactionLineService: TransactionLineService, private fiscalPeriodService: FiscalPeriodService,
        public toastrService: ToastrService, public translate: TranslateService, public renderer: Renderer2, public metadata: MetaDataService) {

        super(toastrService, translate, renderer, metadata, Entities.Account, Metadatas.Account);

        this.getFiscalPeriod();

    }

    /* load fiscal periods */
    getFiscalPeriod() {

        this.fiscalPeriodService.getFiscalPeriod(this.CompanyId).subscribe(res => {
            this.fiscalPeriodRows = res;
        });
    }
}