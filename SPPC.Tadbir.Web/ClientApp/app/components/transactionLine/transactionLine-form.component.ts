import { Component, Input, Output, EventEmitter, Renderer2 } from '@angular/core';
import { Validators, FormGroup, FormControl } from '@angular/forms';
import {  TransactionLineService, TransactionLineInfo } from '../../service/index';

import { TransactionLine } from '../../model/index';
import { TranslateService } from "ng2-translate";
import { ToastrService, ToastConfig } from 'toastr-ng2';

import { Observable } from 'rxjs/Observable';
import { ContextInfo } from "../../service/login/authentication.service";
import { DefaultComponent } from "../../class/default.component";
import { MetaDataService } from '../../service/metadata/metadata.service';

import createNumberMask from 'text-mask-addons/dist/createNumberMask'



interface Item {
    Key: string,
    Value: string
}


@Component({
    selector: 'transactionLine-form-component',
    styles: [
        "input[type=text] { width: 100%; }"
    ],
    templateUrl: './TransactionLine-form.component.html'
})

export class TransactionLineFormComponent extends DefaultComponent {

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

        fullAccount: new FormControl()
    });

    //create properties
    active: boolean = false;
    @Input() public isNew: boolean = false;


    @Input() public set model(transactionLine: TransactionLine) {

        this.editForm.reset(transactionLine);

        this.active = transactionLine !== undefined || this.isNew;
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

    constructor(private transactionLineService: TransactionLineService,
        public toastrService: ToastrService, public translate: TranslateService,
        public renderer: Renderer2, public metadata: MetaDataService) {
        super(toastrService, translate, renderer, metadata,'TransactionLine');

        
    }

}