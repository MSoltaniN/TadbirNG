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
    //parentFullCode: string = '';

    accParentId: number = 0;

    @Input() public disableSaveBtn: boolean = false;
    @Input() public isNew: boolean = false;
    @Input() public errorMessage: string = '';

    @Input() public parentTitle: string = '';
    @Input() public parentValue: string = '';

    @Input() public set parentId(id: number) {
        this.accParentId = 0;
        if (id)
            this.accParentId = id;
    }

    @Input() public set model(account: Account) {

        this.accountService.getAccountFullCode(this.accParentId).subscribe(res => {
            this.editForm.reset(account);
            var fullCode = res;
            if (account)
                fullCode = res + account.code;
            this.editForm.patchValue({ fullCode: fullCode });

            this.active = account !== undefined || this.isNew;
            this.disableSaveBtn = false;
        })
    }

    @Output() cancel: EventEmitter<any> = new EventEmitter();
    @Output() save: EventEmitter<Account> = new EventEmitter();
    //create properties

    //public fiscalPeriodRows: Array<Item>;
    public selectedValue: string = '1';

    //Events
    public onSave(e: any): void {
        debugger;
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

    onChanges(): void {
        //    this.myForm.valueChanges.subscribe(val => {
        //        this.formattedMessage =
        //            `Hello,

        //My name is ${val.name} and my email is ${val.email}.

        //I would like to tell you that ${val.message}.`;
        //    });



        //if (this.editForm) {
        //    this.editForm.get('code').valueChanges.subscribe(val => {

        //    });
        //}


        //this.editForm.get('code').valueChanges.subscribe(val => {

        //});

        //this.editForm.valueChanges.subscribe(res => {

        //    debugger;
        //    for (const field in res) {

        //        const formControl = this.editForm.get(field);

        //        //if (formControl) {
        //        //    formControl.setValue(res[field]);
        //        //}
        //        if (field == "fullCode") {
        //            this.editForm.patchValue({ fullCode: 'res.code' });
        //        }
        //    }
        //    ////debugger;
        //    //console.log(res);
        //    ////var f = this.editForm.controls['fullCode'].value;
        //    //this.editForm.patchValue({ fullCode: 'res.code' });
        //})

    }

}