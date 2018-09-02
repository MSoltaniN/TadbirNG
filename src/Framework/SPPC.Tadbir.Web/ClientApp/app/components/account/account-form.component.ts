import { Component, Input, Output, EventEmitter, Renderer2, OnInit } from '@angular/core';
import { Validators, FormGroup, FormControl } from '@angular/forms';
import { AccountService, AccountInfo, VoucherLineService, FiscalPeriodService } from '../../service/index';

import { Account } from '../../model/index';
import { Property } from "../../class/metadata/property"
import { TranslateService } from "ng2-translate";
import { ToastrService } from 'ngx-toastr';

import { Observable } from 'rxjs/Observable';
import { ContextInfo } from "../../service/login/authentication.service";
import { DefaultComponent } from "../../class/default.component";

import { Layout, Entities, Metadatas } from "../../enviroment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { AccountApi } from '../../service/api/accountApi';
import { String } from '../../class/source';

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
        "input[type=text],textarea { width: 100%; },"
        , `.accInfoTitle {
        padding-right: 0px;
        padding-left: 0px;}`],
    templateUrl: './account-form.component.html',
    providers: [{
        provide: RTL,
        useFactory: getLayoutModule,
        deps: [Layout]
    }]

})

export class AccountFormComponent extends DefaultComponent implements OnInit {

    //create properties
    active: boolean = false;

    fullCodeApiUrl: string;

    @Input() public disableSaveBtn: boolean = false;
    @Input() public isNew: boolean = false;
    @Input() public errorMessage: string = '';

    @Input() public parentTitle: string = '';
    @Input() public parentValue: string = '';

    @Input() public set parentId(id: number) {
        this.fullCodeApiUrl = String.Format(AccountApi.AccountFullCode, id ? id : 0);
    }

    @Input() public set model(account: Account) {

        this.editForm.reset(account);

        this.active = account !== undefined || this.isNew;
        this.disableSaveBtn = false;
    }

    @Output() cancel: EventEmitter<any> = new EventEmitter();
    @Output() save: EventEmitter<Account> = new EventEmitter();
    //create properties

    //public fiscalPeriodRows: Array<Item>;
    public selectedValue: string = '1';

    //Events
    public onSave(e: any): void {
        e.preventDefault();
        if (this.editForm.valid) {
            this.disableSaveBtn = true;
            this.save.emit(this.editForm.value);
            this.active = true;
        }
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

    ngOnInit(): void {

//        this.onChanges();
    }

    constructor(private accountService: AccountService, private voucherLineService: VoucherLineService, private fiscalPeriodService: FiscalPeriodService,
        public toastrService: ToastrService, public translate: TranslateService, public renderer: Renderer2, public metadata: MetaDataService) {

        super(toastrService, translate, renderer, metadata, Entities.Account, Metadatas.Account);

        //this.getFiscalPeriod();

    }

    
}